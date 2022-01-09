using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymCheckin.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        bool _IsPotrait = true;
        public bool IsPotrait
        {
            get
            {
                return _IsPotrait;
            }
            set
            {
                if (_IsPotrait != value)
                {
                    _IsPotrait = value;
                    notifyPropertyChanged("IsPotrait");
                    notifyPropertyChanged("Orientation");
                }
            }
        }

        public Xamarin.Forms.StackOrientation Orientation
        {
            get
            {
                return IsPotrait ? Xamarin.Forms.StackOrientation.Vertical : Xamarin.Forms.StackOrientation.Horizontal;
            }
        }

        private void notifyPropertyChanged(string propertyName)
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
