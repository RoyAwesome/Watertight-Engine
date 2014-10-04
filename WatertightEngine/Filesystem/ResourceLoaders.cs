using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Watertight.LuaSystem;
using Watertight.Mods;
using Newtonsoft.Json;


namespace Watertight.Filesystem
{

    public interface Resource
    {
        Uri Path
        {
            get;
            set;
        }

    }

    abstract class ResourceFactory<E> where E : Resource
    {
        public abstract E GetResource(Stream stream);

        public virtual E GetResource(Uri path)
        {
            Stream s = FileSystem.GetFileStream(path);
            E instance = GetResource(s);
            s.Close();

            return instance;
        }
    }

    internal class LuaFileFactory : ResourceFactory<LuaFile>
    {
        public override LuaFile GetResource(Stream stream)
        {
            LuaFile file = new LuaFile();

            using(StreamReader reader = new StreamReader(stream))
            {
                file.Lua = reader.ReadToEnd();
            }
            
            return file;
        }

        public override LuaFile GetResource(Uri path)
        {
            LuaFile f = base.GetResource(path);
            f.mod = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            return f;
        }
    }

    internal class DescriptorFactory : ResourceFactory<ModDescriptor>
    {
        public override ModDescriptor GetResource(Stream stream)
        {
            JsonTextReader reader = new JsonTextReader(new StreamReader(stream));
            JsonSerializer ser = new JsonSerializer();
            return ser.Deserialize<ModDescriptor>(reader);
        }


    }

}
