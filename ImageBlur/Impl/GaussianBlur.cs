using System;
using System.Drawing;

namespace ImageBlurring.ImageBlur.Impl
{
    public class GaussianBlur : IImageBlur
    {
        public Bitmap Blur(Bitmap sourceBitmap, int radius)
        {
            var sigma = Math.Max((double) radius / 2, 1);
            //var sigma = (radius + 0.5) / Math.PI;
            var width = sourceBitmap.Width;
            var height = sourceBitmap.Height;

            var blurredBitmap = (Bitmap) sourceBitmap.Clone();

            var weightMatrix = CreateWeightMatrix(sigma, radius);

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var rgb = new double[3];

                for (var i = -radius; i <= radius; i++)
                for (var j = -radius; j <= radius; j++)
                {
                    var px = x + i;
                    var py = y + j;

                    if (px < 0) px = 0;
                    if (px >= width) px = width - 1;
                    if (py < 0) py = 0;
                    if (py >= height) py = height - 1;

                    var weight = weightMatrix[i + radius, j + radius];

                    rgb[0] += sourceBitmap.GetPixel(px, py).R * weight;
                    rgb[1] += sourceBitmap.GetPixel(px, py).G * weight;
                    rgb[2] += sourceBitmap.GetPixel(px, py).B * weight;
                }

                blurredBitmap.SetPixel(x, y, GetColorFromRgb(rgb));
            }

            return blurredBitmap;
        }

        private double[,] CreateWeightMatrix(double sigma, int radius)
        {
            var kernelWidth = 2 * radius + 1;
            var weightMatrix = new double[kernelWidth, kernelWidth];

            var sum = 0.0;
            for (var x = 0; x < kernelWidth; x++)
            for (var y = 0; y < kernelWidth; y++)
            {
                var _x = x - radius;
                var _y = y - radius;
                var kernelValue = Math.Exp(-((Math.Pow(_x, 2) + Math.Pow(_y, 2)) / (2 * Math.Pow(sigma, 2))))
                                  / (2 * Math.PI * Math.Pow(sigma, 2));
                sum += kernelValue;
                weightMatrix[x, y] = kernelValue;
            }

            for (var x = 0; x < kernelWidth; x++)
            for (var y = 0; y < kernelWidth; y++)
                weightMatrix[x, y] /= sum;

            return weightMatrix;
        }

        private Color GetColorFromRgb(double[] rgb)
        {
            return Color.FromArgb((byte) rgb[0], (byte) rgb[1], (byte) rgb[2]);
        }
    }
}