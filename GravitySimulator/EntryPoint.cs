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
            string startStateCSV = File.ReadAllText(args[1]);
            string endStateCSV = File.ReadAllText(args[2]);


        }

        /// <summary>
        /// Generate a list of SystemBody instances from a CSV string.
        /// </summary>
        /// <param name="inputCSV">The input CSV string.</param>
        /// <returns>A list of SystemBody instances.</returns>
        public static List<SystemBody> BodyListFromCSV(string inputCSV)
        {
            List<SystemBody> bodyList = new List<SystemBody>();
            string[] lineSplitCSV = inputCSV.Split('\n');

            foreach(string line in lineSplitCSV)
            {
                string[] commaSplitCSV = line.Split(',');
                string name = commaSplitCSV[0];
                double mass = double.Parse(commaSplitCSV[1], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);

                double x = double.Parse(commaSplitCSV[2], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
                double y = double.Parse(commaSplitCSV[3], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
                double z = double.Parse(commaSplitCSV[4], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);

                double vx = double.Parse(commaSplitCSV[5], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
                double vy = double.Parse(commaSplitCSV[6], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);
                double vz = double.Parse(commaSplitCSV[7], NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);

                SystemBody systemBody = new SystemBody(name, mass, x, y, z, vx, vy, vz);
                bodyList.Add(systemBody);
            }

            return bodyList;
        }
    }
}
