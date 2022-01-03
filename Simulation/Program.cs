using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace Simulation
{
    class Program
    {
        /*
         * POINT1
         *   v
         *   |
         *   |
         *   |
         *   |
         *   |
         *   \----------- < POINT2
         */

        /// <summary>
        /// Distance of Point1 from the origin of the right angle
        /// </summary>
        public const float s1 = 15;
        /// <summary>
        /// Distance of Point2 from the origin of the right angle
        /// </summary>
        public const float s2 = 10;

        /// <summary>
        /// The size of velocity vector used by Point1 that is heading to the origin of the right angle
        /// </summary>
        public const float v1 = 15;
        /// <summary>
        /// The size of velocity vector used by Point2 that is heading to the origin of the right angle
        /// </summary>
        public const float v2 = 20;

        /// <summary>
        /// The mass of Point1
        /// </summary>
        public const float m1 = 10;
        /// <summary>
        /// The mass of Point2
        /// </summary>
        public const float m2 = 20;

        /// <summary>
        /// The acceleration used by Point1 (used only by the acceleration simulation)
        /// </summary>
        public const float a1 = 2;
        /// <summary>
        /// The acceleration used by Point2 (used only by the acceleration simulation)
        /// </summary>
        public const float a2 = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating worlds...");
            // Create seperate worlds for each simulation
            World constVelWorld = new World();
            var constPoint1 = new DynamicWorldPoint(constVelWorld, new Vector2(0, s1), m1, new Vector2(0, -v1));
            var constPoint2 = new DynamicWorldPoint(constVelWorld, new Vector2(s2, 0), m2, new Vector2(-v2, 0));

            World accelVelWorld = new World();
            var accelPoint1 = new DynamicWorldPoint(accelVelWorld, new Vector2(0, s1), m1, new Vector2(0, -v1));
            accelPoint1.StartApplyingForce(new Vector2(0, -(a1 * m1)));
            var accelPoint2 = new DynamicWorldPoint(accelVelWorld, new Vector2(s2, 0), m2, new Vector2(-v2, 0));
            accelPoint2.StartApplyingForce(new Vector2(-(a2 * m2), 0));

            Console.WriteLine("Creating simulations...");
            Simulation constSimulation = new ConstantStepSimulation(constVelWorld, TimeSpan.FromMilliseconds(1), TimeSpan.FromSeconds(10));
            Simulation accelSimulation = new ConstantStepSimulation(accelVelWorld, TimeSpan.FromMilliseconds(1), TimeSpan.FromSeconds(10));

            // Run the simulations
            Console.WriteLine("Constant speed simulation:");
            var closestConstEncounter = FindClosestEncounter(constSimulation, constPoint1, constPoint2, true);
            Console.WriteLine("Constant speed simulation:");
            var closestAccelEncounter = FindClosestEncounter(accelSimulation, accelPoint1, accelPoint2, true);

            // Print the results
            Console.WriteLine($"Closest encounter with constnant speed happend { closestConstEncounter.Timestamp.TotalSeconds.ToString("0.000") } seconds after the epoch. Points were { closestConstEncounter.Distance }m apart from eachother.");
            Console.WriteLine($"Closest encounter when accelerating happend { closestAccelEncounter.Timestamp.TotalSeconds.ToString("0.000") } seconds after the epoch. Points were { closestAccelEncounter.Distance }m apart from eachother.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Runs the simulation and records closest encounter of two objects
        /// </summary>
        /// <param name="simulation">The simulation in which the objects are simulated</param>
        /// <param name="obj1">First object that should be watched</param>
        /// <param name="obj2">Second object that should be watched</param>
        /// <returns>Instance of <see cref="Encounter"/> describing the closest encounter</returns>
        public static Encounter FindClosestEncounter(Simulation simulation, DynamicWorldPoint obj1, DynamicWorldPoint obj2, bool reportTime = false)
        {
            Encounter closest;
            using (var watcher = new ClosestEncounterWatcher(simulation, obj1, obj2))
            {
                Console.WriteLine("Simulating...");
                Console.Write("Seconds Processed: ");
                if(reportTime)
                    simulation.Stepped += Simulation_ReportTime;

                simulation.Simulate();
                Console.WriteLine();
                closest = watcher.CurrentClosestEncounter;
            }

            if (reportTime)
                simulation.Stepped -= Simulation_ReportTime;

            return closest;

            static void Simulation_ReportTime(object sender, SteppedEventArgs e)
            {
                int x = Console.CursorLeft, y = Console.CursorTop;
                Console.Write(e.TimeSinceEpoch.TotalSeconds.ToString("0.000"));
                Console.SetCursorPosition(x, y);
            }
        }
    }
}
