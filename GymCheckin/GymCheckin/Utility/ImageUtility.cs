using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;

namespace GymCheckin.Utility
{

    /// <summary>
    /// Static methods for image manipulation
    /// </summary>
    public static class ImageUtility
    {

        /// <summary>
        /// Resizes the passed image in raw bytes by a factor
        /// </summary>
        /// <param name="data">raw data</param>
        /// <param name="factor">factor must be positive and greater than 0</param>
        /// <returns></returns>
        public static byte[] ResizeImage(byte[] data, decimal factor, out Models.ImageDetails imageDetails)
        {
            imageDetails = new Models.ImageDetails();

            if (factor <= 0)
            {
                throw new InvalidOperationException("Factor must be greater than 0");
            }

            using (var image1 = Image.Load(data))
            {
                image1.Mutate(o =>
                    o.Resize((int)(image1.Width * factor), (int)(image1.Height * factor)));

                imageDetails.Width = image1.Width;
                imageDetails.Height = image1.Height;

                using (var stream = new System.IO.MemoryStream())
                {
                    image1.Save(stream, SixLabors.ImageSharp.Formats.Png.PngFormat.Instance);
                    stream.Flush();
                    return stream.GetBuffer();
                }
            }
        }


        /// <summary>
        /// Combines images together and saves the output in a file 
        /// </summary>
        /// <param name="file1">image 1 file name without path</param>
        /// <param name="file2">image 1 file name without path</param>
        /// <param name="file3">name of the output</param>
        /// <returns></returns>
        public static Models.ImageDetails CombileImageFiles(string file1, string file2, string file3)
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

                var result = new Models.ImageDetails
                {
                    Width = totalWidth,
                    Height = totalHeight
                };

                return result;
            }
        }

        /// <summary>
        /// Gets the image info by reading the raw bytes and returns the image details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.ImageDetails GetImageDetails(byte[] data)
        {
            var info = Image.Identify(data);

            var details = new Models.ImageDetails
            {
                Width = info.Width,
                Height = info.Height
            };

            return details;
        }

        /// <summary>
        /// Gets the image info by reading the raw bytes and returns the image details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.ImageDetails GetImageDetails(string imageFile)
        {
            var info = Image.Identify(FileUtility.GetFullPath(imageFile));

            var details = new Models.ImageDetails
            {
                Width = info.Width,
                Height = info.Height
            };

            return details;
        }

        /// <summary>
        /// Combines images together and saves the output in a file 
        /// </summary>
        /// <param name="file1">raw image bytes</param>
        /// <param name="file2">raw image bytes</param>
        /// <param name="file3">name of the output</param>
        /// <returns></returns>
        public static Models.ImageDetails CombileImageFiles(byte[] file1, byte[] file2, string file3)
        {
            using (var image1 = Image.Load(new System.IO.MemoryStream(file1)))
            using (var image2 = Image.Load(new System.IO.MemoryStream(file2)))
            using (var image3 = new Image<Rgba32>(Math.Max(image1.Width, image2.Width) + 20,
                                                image1.Height + image2.Height + 20, Rgba32.ParseHex("#fff")))
            {
                int totalWidth = Math.Max(image1.Width, image2.Width);
                int totalHeight = image1.Height + image2.Height + 10;

                image3.Mutate(o =>
                    o.DrawImage(image1, new Point((totalWidth - image1.Width) / 2, 10), 1f)
                    .DrawImage(image2, new Point((totalWidth - image2.Width) / 2, image1.Height + 20), 1f)
                );

                image3.SaveAsPng(FileUtility.GetFullPath(file3));

                var result = new Models.ImageDetails
                {
                    Width = totalWidth,
                    Height = totalHeight
                };

                return result;
            }
        }



    }
}
