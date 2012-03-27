using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Watertight.Renderer.Shaders;

namespace Watertight.Renderer
{
    public enum RenderMode
    {
        GL11,
        GL30,

    }

   
    public abstract partial class BatchVertexRenderer
    {
        public static RenderMode RenderMode = RenderMode.GL30;

        public BaseShader ActiveShader { get; set; }


        protected List<float> vertexBuffer = new List<float>();
        protected List<float> colorBuffer = new List<float>();
        protected List<float> normalBuffer = new List<float>();
        protected List<float> uvBuffer = new List<float>();

        protected bool useColors = false;
        protected bool useNormals = false;
        protected bool useTexture = false;



        bool batching = false;
        bool flushed = true;

        protected int numVerticies = 0;


        

        public void Begin()
        {
            if (batching) throw new InvalidOperationException("Cannot begin while batching!");
            batching = true;
            flushed = false;
            vertexBuffer.Clear();
            colorBuffer.Clear();
            normalBuffer.Clear();
            uvBuffer.Clear();

            numVerticies = 0;
        }


        public void End()
        {
            if (!batching) throw new InvalidOperationException("Cannot End when not batching! (did you forget to call begin?)");
            batching = false;
            Flush();
        }


        public void Flush()
        {
            if (vertexBuffer.Count % 4 != 0) throw new BatcherException("Vertex Buffer Size not divisible by 4!");
            if (useColors)
            {
                if (colorBuffer.Count % 4 != 0) throw new BatcherException("Color Buffer Size not divisbile by 4!");
                if (colorBuffer.Count / 4 != numVerticies) throw new BatcherException("Buffer Size Mismatch! Color Buffer != numVerticies");
            }
            if (useNormals)
            {
                if (normalBuffer.Count % 4 != 0) throw new BatcherException("Normal Buffer Size not divisbile by 4!");
                if (normalBuffer.Count / 4 != numVerticies) throw new BatcherException("Buffer Size Mismatch! Normal Buffer != numVerticies");
            }
            if (useTexture)
            {
                if (uvBuffer.Count % 2 != 0) throw new BatcherException("UV Buffer Size not divisbile by 2!");
                if (uvBuffer.Count / 2 != numVerticies) throw new BatcherException("Buffer Size Mismatch! UV Buffer != numVerticies");
            }
            if (ActiveShader == null) throw new BatcherException("Shader cannot be null!");
            ActiveShader.Assign();
            doFlush();

            flushed = true;
        }


        protected abstract void doFlush();

        public void Draw()
        {
            if (batching) throw new BatcherException("Cannot Render while Batching!");
            if (!flushed) throw new BatcherException("Cannot Render without flushing the batch!");
            if (ActiveShader == null) throw new BatcherException("Shader cannot be null!");
            ActiveShader.Assign();
            doDraw();
        }
        protected abstract void doDraw();



        public void DumpBuffers()
        {
            Console.Write("Vertex Buffer \t\t\t ColorBuffer\n");
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                Console.Write(vertexBuffer[i] + "\t\t\t");
                Console.WriteLine(colorBuffer[i]);


            }

        }
    }
}
