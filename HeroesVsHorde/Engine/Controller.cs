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
    abstract class Controller
    {
        public abstract string Type{get; }

        protected Entity ent;

        public Controller(Entity ent)
        {
            this.ent = ent;
        }

        public Controller() { }

        public abstract void UpdateEnt(GameTime gameTime);
    }

    abstract class DrawController : Controller
    {
        /// <summary>
        /// Defines the order in which this has its Draw Method called.
        /// Draw orders to not have to be consecutive.
        /// </summary>
        public int DrawOrder;
        public abstract void Draw(GameTime gameTime);
    }
}
