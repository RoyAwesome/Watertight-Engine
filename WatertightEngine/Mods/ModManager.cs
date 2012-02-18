using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Filesystem;
using System.IO;
using LuaInterface;
using Watertight.LuaSystem;
using Ionic.Zip;

namespace Watertight.Mods
{
    static class ModManager
    {
        
        static Dictionary<string, Mod> loadedMods = new Dictionary<string, Mod>();

        public static IEnumerable<Mod> Mods()
        {
            return loadedMods.Values;
        }

        [BindFunction("_G", "GetMod")]
        public static Mod GetMod(string name)
        {
            
            if (!loadedMods.ContainsKey(name.ToLower())) throw new ArgumentException("Mod: " + name + " Is not loaded!");
            return loadedMods[name.ToLower()];
        }

        [BindFunction("_G", "RegisterMod")]       
        public static void RegisterMod(LuaTable table)
        {
            Mod mod = new Mod();

            ModDescriptor descriptor = ReadDescriptor(table);

            mod.Descriptor = descriptor;
            Console.WriteLine("Setting Mod: " + mod.GetName());
            loadedMods[mod.GetName().ToLower()] = mod;
           
            
        }


        private static ModDescriptor ReadDescriptor(LuaTable mod)
        {
            ModDescriptor d = new ModDescriptor();
            d.Name = (string)((string)mod["Name"]).Clone();
            string[] authors;
            if (mod["Author"] is LuaTable)
            {
                authors = new string[(mod["Author"] as LuaTable).Values.Count];
                (mod["Author"] as LuaTable).Values.CopyTo(authors, 0);
            }
            else
            {
                authors = new string[] { (string)mod["Author"] };
            }
            d.Author = authors;
            d.Version = (string)((string)mod["Version"]).Clone();
            d.ServerMain = (string)((string)mod["ServerMain"]).Clone();
            d.ClientMain = (string)((string)mod["ClientMain"]).Clone();
            
            return d;
        }

        public static void EnableMods()
        {
            foreach (Mod m in loadedMods.Values)
            {
                Console.WriteLine("Enabling Mod: " + m.Descriptor.Name);
                EnableMod(m);
            }
        }

        public static void EnableMod(Mod mod)
        {
            Uri entry;
            if(Watertight.GetGame().GetPlatform() == Platform.Server)
                entry = new Uri("script://" + mod.GetName() + "/" + mod.Descriptor.ServerMain);
            else
                entry = new Uri("script://" + mod.GetName() + "/" + mod.Descriptor.ClientMain);

            LuaFile entrypoint = FileSystem.LoadResource<LuaFile>(entry);
            entrypoint.DoFile(LuaHelper.LuaVM);
            Console.WriteLine("Loaded mod: " + mod.Descriptor.Name + " Version: " + mod.Descriptor.Version);

            mod.Init();

        
        }
      


     

        private static string StripFileStuff(string path)
        {
            int start = path.LastIndexOf('/');
            return path.Substring(start + 1).Replace(".mod", "");
        }

        private static void CacheModFile(string file)
        {
            using (ZipFile zip = new ZipFile(file))
            {
                zip.ExtractAll(FileSystem.CacheDirectory + StripFileStuff(file), ExtractExistingFileAction.OverwriteSilently);
            }

        }

        public static void LoadMods()
        {
           foreach(string dir in Directory.GetDirectories(FileSystem.ModDirectory))
           {
               Console.WriteLine(dir + "/mod.lua");
               if(File.Exists(dir + "/mod.lua"))
               {
                   Console.WriteLine("Loading Mod: " + dir);
                   using (StreamReader r = new StreamReader(dir + "/mod.lua"))
                   {
                       LuaFile f = FileSystem.LoadResource<LuaFile>(r);
                       f.DoFile(LuaHelper.LuaVM);
                   }
               }
           }

           foreach (string file in Directory.GetFiles(FileSystem.ModDirectory))
           {
               if (file.Contains(".mod"))
               {
                   Console.WriteLine("Loading Mod: " + file);
                   //Extract the mod into the cache for faster loading:
                   if(!Directory.Exists(FileSystem.CacheDirectory + StripFileStuff(file)))
                   {
                       CacheModFile(file);
                   }
                   if (Directory.GetLastWriteTime(FileSystem.CacheDirectory + StripFileStuff(file)) < File.GetLastWriteTime(file))
                   {
                       CacheModFile(file);
                   }
                   
                   //Load the lua file (Strip the .mod off the end because the .mod searcher will find it for us)
                   

                   Uri path = new Uri("script://" + StripFileStuff(file) + "/mod.lua");
                   LuaFile f = FileSystem.LoadResource<LuaFile>(path);
                   f.DoFile(LuaHelper.LuaVM);
               }

           }

        }



        static ModManager()
        {



        }



    }
}
