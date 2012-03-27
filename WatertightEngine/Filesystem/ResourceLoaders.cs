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
        public abstract E getResource(StreamReader stream);

        public virtual E getResource(Uri path)
        {
            return getResource(FileSystem.GetFileStream(path));
        }
    }

    internal class LuaFileFactory : ResourceFactory<LuaFile>
    {
        public override LuaFile getResource(StreamReader stream)
        {
            LuaFile file = new LuaFile();
            file.Lua = stream.ReadToEnd();
            return file;
        }

        public override LuaFile getResource(Uri path)
        {
            LuaFile f = base.getResource(path);
            f.mod = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            return f;
        }
    }

    internal class DescriptorFactory : ResourceFactory<ModDescriptor>
    {
        public override ModDescriptor getResource(StreamReader stream)
        {
            JsonTextReader reader = new JsonTextReader(stream);
            JsonSerializer ser = new JsonSerializer();
            return ser.Deserialize<ModDescriptor>(reader);
        }


    }

}
