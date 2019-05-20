using System;
using System.Linq;
using System.Collections.Generic;

namespace GravitySimulator
{
    /// <summary>
    /// This class is responsible for simulation a whole system of gravitational bodies. In this
    /// specific instance, the gravitationally significant bodies of the solar system. This class
    /// can also determine simulation accuracy, given a set of real-world ending data.
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
        public void IntegrateSystem()
        {
            for(int n = 0; n <= this.Iterations; n++)
            {
                foreach(SystemBody body in this.CurrentState)
                {
                    body.IntegratePosition();
                }

                foreach(SystemBody body in this.CurrentState)
                {
                    double vx = (G / this.DT) * this.CurrentState.Where(obj => obj != body).Sum(obj => obj.Mass / Math.Pow(body.DistanceBetweenX(obj), 2));
                    double vy = (G / this.DT) * this.CurrentState.Where(obj => obj != body).Sum(obj => obj.Mass / Math.Pow(body.DistanceBetweenY(obj), 2));
                    double vz = (G / this.DT) * this.CurrentState.Where(obj => obj != body).Sum(obj => obj.Mass / Math.Pow(body.DistanceBetweenZ(obj), 2));

                    body.SetVelocity(vx, vy, vz);
                }
            }
        }
    }
}
