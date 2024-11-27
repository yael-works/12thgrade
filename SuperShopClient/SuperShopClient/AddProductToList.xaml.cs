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
    public sealed partial class AddProductToList : Page
    {
        private List<ProductToRecipe> products;
        int b;
        public AddProductToList()
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void KodCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string nameField;
            //object st = null;
            //nameField = ((ComboBox)sender).Name;
            //object obj = ((ComboBox)sender).SelectedItem;
            //st = obj.GetType().GetProperty(nameField).GetValue(obj, null);
            //lstV2.ItemsSource = await Global.proxy.GetProductsBySelectAsync(nameField, st.ToString());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                KodCateg.ItemsSource = await Global.proxy.GetListCategoryAsync();
                KodCateg.DisplayMemberPath = "Nameteur";
                KodCateg.SelectedValuePath = "KodCateg";
                KodKindAmount.ItemsSource = await Global.proxy.GetListKindAmountProductAsync();
                KodKindAmount.DisplayMemberPath = "NameKindAmount";
                KodKindAmount.SelectedValuePath = "KodKindProduct";
            }
            catch
            {
                Console.WriteLine();
            }
        }

        private void lstV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lstV2.SelectedIndex != -1)
                {
                    Global.currentProduct = lstV2.SelectedItem as ServiceSuperShop.Products;
                    Fill(Global.currentProduct);
                }

            }
            catch
            {
                Console.WriteLine();
            }
        }
        private void Fill(Products products)
        {
            NameProduct.Text = products.NameProduct.ToString();
        }

        private async void KodKindAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameField;
            object st = null;
            int a;
            nameField = ((ComboBox)sender).Name;
            object obj = ((ComboBox)sender).SelectedItem;
            st = obj.GetType().GetProperty(nameField).GetValue(obj, null);
            a = Convert.ToInt32(st);
            //Global.currentKindAmount = await Global.proxy.GetKindAmountByCodeAsync(a);



        }

        private async void btt8_Click(object sender, RoutedEventArgs e)
        {
            //int a = Global.currentKindAmount.KodKindAmount;
            //int kindAmount = 0;
            //if (a == 1)
            //    kindAmount = 25;
            //else if (a == 2)
            //    kindAmount = 10;
            //else if (a == 3)
            //    kindAmount = 100;
            //else if (a == 4)
            //    kindAmount = 1;
            //Global.currentProduct = lstV2.SelectedItem as Products;
            //Global.currentProductToRecipe = new ProductToRecipe()
            //{
            //    Amount = Convert.ToInt32(Grams.Text) * kindAmount,
            //    KodProduct = Global.currentProduct,
            //    KodKindAmount = Global.currentKindAmount,
            //    KodRecipe = Global.currentRecipe
            //};
            await Global.proxy.AddProductToRecipeAsync(Global.currentProductToRecipe);
            RefreshProductsList();

            //Global.currentKindAmountProduct = new ServiceSuperShop.KindAmountProduct()
            //{
            //    KodKindProduct = await Global.proxy.GetNextKeyKindAmountProductAsync() ,
            //    NameKindAmount=
            //    Grams = Convert.ToInt32(Grams.Text)
            //};
            //await Global.proxy.AddkindAmountProductAsync(Global.currentKindAmountProduct);

        }
        private async void RefreshProductsList()
        {
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = await Global.proxy.GetListProductToRecipeAsync();
        }
        private void ClearProductsList()
        {
            products = new List<ProductToRecipe>();
            RefreshProductsList();
        }

        private async void btt9_Click(object sender, RoutedEventArgs e)
        {

            //Global.currentRecipe = new Recipe()
            //{
            //    KodREcipe = await Global.proxy.GetNextKeyRecipeAsync(),
            //   NameRecipe= NameRecipe.Text ,
            //   AmountMana=Convert.ToInt32( AmountManots.Text) ,
            //   Status=true 
            //};
            //await Global.proxy.AddRecipeAsync(Global.currentRecipe);

            //foreach (ProductToRecipe product in products)
            //{
            //    product.KodProduct = Global.currentProduct;
            //    await Global.proxy.AddProductToRecipeAsync(product);
            //}
            //ClearBuyingDetails();
        }
        private void ClearBuyingDetails()
        {
            NameRecipe.Text = string.Empty;
            NameProduct.Text = string.Empty;
            KodCateg.SelectedItem = null;
            ClearProductsList();
        }

        private async void Shmor_Click(object sender, RoutedEventArgs e)
        {
            Global.currentRecipe = new Recipe()
            {
                KodRecipe = await Global.proxy.GetNextKeyRecipeAsync(),
                NameRecipe = NameRecipe.Text,
                AmountMana = Convert.ToInt32(AmountManots.Text),
                Status = true
            };
            await Global.proxy.AddRecipeAsync(Global.currentRecipe);


        }
    }
    }

