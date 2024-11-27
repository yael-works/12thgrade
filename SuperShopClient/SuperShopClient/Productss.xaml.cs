using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

    public sealed partial class Productss : Page
    {


        public Productss()
        {
            this.InitializeComponent();

        }

        private async void btt5_Click(object sender, RoutedEventArgs e)
        {
            Global.currentCategory = KodCateg.SelectedItem as Category;
            if(Global.currentCategory==null)
            { 
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "יש לבחור קטגוריה!",
                    CloseButtonText = "OK"
                };
            await dialog.ShowAsync();
            }
            else
            this.Frame.Navigate(typeof(AddNewProduct));
        }

      
        private void End_Click(object sender, RoutedEventArgs e)
        {
            End.Visibility = Visibility.Collapsed;
        }



        private void KodCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.currentCategory = KodCateg.SelectedItem as Category;
           
            if (Global.currentCategory != null)
            {

                if (lstVWithoutStatus.Visibility == Visibility.Visible)
                    RefreshProductsList();
                else
                {
                    RefreshProductsListStatus();
                }

                fl1.Hide();
            }
        }

        private async void RefreshProductsList()
        {
            Global.currentCategory = KodCateg.SelectedItem as Category;
            lstVWithoutStatus.ItemsSource = null;
            lstVWithoutStatus.ItemsSource = (await Global.proxy.GetListProductsExistsAsync())
                .Where(p => p.KodCateg.KodCateg == Global.currentCategory.KodCateg);
        }



        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            KodCateg.ItemsSource = await Global.proxy.GetListCategoryAsync();
            KodCateg.DisplayMemberPath = "Nameteur";
            KodCateg.SelectedValuePath = "KodCateg";
            kindamountproduct.ItemsSource = await Global.proxy.GetListKindAmountProductAsync();
            kindamountproduct.DisplayMemberPath = "NameKindAmount";
            kindamountproduct.SelectedValuePath = "KodKindProduct";
            if (Global.currentCategory != null)
                KodCateg.SelectedValue = Global.currentCategory.KodCateg;
           
        }



        private async void Nameproduct_TextChanged(object sender, TextChangedEventArgs e)
        {

            List<Products> good = new List<Products>();
            List<Products> product = await Global.proxy.GetProductsExistsBySelectAsync(((TextBox)sender).Name.ToString(), ((TextBox)sender).Text, true);
            foreach (Products p in product)

            {
                if (p.KodCateg.KodCateg == Global.currentCategory.KodCateg)
                    good.Add(p);
            }
            lstVWithoutStatus.ItemsSource = null;
            lstVWithoutStatus.ItemsSource = good;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void btt3_Click(object sender, RoutedEventArgs e)
        {
            Global.currentProduct = lstVWithoutStatus.SelectedItem as Products;
            if (Global.currentProduct != null)
            { 
              Global.currentProduct.Status = false;
            await Global.proxy.UpdateProductAsync(Global.currentProduct);
            RefreshProductsList();
            }

        }

        private void btt2_Click(object sender, RoutedEventArgs e)
        {
            Global.currentProduct = lstVWithoutStatus.SelectedItem as Products;
            if (Global.currentProduct!=null)
            this.Frame.Navigate(typeof(AddAmountProductPage));

        }

        private async void Shihizur_Click(object sender, RoutedEventArgs e)
        {
            List<Products> products = new List<Products>();
            List<Products> good = new List<Products>();
            NameProduct.Visibility = Visibility.Collapsed;
            KodProduct.Visibility = Visibility.Collapsed;
            if (Global.currentCategory == null) { 
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "בחר קטגוריה!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else {
            products = await Global.proxy.GetProductsBySelectAsync("Status", "False", false);
            foreach (Products p in products)
                if (p.KodCateg.KodCateg == Global.currentCategory.KodCateg)
                    good.Add(p);
            lstVWithStatus.ItemsSource = good;
            lstVWithoutStatus.Visibility = Visibility.Collapsed;
            lstVWithStatus.Visibility = Visibility.Visible;
                    }
        }


        private async void RefreshCategory()
        {
            KodCateg.ItemsSource = await Global.proxy.GetListCategoryAsync();
            KodCateg.DisplayMemberPath = "Nameteur";
            KodCateg.SelectedValuePath = "KodCateg";
        }
        private async void RefreshProductsListStatus()
        {

            List<Products> products = new List<Products>();
            List<Products> good = new List<Products>();

            products = await Global.proxy.GetProductsBySelectAsync("Status", "False", false);
            foreach (Products p in products)
                if (p.KodCateg.KodCateg == Global.currentCategory.KodCateg)
                    good.Add(p);
            lstVWithStatus.ItemsSource = good;
        }
        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox checkBox = sender as CheckBox;
            int productCode = Convert.ToInt32(checkBox.Tag);
            Global.currentProduct = await Global.proxy.GetProductByCodeAsync(productCode);
            Global.currentProduct = new ServiceSuperShop.Products()
            {
                AmountGmBag = Global.currentProduct.AmountGmBag,
                AmountGmMlay = Global.currentProduct.AmountGmMlay,
                AmountMlay = Global.currentProduct.AmountMlay,
                KodCateg = Global.currentProduct.KodCateg,
                KodProduct = Global.currentProduct.KodProduct,
                NameProduct = Global.currentProduct.NameProduct,
                Picture = Global.currentProduct.Picture,
                Status = true
            };
            await Global.proxy.UpdateProductAsync(Global.currentProduct);
            RefreshProductsListStatus();
        }

        private async void Kodproduct_TextChanged(object sender, TextChangedEventArgs e)
        {


            KodProduct.Visibility = Visibility.Visible;
            List<Products> good = new List<Products>();
            List<Products> product = await Global.proxy.GetProductsExistsBySelectAsync(((TextBox)sender).Name.ToString(), ((TextBox)sender).Text, true);
            foreach (Products p in product)

            {
                if (p.KodCateg.KodCateg == Global.currentCategory.KodCateg)
                    good.Add(p);
            }
            lstVWithoutStatus.ItemsSource = null;
            lstVWithoutStatus.ItemsSource = good;
            if (lstVWithoutStatus.ItemsSource == null)
                this.RefreshProductsList();

        }



        private async void c_Click(object sender, RoutedEventArgs e)
        {
            Global.currentCategory = new ServiceSuperShop.Category()
            {

                KodCateg = await Global.proxy.GetNextKeyCategoryAsync(),
                Nameteur = Nameteur.Text,

            };
            await Global.proxy.AddCategoryAsync(Global.currentCategory);
            RefreshCategory();
            fl1.Hide();

        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            Global.currentProduct = lstVWithoutStatus.SelectedItem as Products;
            await Global.proxy.UpdateProductAmountAsync(Global.currentProduct, Convert.ToDouble(amount.Text), kindamountproduct.SelectedItem as KindAmountProduct);
            RefreshProductsList();
            kindamountproduct.SelectedItem = null;
            amount.Text = string.Empty;
            fl2.Hide();
        }
        



        private void refreshStatus_Click(object sender, RoutedEventArgs e)
        {

            KodProduct.Visibility = Visibility.Collapsed;
            NameProduct.Visibility = Visibility.Collapsed;
            lstVWithoutStatus.Visibility = Visibility.Collapsed;
            lstVWithStatus.Visibility = Visibility.Visible;
            this.RefreshProductsListStatus();
        }

        private void refreshWithOutStatus_Click(object sender, RoutedEventArgs e)
        {
            NameProduct.Visibility = Visibility.Visible;
            KodProduct.Visibility = Visibility.Visible;

            if (Global.currentCategory != null)
            {
                lstVWithStatus.Visibility = Visibility.Collapsed;
                lstVWithoutStatus.Visibility = Visibility.Visible;
                this.RefreshProductsList();
            }



        }

        
    }
}
