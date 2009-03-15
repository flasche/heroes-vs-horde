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
        protected List<Controller> controllers;
        /// <summary>
        /// The int is the draw level, which makes it easier to draw things in order.
        /// </summary>
        protected Dictionary<int, List<DrawController>> drawControllers;
        protected List<Entity> ents;

        public sealed void Draw(GameTime gameTime)
        {
            List<DrawController> dcCurrents;
            ManualResetEvent[] mrEvents;
            foreach (int key in drawControllers.Keys.Sort())
            {
                dcCurrents = drawControllers[key];
                mrEvents = new ManualResetEvent[dcCurrents.Count];
                for (int i = 0; i < mrEvents.Length; i++)
                    mrEvents[i] = new ManualResetEvent(false);

                for (int i = 0; i < dcCurrents.Count; i++)
                {
                    ThreadPool.QueueUserWorkItem(
                        new WaitCallback(
                            (object o) =>
                            {
                                dcCurrents[i].Draw((GameTime)o);
                                mrEvents[i].Set();
                            }
                    ), (object)gameTime); //compiler should make temp variable
                    //for gametime, so its not cast over and over and over again
                    //can probably just pull in gameTime from the scope, but
                    //don't wanna try it just yet!
                }
                WaitHandle.WaitAll(mrEvents); //Lets the current layer finish
                //drawing before the next happens. Hope it works!!
            }
        }

        public sealed void Update(GameTime gameTime)
        {
            var mrEvents = new ManualResetEvent[ents.Count];
            for (int i = 0; i < mrEvents.Length; i++)
                mrEvents[i] = new ManualResetEvent(false);

            for (int i = 0; i < controllers.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(
                        (object o) =>
                        {
                            controllers[i].UpdateEnt((GameTime)o);
                            mrEvents[i].Set();
                        }
                ));
            }
            WaitHandle.WaitAll(mrEvents);
        }        
    }
}
   