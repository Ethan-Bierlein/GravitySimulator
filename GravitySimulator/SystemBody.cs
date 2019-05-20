using System;

namespace GravitySimulator
{
    /// <summary>
    /// This struct is simply a wrapper for important information associated with a 
    /// body and any methods required for modifying that information; all actual 
    /// simulation is done in the 'System' class.
    /// </summary>
    public struct SystemBody
    {
        public string Name { get; set; }
        public double Mass { get; set; }

        private double X { get; set; }
        private double Y { get; set; }
        private double Z { get; set; }

        private double VX { get; set; }
        private double VY { get; set; }
        private double VZ { get; set; }

        /// <summary>
        /// Constructor for the SystemBody struct.
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
        public void IntegratePosition()
        {
            this.X += this.VX;
            this.Y += this.VY;
            this.VZ += this.VZ;
        }
    }
}
