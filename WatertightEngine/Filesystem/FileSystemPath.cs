using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;

namespace Watertight.Filesystem
{
    abstract class FileSystemPathFinder
    {
        protected abstract bool ExistsInPath(string file, string path);
        public virtual bool ExistsInPath(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return ExistsInPath(FileSystem.ModDirectory + modFile, filePath);
        }

        protected abstract StreamReader GetFileStream(string file, string path);
        public virtual StreamReader GetFileStream(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return GetFileStream(FileSystem.ModDirectory + modFile, filePath);
        }
    }

    internal class ModFileSearchPath : FileSystemPathFinder
    {
        protected override bool ExistsInPath(string file, string path)
        {
            
            using(ZipFile zip = new ZipFile(file + ".mod"))
            {
                return zip.ContainsEntry(path);                
            }
        }

        protected override StreamReader GetFileStream(string file, string path)
        {
            ZipFile zip = new ZipFile(file + ".mod");           
            if (!zip.ContainsEntry(path)) throw new ArgumentException("File " + path.ToString() + " Does not exist in mod!");
            ZipEntry entry = zip[path];
            return new StreamReader(entry.OpenReader());
           
        }

    }

    internal class FileSystemSearchPath : FileSystemPathFinder
    {
        protected override bool ExistsInPath(string file, string path)
        {
            return File.Exists(file + path);
        }

        public override bool ExistsInPath(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return ExistsInPath(FileSystem.ModDirectory + modFile + "/", filePath);
        }

        public override StreamReader GetFileStream(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return GetFileStream(FileSystem.ModDirectory + modFile + "/", filePath);
        }


        protected override StreamReader GetFileStream(string file, string path)
        {
            return new StreamReader(file + path);
        }
    }

    internal class CacheSystemSearchPath : FileSystemPathFinder
    {
        public override bool ExistsInPath(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);
           return ExistsInPath(FileSystem.CacheDirectory + modFile, filePath);
        }

        protected override bool ExistsInPath(string file, string path)
        {
        
             Console.WriteLine("\t\t Querying: " + file + "/" +  path);
            
            return File.Exists(file + "/" + path);
        }

        public override StreamReader GetFileStream(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return GetFileStream(FileSystem.CacheDirectory + modFile, filePath);
        }

        protected override StreamReader GetFileStream(string file, string path)
        {
            return new StreamReader(file + "/" + path);
        }

    }

}
