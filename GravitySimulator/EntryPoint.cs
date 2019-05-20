using System;
using System.IO;
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

            List<SystemBody> initialState = BodyListFromCSV(initialStateCSV);
            List<SystemBody> idealEndState = BodyListFromCSV(idealEndStateCSV);
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
