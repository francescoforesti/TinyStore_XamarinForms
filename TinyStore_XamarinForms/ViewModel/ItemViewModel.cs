using System;
using GalaSoft.MvvmLight.Ioc;
using TinyStore_XamarinForms.Service;
using TinyStore_XamarinForms.Model;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace TinyStore_XamarinForms.ViewModel
{
    public class ItemViewModel : BaseViewModel
    {
        private ICatalogService service;
        private CartViewModel _cartViewModel;

        private Item _item;
        public Item Item 
        { 
            get 
            {
                return _item;
            } 
            set 
            {
                if(Set(ref _item, value))
                {
                    // TODO : handle value change
                }
            }
        }

        private UInt16 _quantity = 0;
        public string Quantity 
        { 
            get { return ""+_quantity; }
            set
            {
                UInt16 parsedValue;
                if(UInt16.TryParse(value, out parsedValue))
                {
                    Set(ref _quantity, parsedValue);
                }
                else
                {
                    Set(ref _quantity, UInt16.Parse("0"));
                }
            }
        }

        public ItemViewModel()
        {
			this.service = SimpleIoc.Default.GetInstance<ICatalogService>();
            _cartViewModel = SimpleIoc.Default.GetInstance<CartViewModel>();
		}

        public async Task LoadItem(long itemId)
        {
            Item = await service.FindItem(itemId);
        }

        public bool AddNewCartItem() 
        {
            CartItem cartItem = new CartItem(_item, _quantity);
            bool result = _cartViewModel.AddToCart(cartItem);
            if(result)
            {
				Quantity = "0";
			}
            return result;
        }

    }
}
