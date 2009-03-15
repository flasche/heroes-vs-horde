using System;

namespace HeroesVsHorde
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HeroesVsHordeGame game = new HeroesVsHordeGame())
            {
                game.Run();
            }
        }
    }
}

