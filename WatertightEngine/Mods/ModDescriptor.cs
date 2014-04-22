using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Watertight.Filesystem;

namespace Watertight.Mods
{
    [JsonObject(MemberSerialization.OptOut)]
    class ModDescriptor : Resource
    {
        string name;
        string[] author;
        string version;
        string serverMain;
        string clientMain;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string[] Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }


        public string ServerMain
        {
            get { return serverMain; }
            set { serverMain = value; }
        }

        public string ClientMain
        {
            get { return clientMain; }
            set { clientMain = value; }
        }

        public override string ToString()
        {
            return "Mod { Name = " + name + " Author = " + author + " Version = " + version + " ServerMain = " + serverMain +
                " ClientMain = " + clientMain;
        }


        public Uri Path
        {
            get;          
            set;           
        }

        public string[] IncludeFiles
        {
            get;
            set;
        }
    }
}
