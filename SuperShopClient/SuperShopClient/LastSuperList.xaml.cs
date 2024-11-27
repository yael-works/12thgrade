using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SuperShopClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LastSuperList : Page
    {
      List<ProductToBuying>  lstp=new List<ProductToBuying>();
        public LastSuperList()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            delete.Visibility = Visibility.Visible;
            string nameField;
            object st = null;
            nameField = ((ComboBox)sender).Name;
            object obj = ((ComboBox)sender).SelectedItem;
            Global.currentKindOfBuying = KodKindBuying.SelectedItem as KindOfBuying;
            //if (Global.currentKindOfBuying == null)
            //    KodKindBuying.SelectedIndex = 0;
            //else {
            //
            if (((ComboBox)sender).SelectedItem!=null)
            { 
                st = obj.GetType().GetProperty(nameField).GetValue(obj, null);
            lstVProducts.ItemsSource = null;
            lstVProducts.ItemsSource = await Global.proxy.GetProductToBuyingBySelectAsync(nameField, st.ToString(), false);
            lstp = await Global.proxy.GetProductToBuyingBySelectAsync(nameField, st.ToString(), false);
            }
            //}

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            KodKindBuying.ItemsSource = await Global.proxy.GetListKindOfBuyingAsync();
            KodKindBuying.DisplayMemberPath = "NameKindBuying";
            KodKindBuying.SelectedValuePath = "KodKindBuying";
        }

        private async void btt7_Click(object sender, RoutedEventArgs e)
        {
           
            bool flag1 = true;//האם מוצר מחוק
            string st1 = " ";
            List<ProductToBuying> list = await Global.proxy.GetProductToBuyingBySelectAsync("KodKindBuying", Global.currentKindOfBuying.KodKindBuying.ToString(), false);
            //בדיקת אם קיים
            foreach (object item in list)
            {
                ProductToBuying p = (ProductToBuying)item;
                if (p.KodProduct.Status == false)
                {
                    flag1 = false;
                    st1 = st1 + " " + p.KodProduct.NameProduct;
                }

             

            }
            //הוספה למלאי
            if ( flag1 == true)
            {
                foreach (object item in list)
                {
                    double x;
                    ProductToBuying p = (ProductToBuying)item;
                    Global.currentProduct = await Global.proxy.GetProductByCodeAsync(p.KodProduct.KodProduct);
                   
                    x= p.Amount * Global.currentProduct.AmountGmBag;
                    Global.currentProduct.AmountGmMlay = Global.currentProduct.AmountGmMlay + x;
                    Global.currentProduct.AmountMlay += p.Amount;
                    await Global.proxy.UpdateProductAsync(Global.currentProduct);
                   
                 
                }
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "הקניה בוצעה בהצלחה",
                    CloseButtonText = "תודה!"
                };
                await dialog.ShowAsync();
            }
            //הודעה למוצרים שמחוקים 
            if (flag1 == false )
            {

                ContentDialog dialog = new ContentDialog()
                {
                    Content = "הבמוצרים:" + " " + st1 + " " + "מחוקים אצלך שחזר אותם כדי לעדכנם במלאי!",
                    CloseButtonText = "ביטול"
                };
                await dialog.ShowAsync();
            }

            this.Frame.GoBack();


        }
     
        private async void Name1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
               lstp= await Global.proxy.GetProductToBuyingBySelectAsync("KodKindBuying", Global.currentKindOfBuying.KodKindBuying.ToString(), false);

                lstp = lstp.Where(w => w.KodProduct.NameProduct.StartsWith(NameProduct.Text)).ToList();
                lstVProducts.ItemsSource = lstp;
          
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            await Global.proxy.DeleteKindOfBuyingAsync(Global.currentKindOfBuying);
            KodKindBuying.ItemsSource = await Global.proxy.GetListKindOfBuyingAsync();
            KodKindBuying.DisplayMemberPath = "NameKindBuying";
            KodKindBuying.SelectedValuePath = "KodKindBuying";

        }
    }     

    } 
