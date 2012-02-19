

using System;
using System.IO;
namespace Watertight
{
    class Program
    {
     
        static void Main(string[] args)
        {
            if (!Directory.Exists("bin/")) Directory.CreateDirectory("bin/");
            AppDomain.CurrentDomain.AppendPrivatePath("bin/");
#if CLIENT
            Game game = new WatertightClient();
#else
            Game game = new WatertightServer();
#endif    

            game.Start(30);    
   
        }
    }
}
