using GymCheckin.Dummy;
using GymCheckin.Models;
using Plugin.Media;
using QRCodeDecoderLibrary;
using Stormlion.ImageCropper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymCheckin.Views
{
    public partial class SelectVaccinePassport : ContentPage
    {
        public SelectVaccinePassport()
        {
            InitializeComponent();
        }


        async private void btnSelectVaccineCertificate_Clicked(object sender, EventArgs e)
        {
            bool success = false;
            try
            {
                PickOptions options = new PickOptions()
                {
                    FileTypes = FilePickerFileType.Pdf,
                    PickerTitle = "Select Vacine Certificate"
                };

                var fileResult = await FilePicker.PickAsync(options);

                if ( fileResult != null && !string.IsNullOrEmpty(fileResult.FullPath))
                {
                    using (var stream = await fileResult.OpenReadAsync())
                    {
                        using (PdfDocument document = PdfDocument.Open(stream))
                        {
                            var page = document.GetPage(1);

                            var images = page.GetImages();

                            if ( images != null && images.Count() >= 3)
                            {
                                var header = images.ElementAt(0);
                                var qrCode = images.ElementAt(2);

                                byte[] qrCodeBytes;
                                qrCode.TryGetPng(out qrCodeBytes);

                                byte[] headerBytes;
                                header.TryGetPng(out headerBytes);

                                if (qrCodeBytes == null)
                                    return;

                                success = true;
                                btnNext.IsVisible = true;

                                qrCodeBytes = Utility.FileUtility.ResizeVC(qrCodeBytes, 1.5M);

                                int totalWidth;
                                int totalHeight;
                                Utility.FileUtility.CombileImageFiles(headerBytes, qrCodeBytes, "vc.png", out totalWidth, out totalHeight);

                                this.imgBarCode.WidthRequest = totalWidth;
                                this.imgBarCode.HeightRequest = totalHeight;
                                
                                this.imgBarCode.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath("vc.png"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CapturePhotoAsync THREW: {ex.ToString()}");
            }

            if (!success)
            {
                await DisplayAlert("Alert", "Something is not right with the pdf selected. Please try selecting the downloaded certificate again...", "Ok");
            }
        }

        async void btnNext_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectIdentificationProof());
        }

    }
}