using System;

namespace Simulation
{
    /// <summary>
    /// A base class used to define how world is simulated
    /// </summary>
    public abstract class Simulation
    {
        /// <summary>
        /// Handler used to handle a stepped event
        /// </summary>
        /// <param name="sender">Object that invoked the step</param>
        /// <param name="e">Instance of <see cref="SteppedEventArgs"/> describing the state of the simulated world after a step</param>
        public delegate void SteppedEventHandler(object sender, SteppedEventArgs e);

        public Simulation(World world)
        {
            this.world = world;
        }

        /// <summary>
        /// Event, which gets invoked every time a step has been completed
        /// </summary>
        public event SteppedEventHandler Stepped;

        protected World world;

        private bool cancellationRequested = false;

        public void StopSimulation() => cancellationRequested = true;

        public TimeSpan TimeSinceEpoch { get; protected set; }

        public void Simulate()
        {
            // First step (before updating any world objects)
            HandleStepped();

            while (!cancellationRequested)
            {
                DoStep();
                HandleStepped();
            }
        }

        private void HandleStepped()
        {
            Stepped?.Invoke(this, new SteppedEventArgs()
            {
                WorldSnapshot = world.Clone() as World,
                TimeSinceEpoch = TimeSinceEpoch
            });
        }

        protected abstract void DoStep();
    }
}
