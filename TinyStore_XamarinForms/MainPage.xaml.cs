using System;
using Xamarin.Forms;
using TinyStore_XamarinForms.View;
using TinyStore_XamarinForms.ViewModel;

namespace TinyStore_XamarinForms
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;


        public MainPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as MainViewModel;
            if(_viewModel != null)
            {
				_viewModel.Navigation = Navigation as INavigation;
			}
        }
              
    }
}