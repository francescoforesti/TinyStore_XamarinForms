using System;
using TinyStore_XamarinForms.ViewModel;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TinyStore_XamarinForms.View
{
    public partial class ItemDetailPage : ContentPage
    {
        private ItemViewModel _viewModel;
        private static readonly int restrictCount = 2;

        public ItemDetailPage()
        {
            InitializeComponent();
            _viewModel = BindingContext as ItemViewModel;
			
			QuantityEntry.TextChanged += (sender, args) =>
            {

                Entry entry = sender as Entry;
                String val = entry.Text;

                if (InvalidQuantity(val))
                {
                    val = val.Remove(val.Length - 1);
                    entry.Text = val;
                }
            };
        }

        public ItemDetailPage(long ItemId) : this()
        {
            Task.Run(() => _viewModel.LoadItem(ItemId)).Wait();   
        }

        public void OnAddNewCartItem(object sender, EventArgs args) 
        {
            if(_viewModel.AddNewCartItem())
            {
				DisplayAlert("Item added to Your Cart", "Thanks!", "OK"); 
                Navigation.PopAsync();
            }
        }

		private bool InvalidQuantity(string val)
		{
			UInt16 parsed;
			return !String.IsNullOrEmpty(val) && (val.Length > restrictCount || !UInt16.TryParse(val, out parsed));
		}

	}
}
