using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace GravitySimulator
{
    public class EntryPoint
    {
        /// <summary>
        /// Simulation entry point.
        /// </summary>
        /// <param name="args">
        /// Command line arguments; the first is the path to the starting state CSV file, the second 
        /// is the path to the ending state CSV file.
        /// </param>
        public static void Main(string[] args)
        {
            string initialStateCSV = File.ReadAllText(args[0]);
            string idealEndStateCSV = File.ReadAllText(args[1]);
            double dt = double.Parse(args[2], NumberStyles.AllowDecimalPoint);

            List<SystemBody> initialState = BodyListFromCSV(initialStateCSV);
            List<SystemBody> idealEndState = BodyListFromCSV(idealEndStateCSV);

            System system = new System(initialState, idealEndState, dt);
            system.Integrate();
            system.AdjustBarycenter();

            foreach(SystemBody body in system.CurrentState)
            {
                SystemBody realBody = system.IdealEndState.Where(m => m.Name == body.Name).ToList()[0];
                Console.WriteLine($"{body.Name},{body.X},{body.Y},{body.Z}");
                Console.WriteLine($"{realBody.Name},{realBody.X},{realBody.Y},{body.Z}");
            }

            BodyListToCSV(system);
        }

        /// <summary>
        /// Write the final simulation data to an output CSV file. The output file will be overwritten for
        /// each simulation with an identical timestep.
        /// </summary>
        /// <param name="system">The simulated system.</param>
        public static void BodyListToCSV(System system)
        {
            List<string> csv = new List<string>();
            List<SystemBody> simulatedBodies = system.CurrentState.OrderBy(body => body.Name).ToList();
            List<SystemBody> realBodies = system.IdealEndState.OrderBy(body => body.Name).ToList();

            for(int i = 0; i < simulatedBodies.Count; i++)
            {
                SystemBody simulatedBody = simulatedBodies[i];
                SystemBody realBody = realBodies[i];

                double errorX = Math.Abs((simulatedBody.X - realBody.X) / realBody.X) * 100;
                double errorY = Math.Abs((simulatedBody.Y - realBody.Y) / realBody.Y) * 100;
                double errorZ = Math.Abs((simulatedBody.Z - realBody.Z) / realBody.Z) * 100;
                double errorDisplacement = (errorX + errorY + errorZ) / 3.0;

                double errorVX = Math.Abs((simulatedBody.VX - realBody.VX) / realBody.VX) * 100;
                double errorVY = Math.Abs((simulatedBody.VY - realBody.VY) / realBody.VY) * 100;
                double errorVZ = Math.Abs((simulatedBody.VZ - realBody.VZ) / realBody.VZ) * 100;
                double errorVelocity = (errorVX + errorVY + errorVZ) / 3.0;

                double trueError = (errorDisplacement + errorVelocity) / 2.0;
                csv.Add($"{simulatedBody.Name},{errorDisplacement},{errorVelocity},{trueError}\n");
            }

            foreach(SystemBody body in simulatedBodies)
            {
                csv.Add($"{body.Name},{body.X},{body.Y},{body.Z},{body.VX},{body.VY},{body.VZ}\n");
            }

            File.WriteAllLines($"output_{system.DT}dt.csv", csv.ToArray());
        }

        /// <summary>
        /// Generate a list of SystemBody instances from a CSV string. This method will convert positions and
        /// velocities from KM and KM/s to M and M/s.
        /// </summary>
        /// <param name="inputCSV">The input CSV string.</param>
        /// <returns>A list of SystemBody instances.</returns>
        public static List<SystemBody> BodyListFromCSV(string inputCSV)
        {
            List<SystemBody> bodyList = new List<SystemBody>();
            string[] lineSplitCSV = inputCSV.Split('\n');

            foreach(string line in lineSplitCSV)
            {
                NumberStyles numberStyle = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
                string[] commaSplitCSV = line.Trim('\n').Trim('\r').Split(',');

                string name = commaSplitCSV[0];
                double mass = double.Parse(commaSplitCSV[1], numberStyle);

                double x = double.Parse(commaSplitCSV[2], numberStyle) * 1000;
                double y = double.Parse(commaSplitCSV[3], numberStyle) * 1000;
                double z = double.Parse(commaSplitCSV[4], numberStyle) * 1000;

                double vx = double.Parse(commaSplitCSV[5], numberStyle) * 1000;
                double vy = double.Parse(commaSplitCSV[6], numberStyle) * 1000;
                double vz = double.Parse(commaSplitCSV[7], numberStyle) * 1000;

                SystemBody systemBody = new SystemBody(name, mass, x, y, z, vx, vy, vz);
                bodyList.Add(systemBody);
            }

            return bodyList;
        }
    }
}
