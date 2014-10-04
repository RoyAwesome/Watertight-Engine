using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using Watertight.Mods;
using Newtonsoft.Json;
using Watertight.LuaSystem;
using Watertight.Resources;

namespace Watertight.Filesystem
{
    class FileSystem
    {
        public static readonly string ModDirectory = "mods/";
        public static readonly string CacheDirectory = "cache/"; 
    

        static Dictionary<Type, object> factories;
        static FileSystemPathFinder[] pathOrder;

        static Dictionary<Uri, Resource> ResourceCache = new Dictionary<Uri, Resource>();
        
        static FileSystem()
        {
            if (!Directory.Exists(ModDirectory)) Directory.CreateDirectory(ModDirectory);
            if (!Directory.Exists(CacheDirectory)) Directory.CreateDirectory(CacheDirectory);
           
            factories = new Dictionary<Type, object>();
            factories[typeof(LuaFile)] = new LuaFileFactory();
            factories[typeof(ModDescriptor)] = new DescriptorFactory();
            factories[typeof(Texture)] = new TextureLoader();
            factories[typeof(Effect)] = new EffectLoader();
            factories[typeof(TextFile)] = new TextFileLoader();

            pathOrder = new FileSystemPathFinder[] {new FileSystemSearchPath(ModDirectory),
                                                    new ModFileSearchPath() };

        }
      
        public static Stream GetFileStream(Uri path)
        {
            for (int i = 0; i < pathOrder.Length; i++)
            {
                if (pathOrder[i].ExistsInPath(path))
                {
                    return pathOrder[i].GetFileStream(path);
                }
            }
            throw new ArgumentException("Cannot find file: " + path.ToString() + " In any search path!");
        }

        

        public static E LoadResource<E>(Uri path) where E : Resource
        {
            if(ResourceCache.ContainsKey(path))
            {
                return (E)ResourceCache[path];
            }

            E resource = (factories[typeof(E)] as ResourceFactory<E>).GetResource(path);


            resource.Path = path;
            ResourceCache[path] = resource;

            return resource;
        }

        public static E LoadResource<E>(Stream reader) where E : Resource
        {
            if (!factories.ContainsKey(typeof(E))) throw new ArgumentException("Cannot load type " + typeof(E).ToString());
            ResourceFactory<E> factory = (ResourceFactory<E>)factories[typeof(E)];
            E ret = factory.GetResource(reader);
            reader.Close();
            return ret;
        }

        [BindFunction("FS", "LoadEffect")]
        public static Effect LoadShader(string path)
        {
            return LoadResource<Effect>(new Uri(path));
        }



        [BindFunction("FS", "GetFilesInDirectory")]
        public static string[] GetFilesInDirectory(string dir)
        {
            //TODO: SECURITY: Check to see if dir is inside the mod's writable dir

            
            return Directory.GetFiles(dir).Select(x=> Path.GetFileName(x)).ToArray();
        }
    }

}
