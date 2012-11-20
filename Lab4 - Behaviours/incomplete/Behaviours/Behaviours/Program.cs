namespace Behaviours
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BehavioursLab game = new BehavioursLab())
            {
                game.Run();
            }
        }
    }
#endif
}

