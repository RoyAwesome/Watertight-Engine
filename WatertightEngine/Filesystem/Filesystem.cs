using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using Watertight.Mods;
using Newtonsoft.Json;
using Watertight.LuaSystem;

namespace Watertight.Filesystem
{
    class FileSystem
    {
        public static readonly string ModDirectory = "mods/";
        public static readonly string CacheDirectory = "cache/"; 
    

        static Dictionary<Type, object> factories;
        static FileSystemPathFinder[] pathOrder;
        static FileSystem()
        {
            if (!Directory.Exists(ModDirectory)) Directory.CreateDirectory(ModDirectory);
            if (!Directory.Exists(CacheDirectory)) Directory.CreateDirectory(CacheDirectory);
           
            factories = new Dictionary<Type, object>();
            factories[typeof(LuaFile)] = new LuaFileFactory();
            factories[typeof(ModDescriptor)] = new DescriptorFactory();

            pathOrder = new FileSystemPathFinder[] { new FileSystemSearchPath(CacheDirectory) ,
                                                    new FileSystemSearchPath(ModDirectory),
                                                    new ModFileSearchPath() };

        }
      
        public static StreamReader GetFileStream(Uri path)
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
            E r = LoadResource<E>(GetFileStream(path));
            r.Path = path;
            return r;
        }

        public static E LoadResource<E>(StreamReader reader) where E : Resource
        {
            if (!factories.ContainsKey(typeof(E))) throw new ArgumentException("Cannot load type " + typeof(E).ToString());
            ResourceFactory<E> factory = (ResourceFactory<E>)factories[typeof(E)];
            E ret = factory.getResource(reader);
            reader.Close();
            return ret;
        }


    }

}
