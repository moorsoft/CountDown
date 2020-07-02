using CountDown.Extensions;
using System.Drawing;
using System.Windows.Media;

namespace CountDown.Utils
{
    public class ScreenUtils
    {
        public static ImageBrush CaptureScreen(int left, int top, int width, int height)
        {
            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(left, top, 0, 0, bmp.Size);
                }

                return new ImageBrush(bmp.ToBitmapSource());
            }
        }
    }
}
