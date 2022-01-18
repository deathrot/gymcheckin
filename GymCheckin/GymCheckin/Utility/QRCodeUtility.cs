using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UglyToad.PdfPig;

namespace GymCheckin.Utility
{
    public static class QRCodeUtility
    {

        /// <summary>
        /// Gets all the QR code that can be found in the pdf file in the order
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static List<byte[]> GetQRCodeFromPDF(Stream stream)
        {
            List<byte[]> result = new List<byte[]>();
            using (PdfDocument document = PdfDocument.Open(stream))
            {
                foreach (var page in document.GetPages())
                {
                    var images = page.GetImages();
                    foreach (var image in images)
                    {
                        byte[] imageDataInBytes;
                        image.TryGetPng(out imageDataInBytes);

                        if (QRCodeUtility.IsQRCode(imageDataInBytes))
                        {
                            result.Add(imageDataInBytes);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns true if the input image is a qr code
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public static bool IsQRCode(byte[] imageData)
        {

            using (var imageObject = SixLabors.ImageSharp.Image.Load(imageData))
            {
                QRCodeDecoderLibrary.QRDecoder decoder = new QRCodeDecoderLibrary.QRDecoder(new Models.NullLogger<QRCodeDecoderLibrary.QRDecoder>());
                var qrData = decoder.ImageDecoder(imageObject);

                if (qrData != null )
                {
                    return true;
                }

                return false;
            }
        }

    }
}
