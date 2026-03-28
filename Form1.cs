using EventMod.Objects;

namespace EventMod
{
    public partial class Form1 : Form
    {
        MyRectangle myRect;
        List<BaseObject> objects = new();
        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(0, 0, 0);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            g.Transform = myRect.GetTransform();

            myRect.Render(g);
        }
    }
}
