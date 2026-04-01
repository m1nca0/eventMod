using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace EventMod.Objects
{
    internal class MyEllipse : BaseObject
    {
        public int lifeTime = 100;
        public Action<MyEllipse> OnDeath;
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
            if (lifeTime > 0)
            {
                g.FillEllipse(new SolidBrush(Color.LightGreen), -1 * (lifeTime / 2), -1 * (lifeTime / 2), lifeTime, lifeTime);
                lifeTime--;
            }
            else
            {
                if(OnDeath != null)
                {
                    OnDeath(this);
                }
            }
        }
        public void Relocation()
        {
            lifeTime = 100;
            
        }
    }
}
