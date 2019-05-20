using System;
using System.IO;

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
    }
}
