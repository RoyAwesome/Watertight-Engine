using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Filesystem;
using System.IO;

namespace Watertight.Resources
{
    class TextFileLoader : ResourceFactory<TextFile>
    {
        public override TextFile GetResource(Stream stream)
        {
            TextFile file = new TextFile();

            using (StreamReader reader = new StreamReader(stream))
            {
                file.Text = reader.ReadToEnd();
            }
            return file;
        }
    }

    public class TextFile : Resource
    {
        public string Text;
                
        public Uri Path
        {
            get;
            set;
        }
    }
}
