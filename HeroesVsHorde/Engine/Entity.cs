using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HeroesVsHorde.Engine
{
    /// <summary>
    /// Doesn't actually have an update method.
    /// The manager autoupdates, by updating every controller, which makes
    /// it easier for threading (I think!)
    /// </summary>
    class Entity
    {
        /// <summary>
        /// The string is a unique identifier for the type of controller, which lets
        /// controllers interact really easily.
        /// </summary>
        protected Dictionary<string, Controller> controllers;
        public Entity(IEnumerable<Controller> controllers)
        {
            foreach (Controller c in controllers)
                this.controllers[c.Type] = c;
        }
        public Entity()
        {
            this.controllers = new Dictionary<string, Controller>();
        }
    }
}
