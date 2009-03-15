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
    class Controller
    {
        protected Entity ent;

        public Controller(Entity ent)
        {
            this.ent = ent;
        }

        public virtual void UpdateEnt(GameTime gameTime);
    }

    class DrawController : Controller
    {
        /// <summary>
        /// Defines the order in which this has its Draw Method called.
        /// Draw orders to not have to be consecutive.
        /// </summary>
        public int DrawOrder;
        public virtual void Draw(GameTime gameTime);
    }
}
