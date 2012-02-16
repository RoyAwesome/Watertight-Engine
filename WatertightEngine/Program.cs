#define CLIENT



namespace Watertight
{
    class Program
    {
     
        static void Main(string[] args)
        {
#if CLIENT
            Game game = new WatertightClient();
#else
            Game game = new WatertightServer();
#endif    

            game.Start(30);    
   
        }
    }
}
