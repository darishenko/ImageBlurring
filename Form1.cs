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

            textBox3.ResetText();
            var bitmap = new Bitmap(filename);
            pictureBox_source.Image = bitmap;
            IImageBlur imageBlur = new SobelOperator();
            var radius = GetRadiusValue(textBox1);
            var blurredImage = GetSelectedImageBlur(comboBox1).Blur(bitmap, radius);
            pictureBox_result.Image = blurredImage;
            textBox3.Text = "ready";
        }

        private int GetRadiusValue(TextBox radiusTextBox)
        {
            var radius = 0;
            try
            {
                radius = int.Parse(radiusTextBox.Text);
            }
            catch (Exception ex)
            {
                // ignored
            }

            return radius;
        }

        private IImageBlur GetSelectedImageBlur(ComboBox comboBoxImageBlurType)
        {
            var imageBlurType = (BlurringType) comboBoxImageBlurType.SelectedIndex;

            switch (imageBlurType)
            {
                case BlurringType.BoxBlur:
                    return new BoxBlur();
                case BlurringType.GaussianBlur:
                    return new GaussianBlur();
                case BlurringType.MedianFilter:
                    return new MedianFilter();
                case BlurringType.SobelOperator:
                    return new SobelOperator();
                default:
                    return new BoxBlur();
            }
        }
    }
}