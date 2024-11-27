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
    public sealed partial class NewSuperList : Page
    {
        private List<ProductToBuying> products;
        int a = 0;
        ProductToBuying p = null;
        public NewSuperList()
        {
            this.InitializeComponent();
            ClearProductsList();
        }

        private void ClearProductsList()
        {
            products = new List<ProductToBuying>();
            RefreshProductsList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddNewProduct));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

     



        private async void KodCateg_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string nameField;
            object st = null;
            nameField = ((ComboBox)sender).Name;
            object obj = ((ComboBox)sender).SelectedItem;
            st = obj.GetType().GetProperty(nameField).GetValue(obj, null);
            lstV2.ItemsSource = await Global.proxy.GetProductsBySelectAsync(nameField, st.ToString(), false);
        }

        private void btt7_Click(object sender, RoutedEventArgs e)
        {
            Global.currentProduct = lstV2.SelectedItem as Products;
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            KodCateg.ItemsSource = await Global.proxy.GetListCategoryAsync();
            KodCateg.DisplayMemberPath = "Nameteur";
            KodCateg.SelectedValuePath = "KodCateg";
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
           bool b=false;
            foreach (ProductToBuying p in products)
                if(p.KodProduct.KodProduct== Global.currentProduct.KodProduct)
                    b= true;
            if(b==false)
            { 
            ProductToBuying productToBuying = new ProductToBuying()
            {
                Amount = Convert.ToInt32(Gm.Text),
                KodProduct = Global.currentProduct,
                KodKindBuying = Global.currentKindOfBuying
            };
            products.Add(productToBuying);
            RefreshProductsList();
            }
            else
            {
                foreach (ProductToBuying p in products)
                    if (p.KodProduct.KodProduct == Global.currentProduct.KodProduct)
                        p.Amount += Convert.ToInt32(Gm.Text);
                RefreshProductsList();
            }
            Gm.Text = "";
        }

        private void RefreshProductsList()
        {
            lstVProducts.ItemsSource = null;
            lstVProducts.ItemsSource = products;
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string nameField, st;
            nameField = ((CheckBox)sender).Name;
            st = (((CheckBox)sender).IsChecked == true).ToString();
            lstVProducts.ItemsSource = await Global.proxy.GetProductToBuyingBySelectAsync(nameField, st,false);
        }

        private void Gm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lstV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.currentProduct = lstV2.SelectedItem as Products;
        }

        private async void FinallyAddSuperList(object sender, RoutedEventArgs e)
        {
            if(a==0)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "יש להגדיר שם לרשימת קניות!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else 
            { 
                Global.currentKindOfBuying = new KindOfBuying()
            {
                KodKindBuying = await Global.proxy.GetNextKeyKindOfBuyingAsync(),
                NameKindBuying = NameList.Text
            };
            await Global.proxy.AddKindOfBuyingAsync(Global.currentKindOfBuying);
            foreach (ProductToBuying product in products)
            {
                product.KodKindBuying = Global.currentKindOfBuying; // קוד רשימת קניות
                await Global.proxy.AddProductToBuyingAsync(product);
            }
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "הרשימה נוספה בהצלחה!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
                this.Frame.GoBack();
                ClearBuyingDetails();
            }
        }

        private void ClearBuyingDetails()
        {
            NameList.Text = string.Empty;
            Gm.Text = string.Empty;
            ClearProductsList();
        }

        private void NameList_TextChanged(object sender, TextChangedEventArgs e)
        {
            a = 1;
        }

        private async void NameProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Products> good = new List<Products>();
            List<Products> product = await Global.proxy.GetProductsBySelectAsync(((TextBox)sender).Name.ToString(), ((TextBox)sender).Text, true);
            foreach (Products p in product)

            {
                if (p.KodCateg.KodCateg == Global.currentCategory.KodCateg)
                    good.Add(p);
            }
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = good;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            products.Remove(p);
        }

        private void lstVProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstVProducts.SelectedIndex != -1)
            {
                p = lstVProducts.SelectedItem as ServiceSuperShop.ProductToBuying;

            }
        }
    }
}
