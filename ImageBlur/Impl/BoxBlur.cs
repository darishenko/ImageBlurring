using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageBlurring.ImageBlur.Impl
{
    public class BoxBlur : IImageBlur
    {
        public Bitmap Blur(Bitmap sourceBitmap, int radius)
        {
            var area = (int) Math.Pow(2 * radius + 1, 2);

            var blurredBitmap = (Bitmap) sourceBitmap.Clone();
            var summedTable = CreateSummedTable(blurredBitmap);

            for (var x = 0; x < blurredBitmap.Width; x++)
            for (var y = 0; y < blurredBitmap.Height; y++)
            {
                var isEdge = y + radius >= blurredBitmap.Height ||
                             x + radius >= blurredBitmap.Width ||
                             x - radius - 1 < 0 ||
                             y - radius - 1 < 0;
                var newColor = isEdge
                    ? CalculateColorForImageEdge(x, y, radius, sourceBitmap)
                    : CalculateColorForImageCenter(x, y, radius, area, summedTable);

                blurredBitmap.SetPixel(x, y, newColor);
            }

            return blurredBitmap;
        }

        private Color CalculateColorForImageCenter(int x, int y, int radius, int area, List<List<int[]>> summedTable)
        {
            var sumPixels = new int[3];

            for (var i = 0; i < 3; i++)
                sumPixels[i] = summedTable[y + radius][x + radius][i]
                               - summedTable[y + radius][x - radius - 1][i]
                               - summedTable[y - radius - 1][x + radius][i]
                               + summedTable[y - radius - 1][x - radius - 1][i];

            for (var i = 0; i < 3; i++) sumPixels[i] /= area;

            return GetColorFromRgb(sumPixels);
        }

        private Color CalculateColorForImageEdge(int x, int y, int radius, Bitmap sourceBitmap)
        {
            var sumPixels = new int[3];
            var count = 1;
            for (var i = -radius; i <= radius; i++)
            for (var j = -radius; j <= radius; j++)
            {
                var newX = x + i;
                var newY = y + j;

                if (newX >= 0 && newX < sourceBitmap.Width && newY >= 0 && newY < sourceBitmap.Height)
                {
                    var pixel = sourceBitmap.GetPixel(newX, newY);
                    sumPixels[0] += pixel.R;
                    sumPixels[1] += pixel.G;
                    sumPixels[2] += pixel.B;
                    count++;
                }
            }

            for (var i = 0; i < 3; i++) sumPixels[i] /= count;

            return GetColorFromRgb(sumPixels);
        }

        private Color GetColorFromRgb(int[] rgb)
        {
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private List<List<int[]>> CreateSummedTable(Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;

            var table = new List<List<int[]>>
            {
                new()
                {
                    GetRgbArrayFromColor(bitmap.GetPixel(0, 0))
                }
            };

            for (var x = 1; x < width; x++)
            {
                var currentPixel = GetRgbArrayFromColor(bitmap.GetPixel(x, 0));
                var previousPixel = table[0][x - 1];

                table[0].Add(SumTwoPixels(currentPixel, previousPixel));
            }

            for (var y = 1; y < height; y++)
            {
                var currentPixel = GetRgbArrayFromColor(bitmap.GetPixel(0, y));
                var previousPixel = table[y - 1][0];

                table.Add(new List<int[]> {SumTwoPixels(currentPixel, previousPixel).ToArray()});
            }

            for (var x = 1; x < width; x++)
            for (var y = 1; y < height; y++)
            {
                var currentPixel = GetRgbArrayFromColor(bitmap.GetPixel(x, y));
                var upperPixel = table[y - 1][x];
                var leftPixel = table[y][x - 1];
                var upperLeftPixel = table[y - 1][x - 1];

                var updatedPixel = new[]
                {
                    currentPixel[0] + upperPixel[0] + leftPixel[0] - upperLeftPixel[0],
                    currentPixel[1] + upperPixel[1] + leftPixel[1] - upperLeftPixel[1],
                    currentPixel[2] + upperPixel[2] + leftPixel[2] - upperLeftPixel[2]
                };

                table[y].Add(updatedPixel);
            }

            return table;
        }

        private int[] SumTwoPixels(int[] first, int[] second)
        {
            return new[]
            {
                first[0] + second[0],
                first[1] + second[1],
                first[2] + second[2]
            };
        }

        private int[] GetRgbArrayFromColor(Color color)
        {
            return new int[]
            {
                color.R,
                color.G,
                color.B
            };
        }
    }
}