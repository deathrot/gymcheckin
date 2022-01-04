using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
//using Controls.ImageCropper;

namespace GymCheckin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!Xamarin.Essentials.Preferences.ContainsKey(Utility.Constants.PreferenceStore_Initialize))
            {
                Navigation.PushAsync(new Initialize(), true);
            }
            else
            {
                string finalResourceFileName = Xamarin.Essentials.Preferences.Get(Utility.Constants.PreferenceStore_ImageResource, string.Empty);

                string fileName = Utility.FileUtility.GetFullPath(finalResourceFileName);

                var imageInfo = SixLabors.ImageSharp.Image.Identify(fileName);
                int width = imageInfo.Width;
                int height = imageInfo.Height;

                System.Diagnostics.Debug.WriteLine(Utility.FileUtility.GetFullPath(finalResourceFileName));
                imgResource.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(finalResourceFileName));
                imgResource.HeightRequest = height;
                imgResource.WidthRequest = width;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Xamarin.Essentials.Preferences.Remove(Utility.Constants.PreferenceStore_Initialize);
            Navigation.PushAsync(new Initialize(), true);
        }
    }
}