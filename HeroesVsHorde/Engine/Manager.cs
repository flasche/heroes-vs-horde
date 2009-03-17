using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeroesVsHorde.Engine
{
    /// <summary>
    /// This runs the show: it should be subclassed to make your game
    /// </summary>
    class Manager
    {
        private class ThreadDataStore
        {
            public GameTime GT;
            public int Count;
        }

        private class DrawThreadDataStore : ThreadDataStore
        {
            public DrawController currentDC;
        }


        public Screen CurrentScreen;

        /// <summary>
        /// Call this from your games Draw method
        /// Draws the code to the screen. Not designed to be overriden.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            /* TODO: Replace the main loop here with some sort of while loop
             * meaning DrawControllers doesn't get accessed twice.
             */
            int currentLayer = CurrentScreen.DrawControllers[0].DrawOrder;
            DrawController currentDC;

            //TODO: This is space wasteful - there should be a way of improving it.
            var mrEvents = new ManualResetEvent[CurrentScreen.DrawControllers.Count];
            for (int i = 0; i < mrEvents.Length; i++)
                mrEvents[i] = new ManualResetEvent(false);

            for (int i = 0; i < CurrentScreen.DrawControllers.Count; i++)
            {
                currentDC = CurrentScreen.DrawControllers[i];
                if (currentDC.DrawOrder != currentLayer)
                    WaitHandle.WaitAll(mrEvents); //Let the current layer draw

                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(
                        (object o) =>
                        {
                            var Data = (ThreadDataStore)o;
                            currentDC.Draw(Data.GT);
                            mrEvents[Data.Count].Set();
                        }
                ), (object)new ThreadDataStore{GT = gameTime, Count = i}); //compiler should make temp variable
                //for gametime, so its not cast over and over and over again
                //can probably just pull in gameTime from the scope, but
                //don't wanna try it just yet!
            }

            WaitHandle.WaitAll(mrEvents); //Finish drawing before finishing.
        }

        /// <summary>
        /// Call this from your games Update method
        /// Updates all controllers. Not designed to be overridde
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            List<IController> controllers = CurrentScreen.Controllers;
            var mrEvents = new ManualResetEvent[controllers.Count];
            for (int i = 0; i < mrEvents.Length; i++)
                mrEvents[i] = new ManualResetEvent(false);

            for (int i = 0; i < controllers.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(
                        (object o) =>
                        {
                            var Data = (ThreadDataStore)o;
                            controllers[Data.Count].UpdateEnt(Data.GT);
                            mrEvents[Data.Count].Set();
                        }
                ), (object)new ThreadDataStore { GT = gameTime, Count = i });
            }
            WaitHandle.WaitAll(mrEvents);
        }
    }
}
   