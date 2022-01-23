using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using System.Reflection;
using Plugin.StoreReview;

//using Controls.ImageCropper;

namespace GymCheckin.Views
{
    public partial class MainPage : ContentPage
    {

        private double width = 0;
        private double height = 0;
        ViewModels.MainViewModel model = null;

        public MainPage()
        {
            InitializeComponent();
            this.model = new ViewModels.MainViewModel();
            this.BindingContext = model;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            setView();

            
            if (!Utility.PreferencesUtility.GetSavedPreferenceAsBool(Utility.Constants.PreferenceStore_Initialize))
            {
                await Navigation.PushAsync(new Initialize(), true);
            }
            else
            {
                try
                {
                    string finalResourceIdWithPath = getIdImageNameWithPath();
                    string finalResourceVCWithPath = getVCImageNameWithPath();

                    if (!string.IsNullOrEmpty(finalResourceIdWithPath) &&
                            !string.IsNullOrEmpty(finalResourceVCWithPath))
                    {
                        var imageInfoId = SixLabors.ImageSharp.Image.Identify(finalResourceIdWithPath);
                        int widthId = imageInfoId.Width;
                        int heightId = imageInfoId.Height;

                        var imageInfoVc = SixLabors.ImageSharp.Image.Identify(finalResourceVCWithPath);
                        int widthVC = imageInfoVc.Width;
                        int heightVC = imageInfoVc.Height;

                        //VC resource
                        imgResourceVCLandscape.Source = ImageSource.FromFile(finalResourceVCWithPath);
                        imgResourceVC.Source = ImageSource.FromFile(finalResourceVCWithPath);

                        //Id resource
                        imgResourceIdLandscape.Source = ImageSource.FromFile(finalResourceIdWithPath);
                        imgResourceId.Source = ImageSource.FromFile(finalResourceIdWithPath);

                        //For someone reason this works only on android
                        if (DeviceInfo.Platform == DevicePlatform.Android)
                        {
                            imgResourceVCLandscape.HeightRequest = heightVC;
                            imgResourceVCLandscape.WidthRequest = widthVC;

                            imgResourceVC.HeightRequest = heightVC;
                            imgResourceVC.WidthRequest = widthVC;

                            imgResourceId.HeightRequest = heightId;
                            imgResourceId.WidthRequest = widthId;

                            imgResourceIdLandscape.HeightRequest = heightId;
                            imgResourceIdLandscape.WidthRequest = widthId;
                        }
                    }

                    await createReviewRequest();
                }
                catch
                {

                }
            }
        }

        private void setView()
        {
            if(Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                imgReset.Source = ImageSource.FromResource("GymCheckin.images.gears_dark.png", typeof(MainPage).GetTypeInfo().Assembly);
            }
            else
            {
                imgReset.Source = ImageSource.FromResource("GymCheckin.images.gears.png", typeof(MainPage).GetTypeInfo().Assembly);
            }

            //imgStar.Source = ImageSource.FromResource("GymCheckin.images.star.png", typeof(MainPage).GetTypeInfo().Assembly);
        }

        async private void imgReset_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Initialize(), true);
        }

        //await CrossStoreReview.Current.RequestReview(true);

        async private void imgResourceId_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PopOutPage(getIdImageNameWithPath()));
        }

        async private void imgResourceVC_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PopOutPage(getVCImageNameWithPath()));
        }

        string getVCImageNameWithPath()
        {
            
            string finalResourceVC = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC, string.Empty);

            return Utility.FileUtility.GetFullPath(finalResourceVC);
        }

        string getIdImageNameWithPath()
        {
            string finalResourceId = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID, string.Empty);

            return Utility.FileUtility.GetFullPath(finalResourceId);
        }

        protected override void OnSizeAllocated(double width, double height)
        {            
            base.OnSizeAllocated(width, height); 
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                //Landscape
                if ( this.width > this.height)
                {
                    this.model.IsPotrait = false;
                }
                else
                {
                    this.model.IsPotrait = true;
                }
            }
        }

        async private Task createReviewRequest()
        {
            var numberOfUse = Utility.PreferencesUtility.GetSavedPreferenceAsInt(Utility.Constants.PreferenceStore_Number_Of_Use);

            if (Utility.ReviewUtility.ShouldRunReview(numberOfUse+1) )
            {
                await CrossStoreReview.Current.RequestReview(false);
            }

            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_Number_Of_Use, numberOfUse+1);
        }

    }
}