using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymCheckin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowVaccinationProofPage : ContentPage
    {

        public ShowVaccinationProofPage()
        {
            InitializeComponent();
            setUpView();
        }

        void setUpView()
        {
            string vcImage = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC_TEMP);


            var detail = Utility.ImageUtility.GetImageDetails(vcImage);
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.imgBarCode.WidthRequest = detail.Width;
                this.imgBarCode.HeightRequest = detail.Height;
            }


            this.imgBarCode.WidthRequest = detail.Width;
            this.imgBarCode.HeightRequest = detail.Height;

            this.imgBarCode.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(vcImage));
        }

        async void btnNext_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectIdentificationProof());
        }

    }
}