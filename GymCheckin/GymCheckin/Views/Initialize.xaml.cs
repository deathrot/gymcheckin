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
    public partial class Initialize : ContentPage
    {

        private InitializeModel model = null;

        public Initialize()
        {
            this.model = new InitializeModel();

            InitializeComponent();
            
            this.BindingContext = model;
        }
                
        async private void btnLetsGetStarted_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectVaccinePassport());
        }

    }
}