using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Filesystem;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using Tao.OpenGl;

namespace Watertight.Resources
{


    internal class TextureLoader : ResourceFactory<Texture>
    {
        public override Texture getResource(System.IO.StreamReader stream)
        {
            return new Texture(new Bitmap(stream.BaseStream));            
        }
    }


    public class Texture : Resource, IDisposable
    {
        Bitmap image;

        int textureID;

        public int TextureID { get { return textureID; } }

        public Texture(Bitmap data)
        {
            this.image = data;
            textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Modulate);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            BitmapData bitmapdata = image.LockBits(new Rectangle(0, 0, data.Width, data.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, bitmapdata.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            data.UnlockBits(bitmapdata);
        }

        

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
        }


        ~Texture()
        {
            Destroy();
        }


        public void Dispose()
        {
            Destroy();
            GC.SuppressFinalize(this);
        }
        
        public void Destroy()
        {
            GL.DeleteTexture(textureID);
        }

        public Uri Path
        {
            get;           
            set;           
        }
    }

}
