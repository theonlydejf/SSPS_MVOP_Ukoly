using System;

namespace Simulation
{
    /// <summary>
    /// Class that holds data about the current step
    /// </summary>
    public class StepEventArgs : EventArgs
    {
        /// <summary>
        /// Time, between the current step and the previous step
        /// </summary>
        public TimeSpan DeltaTime { get; set; }
    }
}
