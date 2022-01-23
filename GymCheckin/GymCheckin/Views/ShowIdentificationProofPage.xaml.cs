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
    public partial class ShowIdentificationProofPage : ContentPage
    {

        public ShowIdentificationProofPage()
        {
            InitializeComponent();
            setUpView();
        }

        void setUpView()
        {
            string vcImage = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID_TEMP);

            var detail = Utility.ImageUtility.GetImageDetails(vcImage);
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.imgIdentificationProof.WidthRequest = detail.Width;
                this.imgIdentificationProof.HeightRequest = detail.Height;
            }

            this.imgIdentificationProof.Source = ImageSource.FromFile(Utility.FileUtility.GetFullPath(vcImage));
        }

        async void btnFinish_Click(object sender, EventArgs e)
        {
            string vc = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC, string.Empty);
            string id = Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID, string.Empty);

            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_ID,
                                                    Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_ID_TEMP, string.Empty));
            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_ImageResource_VC,
                                                    Utility.PreferencesUtility.GetSavedPreferenceAsString(Utility.Constants.PreferenceStore_ImageResource_VC_TEMP, string.Empty));

            Utility.PreferencesUtility.SavePreference(Utility.Constants.PreferenceStore_Initialize, true);

            Utility.FileUtility.CleanUp(vc);
            Utility.FileUtility.CleanUp(id);

            await Navigation.PopToRootAsync();
        }

    }
}