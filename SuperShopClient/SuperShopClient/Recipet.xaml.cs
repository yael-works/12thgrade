using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public sealed partial class Recipet : Page
    {
        List<ProductToRecipe> lstP=new List<ProductToRecipe>();
        bool b = false;
        public Recipet()
        {
            this.InitializeComponent();

        }

        private void btt5_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewRecipe));
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



        private async void Kodproduct_TextChanged(object sender, TextChangedEventArgs e)
        {
        
            List<Recipe> good = new List<Recipe>();
            List<Recipe> a = await Global.proxy.GetRecipeBySelectAsync(((TextBox)sender).Name.ToString(), ((TextBox)sender).Text, true);
            foreach(Recipe p in a)
            {
                if(p.Status==true)
                    good.Add(p);

            }
            lstV2.ItemsSource=good;
        }


        private async  void ProductToRecipe_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (Global.currentRecipe!=null)

            {
                lstP = await Global.proxy.GetProductToRecipeBySelectAsync("KodRecipe", Global.currentRecipe.KodRecipe.ToString(),false);
                lstP = lstP.Where(w => w.KodProduct.NameProduct.StartsWith(KodProduct.Text)).ToList();
                lstV3.ItemsSource=lstP;
            }
           

        }

        private async void btt8_Click(object sender, RoutedEventArgs e)
        {
            bool flagMake = true;//הודעה על ביצוע מתכון
            bool flag1 = true;//האם אין מספיק כמות במלאי
            bool flag = true;//האם מוצר מחוק
            string st = " ";
            string st1 =" ";
            List<ProductToRecipe> list = await Global.proxy.GetProductToRecipeBySelectAsync("KodRecipe", Global.currentRecipe.KodRecipe.ToString(),false);
          //בדיקת אם ניתן להוריד מהמלאי
            foreach (object item in list)
            {
                ProductToRecipe p = (ProductToRecipe)item;
                if (p.KodProduct.Status == false)
                { 
                    flag = false;
                    st1 = st1 + " " + p.KodProduct.NameProduct;
                    flagMake = false;
                }

                else 
                    if((p.AmountGrams * Convert.ToInt32(AmountManots.Text)) > p.KodProduct.AmountGmMlay)
                { 
                    flag1 = false;
                    st =st+" "+ p.KodProduct.NameProduct;
                    flagMake = false;
                }
               
            }
            //הורדה מהמלאי
            if (flag==true&&flag1==true)
            {
                foreach (object item in list)
                {
                    for (int i = Convert.ToInt32(AmountManots.Text); i > 0; i--)
                    {
                        ProductToRecipe p = (ProductToRecipe)item;

                        Global.currentProduct = p.KodProduct;
                        Global.currentProduct.AmountGmMlay -= p.AmountGrams;
                    }
                        int b;
                        double a = Global.currentProduct.AmountGmMlay;
                        b =(int) (a / Global.currentProduct.AmountGmBag);
                      
                        Global.currentProduct.AmountMlay =b;
                        await Global.proxy.UpdateProductAsync(Global.currentProduct);
                      
                    
                }

            }
            //הודעה למוצרים שמחוקים 
            if (flag == false && flag1 == true)
            {

                ContentDialog dialog = new ContentDialog()
                {
                    Content = "המוצרים:" +" " +st1 + " "+"מחוקים אצלך שחזר אותם כדי לעדכנם במלאי!",
                    CloseButtonText = "ביטול"
                };
                await dialog.ShowAsync();
            }
            //הודעה למוצרים שלא קיימים  בכמות במלאי
           else
            if (flag1==false)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Content = "המוצרים:"+" " + st +" "+ "אינם נמצאים בכמות מספיקה במלאי!",
                        CloseButtonText = "ביטול"
                    };
                    await dialog.ShowAsync();
                }
           
       //הודעה על ביצוע מתכון
          if(flagMake==true)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "המתכון בוצע בהצלחה!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

      

        private async void lstV2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
           
           Global.currentRecipe=lstV2.SelectedItem as Recipe;
            if (lstV2.SelectedItem != null)
            {
                Global.currentRecipe = (Recipe)lstV2.SelectedItem;
                lstV3.ItemsSource = await Global.proxy.GetProductToRecipeBySelectAsync("KodRecipe", Global.currentRecipe.KodRecipe.ToString(),false);
                lstP = await Global.proxy.GetProductToRecipeBySelectAsync("KodRecipe", Global.currentRecipe.KodRecipe.ToString(), false);

            }
            if (b == false) {
            AmountManots.Text = Global.currentRecipe.AmountMana.ToString();
            b= true;
            }
        }

        private async void AllRecipies_Click(object sender, RoutedEventArgs e)
        {
            NameRecipe.Visibility = Visibility.Visible;
            lstVStatus.Visibility = Visibility.Collapsed;
            lstV2.Visibility = Visibility.Visible;
            List<Recipe> list = await Global.proxy.GetListRecipeAsync();
            List<Recipe> good = new List<Recipe>();
            foreach (Recipe recipe in list)
            {
                if (recipe.Status == true)
                    good.Add(recipe);
            }
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = good;

        }

        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void lstV3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Global.currentRecipe.Status = false;
            await Global.proxy.UpdateRecipeAsync(Global.currentRecipe);
            List<Recipe> list = await Global.proxy.GetListRecipeAsync();
            List<Recipe> good = new List<Recipe>();
            foreach (Recipe recipe in list)
            {
                if(recipe.Status==true)
                    good.Add(recipe);
            }
            lstV2.ItemsSource = null;
            lstV2.ItemsSource = good;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
          
            NameRecipe.Visibility = Visibility.Collapsed;
            lstV2.Visibility = Visibility.Collapsed;
            lstVStatus.Visibility = Visibility.Visible;
            lstV2.Visibility = Visibility.Collapsed;
            lstVStatus.ItemsSource = await Global.proxy.GetRecipeBySelectAsync("Status", "False", false);
          
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            int Code = Convert.ToInt32(checkBox.Tag);
            Global.currentRecipe = await Global.proxy.GetRecipeByCodeAsync(Code);
            Global.currentRecipe = new ServiceSuperShop.Recipe()
            {
                AmountMana=Global.currentRecipe.AmountMana,
                KodRecipe=Global.currentRecipe.KodRecipe,
                NameRecipe=Global.currentRecipe.NameRecipe,
                Status = true
            };
            await Global.proxy.UpdateRecipeAsync(Global.currentRecipe);
            RefreshProductsListStatus();
        }
        private async void RefreshProductsListStatus()
        {

            List<Recipe> r= new List<Recipe>();
            List<Recipe> good = new List<Recipe>();
            lstVStatus.ItemsSource = await Global.proxy.GetRecipeBySelectAsync("Status", "False", false);
           
        }
    }
}

