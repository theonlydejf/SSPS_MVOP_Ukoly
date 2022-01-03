using System;
using System.Collections.Generic;
using System.Numerics;

namespace Simulation
{
    /// <summary>
    /// An object used to hold world objects(see <see cref="World.Object"/>)
    /// </summary>
    public class World : ICloneable
    {
        /// <summary>
        /// Handler used to handle a step event
        /// </summary>
        /// <param name="sender">Object that invoked the step</param>
        /// <param name="e">Instance of <see cref="StepEventArgs"/> describing the step</param>
        public delegate void StepEventHandler(object sender, StepEventArgs e);

        /// <summary>
        /// Creates a new world
        /// </summary>
        public World()
        {
            WorldObjects = new List<Object>();
        }

        /// <summary>
        /// Event, which gets invoked when a new step is being taken
        /// </summary>
        public event StepEventHandler Step;

        private readonly List<Object> WorldObjects;

        /// <summary>
        /// Takes a simulation step
        /// </summary>
        /// <param name="deltaTime">A time between this step and a previous step</param>
        public void DoStep(TimeSpan deltaTime)
        {
            Step?.Invoke(this, new StepEventArgs() { DeltaTime = deltaTime });
        }

        public object Clone()
        {
            World clone = new World();
            WorldObjects.ForEach(obj => obj.CloneToWorld(clone));
            return clone;
        }

        /// <summary>
        /// Find a objects by its ID
        /// </summary>
        /// <param name="id">ID of the object</param>
        /// <returns>An instance of the object from this world</returns>
        public Object FindObject(Guid id) => WorldObjects.Find(x => x.ID.Equals(id));
        /// <summary>
        /// Gets object by its index. Indexes are being asigned by order the objects were binded to the world.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Object GetObjectByIndex(int index) => WorldObjects[index];

        /// <summary>
        /// Abstract class used to define a world object
        /// </summary>
        public abstract class Object
        {
            /// <summary>
            /// Creates instance of a world object
            /// </summary>
            /// <param name="world">An instance of <see cref="World"/> in which the point exists and is bound to</param>
            /// <param name="location">The location in relation to the origin</param>
            public Object(World world, Vector2 location)
            {
                world.Step += Step;
                world.WorldObjects.Add(this);
                Location = location;
                id = Guid.NewGuid();
            }

            /// <summary>
            /// The location in relation to the origin
            /// </summary>
            public Vector2 Location { get; set; }

            private Guid id;

            /// <summary>
            /// A unique ID od the world object
            /// </summary>
            public Guid ID => id;

            /// <summary>
            /// A method which is called when a step is being taken
            /// </summary>
            /// <param name="sender">Object that invoked the step</param>
            /// <param name="e">Instance of <see cref="StepEventArgs"/> describing the step</param>
            protected abstract void Step(object sender, StepEventArgs e);

            /// <summary>
            /// Clones this world object to a different instance of <see cref="World"/> and binds it to that world
            /// </summary>
            /// <param name="world">An instance of <see cref="World"/> the object is being cloned to</param>
            /// <returns>A cloned instance of <see cref="World.Object"/></returns>
            public abstract Object CloneToWorld(World world);
        }
    }
}
