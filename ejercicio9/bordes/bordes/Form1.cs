using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace bordes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

      
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap originalImage = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = originalImage;

                    Bitmap edgeImage = DetectEdges(originalImage);
                    pictureBox2.Image = edgeImage;
                }
            }
        }

        private Bitmap DetectEdges(Bitmap original)
        {
            Bitmap edges = new Bitmap(original.Width, original.Height);

         
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int gray = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    edges.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

      
            for (int y = 1; y < edges.Height - 1; y++)
            {
                for (int x = 1; x < edges.Width - 1; x++)
                {
                    int gx = (edges.GetPixel(x - 1, y - 1).R + 2 * edges.GetPixel(x, y - 1).R + edges.GetPixel(x + 1, y - 1).R) -
                              (edges.GetPixel(x - 1, y + 1).R + 2 * edges.GetPixel(x, y + 1).R + edges.GetPixel(x + 1, y + 1).R);

                    int gy = (edges.GetPixel(x - 1, y - 1).R + 2 * edges.GetPixel(x - 1, y).R + edges.GetPixel(x - 1, y + 1).R) -
                              (edges.GetPixel(x + 1, y - 1).R + 2 * edges.GetPixel(x + 1, y).R + edges.GetPixel(x + 1, y + 1).R);

                    int g = (int)Math.Sqrt(gx * gx + gy * gy);
                    g = g > 255 ? 255 : g;

                    edges.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }

            return edges;
        }

     
    }
}
