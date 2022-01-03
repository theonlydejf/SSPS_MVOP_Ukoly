using System;
namespace Simulation
{
    /// <summary>
    /// Abstract class used to define so called watchers. Watchers are objects
    /// that can analyze a world every simulation step. For example a watcher
    /// can look for a closest encounter of two objects.
    /// </summary>
    public abstract class SimulationWatcher : IDisposable
    {
        /// <summary>
        /// Creates an instance of watcher, that analyzes a specific simulation
        /// </summary>
        /// <param name="simulation">Instance of <see cref="Simulation"/>, that the watcher watches</param>
        public SimulationWatcher(Simulation simulation)
        {
            simulation.Stepped += Simulation_Stepped;
            this.simulation = simulation;
        }

        /// <summary>
        /// Simulation which is watched by this watcher
        /// </summary>
        protected readonly Simulation simulation;

        /// <summary>
        /// Gets invoked after every step in the watched simulation
        /// </summary>
        /// <param name="sender">Instance of the object that invoked the step</param>
        /// <param name="e">Instance of <see cref="SteppedEventArgs"/> describing the state of the simulated world after the step</param>
        protected abstract void Simulation_Stepped(object sender, SteppedEventArgs e);

        /// <summary>
        /// Releases the watcher from the simulation
        /// </summary>
        public void Dispose() => simulation.Stepped -= Simulation_Stepped;
    }
}
