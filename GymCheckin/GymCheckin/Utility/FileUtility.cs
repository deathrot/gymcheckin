using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Threading.Tasks;

namespace GymCheckin.Utility
{
    public static class FileUtility
    {

        public static bool SaveToFile(string fileName, byte[] data)
        {
            try
            {
                System.IO.File.WriteAllBytes(GetFullPath(fileName), data);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static byte[] ResizeVC(byte[] data, decimal factor)
        {
            using (var image1 = SixLabors.ImageSharp.Image.Load(data))
            {
                image1.Mutate(o =>
                    o.Resize((int)(image1.Width * factor), (int)(image1.Height * factor)));

                using (var stream = new System.IO.MemoryStream())
                {
                    image1.Save(stream, SixLabors.ImageSharp.Formats.Png.PngFormat.Instance);
                    stream.Flush();
                    return stream.GetBuffer();
                }
            }
        }

        public static bool CombileImageFiles(string file1, string file2, string file3)
        {
            using (var image1 = SixLabors.ImageSharp.Image.Load(GetFullPath(file1)))
            using (var image2 = SixLabors.ImageSharp.Image.Load(GetFullPath(file2)))
            using (var image3 = new SixLabors.ImageSharp.Image<Rgba32>(Math.Max(image1.Width, image2.Width) + 20,
                                                image1.Height + image2.Height + 100, Rgba32.ParseHex("#fff")))
            {
                int totalWidth = Math.Max(image1.Width, image2.Width);
                int totalHeight = image1.Height + image2.Height + 10;


                image3.Mutate(o =>
                    o.DrawImage(image1, new Point((totalWidth - image1.Width) / 2, 10), 1f)
                    .DrawImage(image2, new Point((totalWidth - image2.Width) / 2, image1.Height + 20), 1f)
                );

                image3.SaveAsPng(GetFullPath(file3));
            }

            return true;
        }

        public static bool CombileImageFiles(byte[] file1, byte[] file2, string file3,
                                                out int totalWidthParam, out int totalHeightParam)
        {
            using (var image1 = SixLabors.ImageSharp.Image.Load(new System.IO.MemoryStream(file1)))
            using (var image2 = SixLabors.ImageSharp.Image.Load(new System.IO.MemoryStream(file2)))
            using (var image3 = new SixLabors.ImageSharp.Image<Rgba32>(Math.Max(image1.Width, image2.Width) + 20,
                                                image1.Height + image2.Height + 20, Rgba32.ParseHex("#fff")))
            {
                int totalWidth = Math.Max(image1.Width, image2.Width);
                int totalHeight = image1.Height + image2.Height + 10;

                totalWidthParam = totalWidth;
                totalHeightParam = totalHeight;

                image3.Mutate(o =>
                    o.DrawImage(image1, new Point((totalWidth - image1.Width) / 2, 10), 1f)
                    .DrawImage(image2, new Point((totalWidth - image2.Width) / 2, image1.Height + 20), 1f)
                );

                image3.SaveAsPng(GetFullPath(file3));
            }

            return true;
        }

        public static string GetFullPath(string file)
        {
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), file);
        }


    }
}
