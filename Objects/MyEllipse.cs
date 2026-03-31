using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace EventMod.Objects
{
    internal class MyEllipse : BaseObject
    {

        public MyEllipse(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.LightGreen), -25, -25, 50, 50);
            g.DrawEllipse(new Pen(Color.Green, 2), -25, -25, 50, 50);
        }
    }
}
