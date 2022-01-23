using Plugin.Media;
using Stormlion.ImageCropper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymCheckin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectIdentificationProof : ContentPage
    {

        private ViewModels.ActivityViewModel model = new ViewModels.ActivityViewModel();

        public SelectIdentificationProof()
        {
            InitializeComponent();
            this.BindingContext = model;
        }

        async private void btnSelectIdentificationProof_Clicked(object sender, EventArgs e)
        {
            try
            {
                model.IsBusy = true;

                await CrossMedia.Current.Initialize();

                new ImageCropper()
                {
                    PageTitle = "Select Identification Proof",
                    AspectRatioX = 1,
                    AspectRatioY = 1,
                    CropShape = ImageCropper.CropShapeType.Rectangle,
                    CancelButtonTitle = "Cancel",

                    Faiure = (resultType) =>
                    {
                        DisplayAlert("Alert", "No image selected. Please try again...", "Ok");
                        model.IsBusy = false;
                    },

                    Success = (imageFile) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            string idImage = $"id_{DateTime.Now.Ticks}.png";
                            Utility.FileUtility.SaveToFile(idImage, System.IO.File.ReadAllBytes(imageFile));

                            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_ID_TEMP, idImage);
                                                                                   
                            model.CanProceedNext = true;
                            model.IsBusy = false;

                            navigateToIdentificationProofPage(idImage);
                        });
                    }
                }.Show(this);
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Alert!", ex.ToString(), "Ok");
                System.Diagnostics.Debug.WriteLine($"{ex.ToString()}");
                model.IsBusy = false;
            }
            finally
            {
            }
        }

        async void navigateToIdentificationProofPage(string imageName)
        {
            await Navigation.PushAsync(new ShowIdentificationProofPage());
        }

    }
}