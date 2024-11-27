using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class MissProducts : Page
    {
      
        public MissProducts()
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

       

      
        private async void NameProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            List<Products> good = new List<Products>();
            List<Products> product = await Global.proxy.GetProductsBySelectAsync(((TextBox)sender).Name.ToString(), ((TextBox)sender).Text, true);
            foreach (Products p in product)

            {
                if (p.AmountMlay==0&& p.Status == true)
                    good.Add(p);
            }
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = good;

        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshProductsList();
            kindamountproduct.ItemsSource = await Global.proxy.GetListKindAmountProductAsync();
            kindamountproduct.DisplayMemberPath = "NameKindAmount";
            kindamountproduct.SelectedValuePath = "KodKindProduct";
        }

        private void lstV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void RefreshProductsList()
        {

            List<Products> good = new List<Products>();
            List<Products> product = await Global.proxy.GetProductsBySelectAsync("AmountMlay", "0", false);
            foreach (Products p in product)

            {
                if (p.Status == true)
                    good.Add(p);
            }
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = good;

        }
        private async void btnUpdate_Click_1(object sender, RoutedEventArgs e)
        {

            Global.currentProduct = lstV2.SelectedItem as Products;
            if(Global.currentProduct == null)
                fl2.Hide();
            else { 
            await Global.proxy.UpdateProductAmountAsync(Global.currentProduct, Convert.ToDouble(amount.Text), kindamountproduct.SelectedItem as KindAmountProduct);
            RefreshProductsList();
            ContentDialog dialog = new ContentDialog()
            {
                Content = "המוצר עודכן בהצלחה!",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
            kindamountproduct.SelectedItem = null;
            amount.Text = string.Empty;
            fl2.Hide();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}

