
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TinyStore_XamarinForms.View;

namespace TinyStore_XamarinForms.ViewModel
{
    
    public class MainViewModel : BaseViewModel
    {

		public ICommand GoToAbout { get; set; }
		public ICommand GoToCatalog { get; set; }
		public ICommand GoToCart { get; set; }

        public MainViewModel()
        {
			GoToAbout = new RelayCommand(() => Navigation.PushAsync(new AboutPage()));
			GoToCatalog = new RelayCommand(() => Navigation.PushAsync(new CatalogPage()));
            GoToCart = new RelayCommand(() => Navigation.PushAsync(new CartPage()));
        }
    }
}