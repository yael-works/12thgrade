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
    public sealed partial class NewRecipe : Page
    {
        private List<ProductToRecipe> products=new List<ProductToRecipe>();
        ProductToRecipe p = null;
        int b;
        int a = 0;
        int c=0;
        public NewRecipe()
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

       

        private async void KodCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = KodCateg.SelectedItem as Category;
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = (await Global.proxy.GetListProductsExistsAsync())
                .Where(p => p.KodCateg.KodCateg == category.KodCateg);
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
           
            if (lstV2.SelectedIndex != -1)
            {
                Global.currentProduct = lstV2.SelectedItem as ServiceSuperShop.Products;
                Fill(Global.currentProduct);
            }

          
            
        }
        private void Fill(Products products)
        {
            NameProduct.Text = products.NameProduct.ToString();
        }

        //private  void KodKindAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string nameField;
        //    object st = null;
        //    int a;
        //    nameField = ((ComboBox)sender).Name;
        //    object obj = ((ComboBox)sender).SelectedItem;
        //    st = obj.GetType().GetProperty(nameField).GetValue(obj, null);
        //    a = Convert.ToInt32(st);
        //    //Global.currentKindAmount = await Global.proxy.GetKindAmountByCodeAsync(a);



        //}

        private  void btt8_Click(object sender, RoutedEventArgs e)
        {
            KindAmountProduct k = KodKindAmount.SelectedItem as KindAmountProduct;
            double y = k.Grams;
            double x = Convert.ToDouble(Grams.Text);
            if (k.KodKindProduct == 5)
                y = Global.currentProduct.AmountGmBag;
            bool b = false;
            foreach(ProductToRecipe p in products)
                if(p.KodProduct.KodProduct== Global.currentProduct.KodProduct)
                    b = true;
            if(b==false)
            { 
            ProductToRecipe productToRecipe = new ProductToRecipe()
            {
                AmountGrams =y*x,
                KodProduct = Global.currentProduct,
                KodRecipe=Global.currentRecipe
            };
            products.Add(productToRecipe);
            RefreshRecipeList();
            }
            else
            {
                foreach (ProductToRecipe p in products)
                    if (p.KodProduct.KodProduct == Global.currentProduct.KodProduct)
                        p.AmountGrams += y * x;
                RefreshRecipeList();
            }
            KodKindAmount.SelectedItem = null;
            NameProduct.Text = string.Empty;
            Grams.Text = string.Empty;
        }
        private void RefreshRecipeList()
        {
            lstV3.ItemsSource = null;
            lstV3.ItemsSource = products;
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
           if (a==0||c==0)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "יש למלא את כל השדות!",
                    CloseButtonText = "OK"
                };
            await dialog.ShowAsync();
            }
            else
            { 
            Global.currentRecipe = new Recipe()
            {
                KodRecipe = await Global.proxy.GetNextKeyRecipeAsync(),
                NameRecipe = NameRecipe.Text ,
                AmountMana=Convert.ToInt32(AmountManots.Text) ,
                Status=true
            };
            await Global.proxy.AddRecipeAsync(Global.currentRecipe);
            foreach (ProductToRecipe product in products)
            {
                product.KodRecipe = Global.currentRecipe; // קוד מתכון
                await Global.proxy.AddProductToRecipeAsync(product);
            }
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "המתכון נוסף בהצלחה!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
                this.Frame.GoBack();
            }

        }
        private void ClearRecipeDetails()
        {
            NameRecipe.Text = string.Empty;
            NameProduct.Text = string.Empty;
            ClearProductsList();
        }

        //private async void Shmor_Click(object sender, RoutedEventArgs e)
        //{
        //    Global.currentRecipe = new Recipe()
        //    {
        //        KodRecipe = await Global.proxy.GetNextKeyRecipeAsync(),
        //        NameRecipe = NameRecipe.Text,
        //        AmountMana = Convert.ToInt32(AmountManots.Text),
        //        Status = true
        //    };
        //    await Global.proxy.AddRecipeAsync(Global.currentRecipe);


        //}

        private  void btt10_Click(object sender, RoutedEventArgs e)
        {
            products.Remove(p);

            RefreshRecipeList();

        }

       

        private void lstV3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstV3.SelectedIndex != -1)
            {
               p = lstV3.SelectedItem as ServiceSuperShop.ProductToRecipe;

            }
        }

        private void AmountManots_TextChanged(object sender, TextChangedEventArgs e)
        {
            a = 1;
        }

        private void NameRecipe_TextChanged(object sender, TextChangedEventArgs e)
        {
            c = 1;
        }
    }


}
