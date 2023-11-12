using System;
using System.Drawing;

namespace ImageBlurring.ImageBlur.Impl
{
    public class SobelOperator : IImageBlur
    {
        private const double RWeight = 0.3;
        private const double GWeight = 0.59;
        private const double BWeight = 0.11;

        private static readonly int[,] RowMask =
        {
            {-1, 0, 1},
            {-2, 0, 2},
            {-1, 0, 1}
        };

        private static readonly int[,] ColumnMask =
        {
            {-1, -2, -1},
            {0, 0, 0},
            {1, 2, 1}
        };

        public Bitmap Blur(Bitmap sourceBitmap, int radius)
        {
            var width = sourceBitmap.Width;
            var height = sourceBitmap.Height;

            var blurredBitmap = (Bitmap) sourceBitmap.Clone();

            for (var x = 1; x < width - 1; x++)
            for (var y = 1; y < height - 1; y++)
            {
                var rgbX = new int[3];
                var rgbY = new int[3];

                for (var j = -1; j <= 1; j++)
                for (var i = -1; i <= 1; i++)
                {
                    var rgb = GetRgbArrayFromColor(sourceBitmap.GetPixel(x + i, y + j));
                    for (var k = 0; k < rgb.Length; k++)
                    {
                        rgbX[k] += rgb[k] * RowMask[j + 1, i + 1];
                        rgbY[k] += rgb[k] * ColumnMask[j + 1, i + 1];
                    }
                }

                blurredBitmap.SetPixel(x, y, CreateGreyColor(rgbX, rgbY));
            }

            return blurredBitmap;
        }

        private Color CreateGreyColor(int[] rgbX, int[] rgbY)
        {
            var rgbMagnitude = new int[3];
            for (var i = 0; i < rgbX.Length; i++)
                rgbMagnitude[i] = (int) Math.Sqrt(Math.Pow(rgbX[i], 2) + Math.Pow(rgbY[i], 2));

            return Color.FromArgb(
                (byte) Math.Min(255, rgbMagnitude[0] * RWeight),
                (byte) Math.Min(255, rgbMagnitude[1] * GWeight),
                (byte) Math.Min(255, rgbMagnitude[2] * BWeight)
            );
        }

        private int[] GetRgbArrayFromColor(Color color)
        {
            return new int[]
            {
                color.G,
                color.R,
                color.B
            };
        }
    }
}