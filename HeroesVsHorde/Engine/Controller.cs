using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeroesVsHorde.Engine
{
    /// <summary>
    /// Controls a certain aspect of entity behaviour
    /// </summary>
    interface IController
    {
        void UpdateEnt(GameTime gameTime);
    }

    abstract class DrawController : IController
    {
        /// <summary>
        /// Defines the order in which this has its Draw Method called.
        /// Draw orders to not have to be consecutive.
        /// </summary>
        public int DrawOrder;
        public abstract void Draw(GameTime gameTime);
        public abstract void UpdateEnt(GameTime gameTime);
    }

    class DrawControllerComparer : Comparer<DrawController>
    {
        public static DrawControllerComparer Instance = new DrawControllerComparer();
        public override int Compare(DrawController x, DrawController y)
        {
            if (x.DrawOrder == y.DrawOrder) return 0;
            return (x.DrawOrder > y.DrawOrder ? 1 : -1);
        }
    }

}
