using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace EventMod.Objects
{
    internal class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;

        public BaseObject(float x, float y, float angle)
        {
            this.X = x;
            this.Y = y;
            this.Angle = angle;
        }
        public Matrix GetTransform()
        {
            var matrix = new Matrix(); 
            matrix.Translate(X,Y);
            matrix.Rotate(Angle);

            return matrix;
        }
        public virtual void Render(Graphics g)
        {

        }
    }
}
