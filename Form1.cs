using EventMod.Objects;

namespace EventMod
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;

        MyEllipse enemy1;
        MyEllipse enemy2;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnEllipseOverlap += (m) =>
            {
                objects.Remove(m);
                OverTime();
                count++;
                txtCount.Text = "Очки: " + count;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            objects.Add(marker);
            objects.Add(player);
        }
        public void OverTime()
        {
            var rnd = new Random();
            enemy1 = new MyEllipse(rnd.Next(30, 530), rnd.Next(30, 450), 0);
            objects.Add(enemy1);
            enemy1.OnDeath += (en) =>
            {
                OverTime();
                objects.Remove(en);
            };
        }
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlaer();
            foreach (var obj in objects.ToList())
            {

                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
     
            foreach (var obj in objects.ToList())
            {
                g.Transform = obj.GetTransform();
                obj.Render(g, count);
            }
            int ellipseCount = objects.OfType<MyEllipse>().Count();
            while (ellipseCount < 10)
            {
                OverTime();
                ellipseCount = objects.OfType<MyEllipse>().Count();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }
        private void updatePlaer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float lenght = MathF.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.X = e.X; marker.Y = e.Y;
        }
    }
}
