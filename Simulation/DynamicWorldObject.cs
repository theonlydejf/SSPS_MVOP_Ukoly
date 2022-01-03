using System;
using System.Numerics;

namespace Simulation
{
    /// <summary>
    /// A point in a world that can move. That means this object is also defined by <see cref="Velocity"/>, current <see cref="Acceleration"/> and its <see cref="Mass"/>
    /// </summary>
    public class DynamicWorldPoint : World.Object
    {
        /// <summary>
        /// Creates an instance of <see cref="DynamicWorldPoint"/> and binds it with a specific <see cref="World"/>
        /// </summary>
        /// <param name="world">An instance of <see cref="World"/> in which the point exists and is bound to</param>
        /// <param name="location">The location in relation to the origin in the <see cref="World"/> that the point is bound to</param>
        /// <param name="mass">The mass of the point</param>
        public DynamicWorldPoint(World world, Vector2 location, float mass) : this(world, location, mass, new Vector2(0))
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="DynamicWorldPoint"/> with a specififc starting velocity and binds it with a specific <see cref="World"/>
        /// </summary>
        /// <param name="world">An instance of <see cref="World"/> in which the point exists and is bound to</param>
        /// <param name="location">The location in relation to the origin in the <see cref="World"/> that the point is bound to</param>
        /// <param name="mass">The mass of the point</param>
        /// <param name="velocity">A vector defining the points speed and direction in which it is travelling</param>
        public DynamicWorldPoint(World world, Vector2 location, float mass, Vector2 velocity) : base(world, location)
        {
            Velocity = velocity;
            Mass = mass;
        }

        /// <summary>
        /// A vector defining current acceleration and direction, at which the point is accelerating
        /// </summary>
        public Vector2 Acceleration { get; private set; }
        /// <summary>
        /// A vector defining the points speed and direction in which it is travelling
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// A mass of the point
        /// </summary>
        public float Mass { get; }

        /// <summary>
        /// A speed at which the point is travelling
        /// </summary>
        public float Speed => Velocity.Length();
        /// <summary>
        /// A direction at wihch the point is travelling
        /// </summary>
        public double Direction => Math.Atan2(Velocity.Y, Velocity.X);

        /// <summary>
        /// Starts applying a force to the point
        /// </summary>
        /// <param name="force">A vector defining a force and a direction in which it is applied</param>
        public void StartApplyingForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }

        /// <summary>
        /// Stops all forces, that are currently being applied on the point
        /// </summary>
        public void StopApplyingForce()
        {
            Acceleration = new Vector2(0);
        }

        public override World.Object CloneToWorld(World world)
        {
            DynamicWorldPoint clone = new DynamicWorldPoint(world, Location, Mass, Velocity);
            clone.Acceleration = Acceleration;
            return clone;
        }

        protected override void Step(object sender, StepEventArgs e)
        {
            Location += Velocity * (float)e.DeltaTime.TotalSeconds;
            Velocity += Acceleration * (float)e.DeltaTime.TotalSeconds;
        }
    }
}
