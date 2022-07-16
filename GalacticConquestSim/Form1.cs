using System.Diagnostics;
using System.Drawing.Imaging;

namespace GalacticConquestSim
{
    public partial class Form1 : Form
    {
        DirectBitmap drawarea;
        Universe board;
        int pixelsize = 1;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            Setup();

            timer.Start();
        }

        private void Setup()
        {
            drawarea = new DirectBitmap(picturebox.Size.Width, picturebox.Size.Height);
            picturebox.Image = drawarea.Bitmap;

            board = new Universe(picturebox.Size.Width, picturebox.Size.Height, pixelsize, 50, 500);
            board.DrawBoard(drawarea, picturebox);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            board.Step();
            board.DrawBoard(drawarea, picturebox);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                board.Step();
                board.DrawBoard(drawarea, picturebox);
            }

            if (e.KeyChar == 's')
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    board.SaveBoard().Save(dialog.FileName, ImageFormat.Bmp);
                }
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Setup();
        }

        private void speed_numeric_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = (int)speed_numeric.Value;
        }

        private void pixelsize_numeric_ValueChanged(object sender, EventArgs e)
        {
            pixelsize = (int)pixelsize_numeric.Value;
            Setup();
        }
    }
}