using System;
using Xamarin.Forms;
using GalaSoft.MvvmLight;

namespace TinyStore_XamarinForms.ViewModel
{
    public class BaseViewModel : ViewModelBase
    {
        public static bool Development { get; set; } = true;
		internal INavigation Navigation { get; set; }

        public BaseViewModel()
        {
        }
    }
}
