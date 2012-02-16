#define CLIENT



using System;
namespace Watertight
{
    class Program
    {
     
        static void Main(string[] args)
        {
            
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
