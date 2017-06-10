using System;
using TinyStore_XamarinForms.ViewModel;
using Xamarin.Forms;

namespace TinyStore_XamarinForms.View
{
    public partial class CartPage : ContentPage
    {
        private CartViewModel _viewModel;

        public CartPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as CartViewModel;
        }

        public void OnCheckout(object sender, EventArgs args)
        {
            // TODO
            DisplayAlert("Your Order has been placed!", "Thank you for ordering", "OK");
            _viewModel.Clear();
        }
    }
}
