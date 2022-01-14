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

        private ViewModels.ActivityViewModel model = new ViewModels.ActivityViewModel();

        public SelectIdentificationProof()
        {
            InitializeComponent();
            this.BindingContext = model;
            setUpView();
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

                    Faiure = (errorType) =>
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
                            
                            imgIdentificationProof.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(idImage));

                            model.CanProceedNext = true;
                            model.IsBusy = false;
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

        async private void btnNext_Click(object sender, EventArgs e)
        {
            
            string vc = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC, string.Empty);
            string id = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID, string.Empty);


            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_ID,
                                                    Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID_TEMP, string.Empty));
            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_VC,
                                                    Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC_TEMP, string.Empty));

            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_Initialize, true);
            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_Number_Of_Use,
                                                Utility.PreferencesUtility.GetSavedPreferenceAsInt(Utility.Constants.PreferenceStore_Number_Of_Use, 0) + 1);

            Utility.FileUtility.CleanUp(vc);
            Utility.FileUtility.CleanUp(id);

            await Navigation.PushAsync(new MainPage());
        }

        void setUpView()
        {
            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                imgNext.Source = ImageSource.FromResource("GymCheckin.images.next_dark.png", typeof(SelectIdentificationProof));
            }
            else
            {
                imgNext.Source = ImageSource.FromResource("GymCheckin.images.next.png", typeof(SelectIdentificationProof));
            }
        }

    }
}