using System.Collections.Generic;
using System.Drawing;

namespace ImageBlurring.ImageBlur.Impl
{
    public class MedianFilter : IImageBlur
    {
        public Bitmap Blur(Bitmap sourceBitmap, int radius)
        {
            var width = sourceBitmap.Width;
            var height = sourceBitmap.Height;

            var blurredBitmap = (Bitmap) sourceBitmap.Clone();

            var rValuesList = new List<byte>();
            var gValuesList = new List<byte>();
            var bValuesList = new List<byte>();

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                rValuesList.Clear();
                gValuesList.Clear();
                bValuesList.Clear();

                for (var j = -radius; j <= radius; j++)
                for (var i = -radius; i <= radius; i++)
                {
                    var px = GetPixelIndex(x + i, width);
                    var py = GetPixelIndex(y + j, height);

                    rValuesList.Add(sourceBitmap.GetPixel(px, py).R);
                    gValuesList.Add(sourceBitmap.GetPixel(px, py).G);
                    bValuesList.Add(sourceBitmap.GetPixel(px, py).B);
                }

                blurredBitmap.SetPixel(x, y, CreateColorFromMedianRgbValues(rValuesList, gValuesList, bValuesList));
            }

            return blurredBitmap;
        }

        private int GetPixelIndex(int index, int size)
        {
            return index < 0 ? 0 : index > size - 1 ? size - 1 : index;
        }

        private Color CreateColorFromMedianRgbValues(List<byte> r, List<byte> g, List<byte> b)
        {
            r.Sort();
            g.Sort();
            b.Sort();

            return Color.FromArgb(r[r.Count / 2], g[g.Count / 2], b[b.Count / 2]);
        }
    }
}