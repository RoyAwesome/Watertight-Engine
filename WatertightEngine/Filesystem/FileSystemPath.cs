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
        protected string directory;

        public FileSystemPathFinder(string folder)
        {
            this.directory = folder;
        }

        protected abstract bool ExistsInPath(string file, string path);
        public virtual bool ExistsInPath(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return ExistsInPath(FileSystem.ModDirectory + modFile, filePath);
        }

        protected abstract Stream GetFileStream(string file, string path);
        public virtual Stream GetFileStream(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return GetFileStream(directory + modFile, filePath);
        }
    }

    internal class ModFileSearchPath : FileSystemPathFinder
    {
        public ModFileSearchPath()
            : base(FileSystem.ModDirectory)
        {

        }

        protected override bool ExistsInPath(string file, string path)
        {
            
            using(ZipFile zip = new ZipFile(file + ".mod"))
            {
                return zip.ContainsEntry(path);                
            }
        }

        protected override Stream GetFileStream(string file, string path)
        {
            ZipFile zip = new ZipFile(file + ".mod");           
            if (!zip.ContainsEntry(path)) throw new ArgumentException("File " + path.ToString() + " Does not exist in mod!");
            ZipEntry entry = zip[path];
            return entry.OpenReader();
           
        }

    }

    internal class FileSystemSearchPath : FileSystemPathFinder
    {
        public FileSystemSearchPath(string folder)
            : base(folder)
        {

        }

        protected override bool ExistsInPath(string file, string path)
        {
            return File.Exists(file + path);
        }

        public override bool ExistsInPath(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return ExistsInPath(directory + modFile + "/", filePath);
        }

        public override Stream GetFileStream(Uri path)
        {
            string modFile = path.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            string filePath = path.GetComponents(UriComponents.Path, UriFormat.UriEscaped);

            return GetFileStream(directory + modFile + "/", filePath);
        }


        protected override Stream GetFileStream(string file, string path)
        {
            return File.Open(file + path, FileMode.Open);
        }
    }

    
}
