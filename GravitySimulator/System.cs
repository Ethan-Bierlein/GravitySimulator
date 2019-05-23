using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace GravitySimulator
{
    /// <summary>
    /// This class is responsible for simulation a whole system of gravitational bodies. In this
    /// specific instance, the gravitationally significant bodies of the solar system. This class
    /// can also determine simulation accuracy, given a set of real-world ending data.
    /// 
    /// The provided real world data are provided from two sets of CSV values at two distinct time 
    /// values: the 1st of January, 2019 to the 1st of May, 2019 (120 days -> 10368000 seconds); 
    /// both datasets are timestamped at 0:00 UTC.
    /// </summary>
    public class System
    {
        public const double G = 0.0000000000667408;
        public const double SIMULATION_LENGTH_SECONDS = 10368000;

        public List<SystemBody> CurrentState { get; set; }
        public List<SystemBody> IdealEndState { get; set; }
        public double DT { get; set; }
        public int Iterations { get; set; }

        /// <summary>
        /// Constructor for the System class.
        /// </summary>
        /// <param name="initialState">The initial state of the system.</param>
        /// <param name="idealEndState">The "ideal" end state of the system.</param>
        /// <param name="dt">The timestep of the simulation in seconds.</param>
        public System(List<SystemBody> initialState, List<SystemBody> idealEndState, double dt)
        {
            this.CurrentState = initialState;
            this.IdealEndState = idealEndState;
            this.DT = dt;
            this.Iterations = (int)Math.Round(SIMULATION_LENGTH_SECONDS / this.DT);
        }

        /// <summary>
        /// Integrate the system for a set number of cycles, determined by the fixed amount of time between data
        /// sets and the simulation timestep (i.e., length / dt). The steps of integration for each single iteration
        /// are as follows:
        /// 
        ///   1. Iterate over all bodies in the current state and integrate their positions.
        ///   2. Iterate over all bodies in the current state and calculate their new velocities.
        /// </summary>
        public void Integrate()
        {
            for(int n = 0; n <= this.Iterations; n++)
            {
                foreach(SystemBody body in this.CurrentState)
                {
                    body.IntegratePosition(this.DT);
                }

                foreach(SystemBody body in this.CurrentState)
                {
                    foreach(SystemBody m in this.CurrentState)
                    {
                        if(m.Name == body.Name)
                        {
                            continue;
                        }

                        double d = body.DistanceBetween(m);
                        double adx = body.DistanceBetweenX(m) / d;
                        double ady = body.DistanceBetweenY(m) / d;
                        double adz = body.DistanceBetweenZ(m) / d;

                        double a = (G * m.Mass) / Math.Pow(body.DistanceBetween(m), 2) * this.DT;
                        double ax = a * adx;
                        double ay = a * ady;
                        double az = a * adz;

                        body.VX += ax;
                        body.VY += ay;
                        body.VZ += az;
                    }
                }

                Console.WriteLine($"{n} / {this.Iterations}");
            }
        }

        /// <summary>
        /// Calculate the location of the new system barycenter (a simple center of mass calculation) and re-adjust
        /// all positional for each body to be relative to the location of the new barycenter. This is done so that
        /// the simulated data is compatible with the JPL's HORIZON data, which generates positions using the solar
        /// system barycenter as a reference frame.
        /// </summary>
        public void AdjustBarycenter()
        {
            double systemMass = this.CurrentState.Sum(body => body.Mass);
            double centerX = this.CurrentState.Sum(body => body.Mass * body.X) / systemMass;
            double centerY = this.CurrentState.Sum(body => body.Mass * body.Y) / systemMass;
            double centerZ = this.CurrentState.Sum(body => body.Mass * body.Z) / systemMass;

            foreach(SystemBody body in this.CurrentState)
            {
                body.SetPosition(
                    body.X - centerX,
                    body.Y - centerY,
                    body.Z - centerZ
                );
            }
        }
    }
}
