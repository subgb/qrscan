using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ZXing;

namespace qrscan
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var list = ScanAllScreens();
            if (list.Length <= 0) return;
            var text = string.Join("\n\n", list);
            Console.WriteLine(text);
            Console.WriteLine("-----------------------\nPress ENTER to copy it!");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Clipboard.SetText(text);
            }
        }

        static string[] ScanAllScreens()
        {
            var list = new List<string>();
            var reader = new BarcodeReader();
            foreach (Screen screen in Screen.AllScreens)
            {
                using(var img = ScreenSnapshot(screen))
                {
                    var result = reader.Decode(img);
                    if (result != null) list.Add(result.Text);
                }
            }
            return list.ToArray();
        }

        static Bitmap ScreenSnapshot(Screen screen)
        {
            var rect = screen.Bounds;
            var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using(var gfx = Graphics.FromImage(bmp))
            {
                gfx.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }
    }
}
