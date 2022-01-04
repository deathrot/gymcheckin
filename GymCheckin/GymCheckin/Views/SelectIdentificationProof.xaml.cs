using Plugin.Media;
using Stormlion.ImageCropper;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymCheckin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectIdentificationProof : ContentPage
    {

        public SelectIdentificationProof()
        {
            InitializeComponent();
        }

        async private void btnSelectIdentificationProof_Clicked(object sender, EventArgs e)
        {
            try
            {
                //await CrossMedia.Current.Initialize();

                new ImageCropper()
                {
                    PageTitle = "Select Identification Proof",
                    AspectRatioX = 1,
                    AspectRatioY = 1,
                    CropShape = ImageCropper.CropShapeType.Rectangle,
                    CancelButtonTitle = "Cancel",

                    Faiure = (errorType) =>
                    {
                        DisplayAlert("Alert", "No image selected. Please try again...", "Ok");
                    },

                    Success = (imageFile) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            string finalResourceName = $"final_{DateTime.Now.Ticks}.png";
                            Utility.FileUtility.SaveToFile("id.png", System.IO.File.ReadAllBytes(imageFile));
                            Utility.FileUtility.CombileImageFiles("vc.png", "id.png", finalResourceName);

                            Xamarin.Essentials.Preferences.Set(Utility.Constants.PreferenceStore_ImageResource, finalResourceName);
                            Xamarin.Essentials.Preferences.Set(Utility.Constants.PreferenceStore_Initialize, true);

                            imgIdentificationProof.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(finalResourceName));

                            btnNext.IsVisible = true;
                        });
                    }
                }.Show(this);
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Alert!", ex.ToString(), "Ok");
                System.Diagnostics.Debug.WriteLine($"{ex.ToString()}");
            }
        }

        async private void btnNext_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

    }
}