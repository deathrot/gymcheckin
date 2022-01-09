using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace GymCheckin.Utility
{
    public static class ImageUtility
    {
        public static byte[] ResizeImage(byte[] data, decimal factor)
        {
            using (var image1 = Image.Load(data))
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
            using (var image1 = Image.Load(FileUtility.GetFullPath(file1)))
            using (var image2 = Image.Load(FileUtility.GetFullPath(file2)))
            using (var image3 = new Image<Rgba32>(Math.Max(image1.Width, image2.Width) + 20,
                                                image1.Height + image2.Height + 100, Rgba32.ParseHex("#fff")))
            {
                int totalWidth = Math.Max(image1.Width, image2.Width);
                int totalHeight = image1.Height + image2.Height + 10;


                image3.Mutate(o =>
                    o.DrawImage(image1, new Point((totalWidth - image1.Width) / 2, 10), 1f)
                    .DrawImage(image2, new Point((totalWidth - image2.Width) / 2, image1.Height + 20), 1f)
                );

                image3.SaveAsPng(FileUtility.GetFullPath(file3));
            }

            return true;
        }

        public static bool CombileImageFiles(byte[] file1, byte[] file2, string file3,
                                                out int totalWidthParam, out int totalHeightParam)
        {
            using (var image1 = Image.Load(new System.IO.MemoryStream(file1)))
            using (var image2 = Image.Load(new System.IO.MemoryStream(file2)))
            using (var image3 = new Image<Rgba32>(Math.Max(image1.Width, image2.Width) + 20,
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

                image3.SaveAsPng(FileUtility.GetFullPath(file3));
            }

            return true;
        }



    }
}
