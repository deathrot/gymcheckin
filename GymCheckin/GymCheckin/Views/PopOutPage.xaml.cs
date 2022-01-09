using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymCheckin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopOutPage : ContentPage
    {
        string imageName = null;

        public PopOutPage(string imageName)
        {
            InitializeComponent();
            this.imageName = imageName;
        }

        public PopOutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (!string.IsNullOrEmpty(imageName))
                {
                    var imageInfo = SixLabors.ImageSharp.Image.Identify(imageName);
                    int width = imageInfo.Width;
                    int height = imageInfo.Height;

                    imgResource.Source = ImageSource.FromFile(imageName);
                    imgResource.HeightRequest = height;
                    imgResource.WidthRequest = width;
                }
            }
            catch
            {

            }

        }

        async protected void imgResource_Tapped(object sender, EventArgs eventArgs)
        {
            await Navigation.PopModalAsync();
        }
    }
}