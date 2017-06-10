using System.Collections.ObjectModel;
using TinyStore_XamarinForms.Model;
using System;
using System.Windows.Input;
using Realms;
using GalaSoft.MvvmLight.Command;

namespace TinyStore_XamarinForms.ViewModel
{
    public class CartViewModel : BaseViewModel
    {

        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();
		public ICommand RemoveItem { get; set; }

        private Double _total = 0;
        public Double TotalPrice { get { return _total; } set { Set(ref _total, value); } }

        private Realm realm;

        internal void RemoveFromCart(int row)
        {
            CartItems.RemoveAt(row);
        }

        public CartViewModel()
        {
            realm = Realm.GetInstance();
            RemoveItem = new RelayCommand<CartItem>((item) => 
            { 
                CartItems.Remove(item); 
                doInTransaction(((o1, o2) => (o1 as Realm).Remove(o2 as CartItem)), realm, item);
            });
            LoadCart();
		}

		private void LoadCart()
		{
			foreach (CartItem ci in realm.All<CartItem>().AsRealmCollection())
			{
				CartItems.Add(ci);
			}
		}

		private void SaveLocally()
		{
			realm.Write(() =>
			{
				foreach (CartItem cItem in CartItems)
				{
					realm.Add(cItem);
				}
			});
		}

        public bool AddToCart(CartItem newItem)
        {
            bool result = false;
            foreach (CartItem item in CartItems)
            {
                if (item.ItemId == newItem.ItemId)
                {
                    if(item.IsManaged) {
                        doInTransaction(((o1, o2) => (o1 as CartItem).Quantity += (o2 as CartItem).Quantity), item, newItem);
                    } 
                    else 
                    {
                        item.Quantity += newItem.Quantity;
                    }
                    result = true;
                }
            }
            if (!result)
            {
                CartItems.Add(newItem);
                result = true;
            }
            SaveLocally();
            CalcTotalPrice();
            return result;
        }

		private void CalcTotalPrice()
		{
			Double total = 0;
			foreach (CartItem item in CartItems)
			{
				total += (item.Price * item.Quantity);
			}
			TotalPrice = total;
		}

        private void doInTransaction(Action<object,object> action, object o1, object o2) {
			var transaction = realm.BeginWrite();
            action.DynamicInvoke(o1,o2);
			transaction.Commit();
        }
                      
    }
}
