using System;

namespace StreetDetectiveV10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Street Detective v1.0!");
            Console.WriteLine("-----");

            SEVirtual.GameLoop gameLoop = new SEVirtual.GameLoop();
            gameLoop.Run();

            Console.WriteLine("-----");
            Console.WriteLine("Thanks & goodbye!");
        }
    }
}
