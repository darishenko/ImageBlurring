using System.Drawing;

namespace ImageBlurring.ImageBlur
{
    public interface IImageBlur
    {
        Bitmap Blur(Bitmap sourceBitmap, int radius);
    }
}