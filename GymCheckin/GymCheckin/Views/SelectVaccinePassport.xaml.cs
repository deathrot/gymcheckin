using System;
using System.Linq;
using UglyToad.PdfPig;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GymCheckin.Views
{
    public partial class SelectVaccinePassport : ContentPage
    {

        private ViewModels.ActivityViewModel model = new ViewModels.ActivityViewModel();

        public SelectVaccinePassport()
        {
            InitializeComponent();
            this.BindingContext = model;
        }

        async private void btnSelectVaccineCertificate_Clicked(object sender, EventArgs e)
        {
            bool success = false;
            try
            {
                model.IsBusy = true;
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
                                model.CanProceedNext = true;
                                model.IsBusy = false;

                                qrCodeBytes = Utility.ImageUtility.ResizeImage(qrCodeBytes, 1.5M);

                                string vcImage = $"vc_{DateTime.Now.Ticks}.png";

                                int totalWidth;
                                int totalHeight;
                                Utility.ImageUtility.CombileImageFiles(headerBytes, qrCodeBytes, vcImage, out totalWidth, out totalHeight);

                                this.imgBarCode.WidthRequest = totalWidth;
                                this.imgBarCode.HeightRequest = totalHeight;
                                
                                this.imgBarCode.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(vcImage));

                                Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_VC_TEMP, vcImage);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CapturePhotoAsync THREW: {ex.ToString()}");
            }
            finally
            {
                model.IsBusy = false;
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