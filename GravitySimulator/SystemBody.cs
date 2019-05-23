using System;

namespace GravitySimulator
{
    /// <summary>
    /// This class is simply a wrapper for important information associated with a 
    /// body and any methods required for modifying that information; all actual 
    /// simulation is done in the System class.
    /// </summary>
    public class SystemBody
    {
        public string Name { get; set; }
        public double Mass { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double VX { get; set; }
        public double VY { get; set; }
        public double VZ { get; set; }

        /// <summary>
        /// Constructor for the SystemBody class.
        /// </summary>
        /// <param name="name">The name of the body.</param>
        /// <param name="mass">The mass of the body.</param>
        /// <param name="x">The x position of the body.</param>
        /// <param name="y">The y position of the body.</param>
        /// <param name="z">The z position of the body.</param>
        /// <param name="vx">The x velocity of the body.</param>
        /// <param name="vy">The y velocity of the body.</param>
        /// <param name="vz">The z velocity of the body.</param>
        public SystemBody(string name, double mass, double x, double y, double z, double vx, double vy, double vz)
        {
            this.Name = name;
            this.Mass = mass;

            this.X = x;
            this.Y = y;
            this.Z = z;

            this.VX = vx;
            this.VY = vy;
            this.VZ = vz;
        }

        /// <summary>
        /// Set the position of the body to a new value.
        /// </summary>
        /// <param name="x">The new x position.</param>
        /// <param name="y">The new y position.</param>
        /// <param name="z">The new z position.</param>
        public void SetPosition(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Set the velocity of the body to a new value.
        /// </summary>
        /// <param name="vx">The new x velocity.</param>
        /// <param name="vy">The new y velocity.</param>
        /// <param name="vz">The new z velocity.</param>
        public void SetVelocity(double vx, double vy, double vz)
        {
            this.VX = vx;
            this.VY = vy;
            this.VZ = vz;
        }

        /// <summary>
        /// Integrate the position of the body based on the current velocity.
        /// </summary>
        /// <param name="dt">The timestep of the simulation.</param>
        public void IntegratePosition(double dt)
        {
            this.X += this.VX * dt;
            this.Y += this.VY * dt;
            this.Z += this.VZ * dt;
        }

        /// <summary>
        /// Calculate the distance between the current body and another body.
        /// </summary>
        /// <param name="other">The other body with which to determine distance.</param>
        /// <returns>The distance between the two bodies.</returns>
        public double DistanceBetween(SystemBody other)
        {
            return 
                Math.Sqrt(
                    Math.Pow(this.X - other.X, 2) +
                    Math.Pow(this.Y - other.Y, 2) +
                    Math.Pow(this.Z - other.Z, 2)
                );
        }

        /// <summary>
        /// Calculate the distance between the current body and another body with respect to
        /// the X component of position.
        /// </summary>
        /// <param name="other">The other body with which to determine distance.</param>
        /// <returns>The distance between the two bodies, with respect to the X component.</returns>
        public double DistanceBetweenX(SystemBody other)
        {
            return
                this.X - other.X;
        }

        /// <summary>
        /// Calculate the distance between the current body and another body with respect to
        /// the Y component of position.
        /// </summary>
        /// <param name="other">The other body with which to determine distance.</param>
        /// <returns>The distance between the two bodies, with respect to the Y component.</returns>
        public double DistanceBetweenY(SystemBody other)
        {
            return
                this.Y - other.Y;
        }

        /// <summary>
        /// Calculate the distance between the current body and another body with respect to
        /// the Z component of position.
        /// </summary>
        /// <param name="other">The other body with which to determine distance.</param>
        /// <returns>The distance between the two bodies, with respect to the Z component.</returns>
        public double DistanceBetweenZ(SystemBody other)
        {
            return
                this.Z - other.Z;
        }
    }
}
