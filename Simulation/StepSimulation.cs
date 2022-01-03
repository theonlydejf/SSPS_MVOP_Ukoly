using System;

namespace Simulation
{
    /// <summary>
    /// A simulation, that has a constant delay between each step.
    /// </summary>
    public class ConstantStepSimulation : Simulation
    {
        /// <summary>
        /// Creagtes an instance of <see cref="ConstantStepSimulation"/>
        /// </summary>
        /// <param name="world">A world that will be simulated</param>
        /// <param name="stepLength">A time between each step (smaller value = more precision)</param>
        /// <param name="simulationLength">A total length of simulation</param>
        public ConstantStepSimulation(World world, TimeSpan stepLength, TimeSpan simulationLength) : base(world)
        {
            this.stepLength = stepLength;
            this.simulationLength = simulationLength;
        }

        private readonly TimeSpan stepLength;
        private readonly TimeSpan simulationLength;

        protected override void DoStep()
        {
            world.DoStep(stepLength);
            TimeSinceEpoch += stepLength;

            if (TimeSinceEpoch > simulationLength)
                StopSimulation();
        }
    }
}
