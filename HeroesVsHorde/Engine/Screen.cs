using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroesVsHorde.Engine
{
    class Screen
    {
        /// <summary>
        /// Do not add directly to this. Read it, fine, but use the
        /// Register Controller method instead.
        /// 
        /// Why Am I doing this - do you think I should?
        /// </summary>
        public List<IController> Controllers;

        /// <summary>
        /// Do not add directly to this. Read it, fine, but use the
        /// Register Controller method instead.
        /// 
        /// Why Am I doing this - do you think I should?
        /// </summary>
        public List<DrawController> DrawControllers;

        /*TODO: (Optimisation) Would this run faster with controllers[i] as a local
         * variable. I'm assuming it already optimises to this, but I'm not sure.
         */
        public void RegisterControllers(params IController[] controllers)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                this.Controllers.Add(controllers[i]);
                if (controllers[i] is DrawController)
                    DrawControllers.Add((DrawController)controllers[i]);
            }
            DrawControllers.Sort(DrawControllerComparer.Instance);
        }

        public void RemoveControllers(params IController[] controllers)
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                this.Controllers.Remove(controllers[i]);
                if (controllers[i] is DrawController)
                    DrawControllers.Remove((DrawController)controllers[i]);
            }
        }
    }
}
