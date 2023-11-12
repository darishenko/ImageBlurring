using System;
using System.Drawing;
using System.Windows.Forms;
using ImageBlurring.ImageBlur;
using ImageBlurring.ImageBlur.Impl;

namespace ImageBlurring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button_open_image_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            var filename = openFileDialog.FileName;

            var bitmap = new Bitmap(filename);
            pictureBox_source.Image = bitmap;
            IImageBlur imageBlur = new GaussianBlur();
            var blurredImage = imageBlur.Blur(bitmap, 5);
            pictureBox_result.Image = blurredImage;
        }
    }
}