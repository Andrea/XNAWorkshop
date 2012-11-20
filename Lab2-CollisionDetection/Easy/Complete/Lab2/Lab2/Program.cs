using System;

namespace Lab2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Lab2 game = new Lab2())
            {
                game.Run();
            }
        }
    }
#endif
}

