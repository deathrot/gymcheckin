using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymCheckin.ViewModels
{
    public class ActivityViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    notifyPropertyChanged("IsBusy");
                }
            }
        }

        bool _CanProceedNext = false;
        public bool CanProceedNext
        {
            get
            {
                return _CanProceedNext;
            }
            set
            {
                if (_CanProceedNext != value)
                {
                    _CanProceedNext = value;
                    notifyPropertyChanged("CanProceedNext");
                }
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
