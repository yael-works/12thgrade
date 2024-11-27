using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections;
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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SuperShopClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Child : Page
    {
        List<StackPanel> stackPanels = new List<StackPanel>();
        List<Products> stockList = new List<Products>();
        List<Products> selectList = new List<Products>();
        ComboBox c;
        KindAmountProduct e1;
        Products x = new Products();
        double i1;
        public Child()
        {
            this.InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            KodCateg.ItemsSource = await Global.proxy.GetListCategoryAsync();
            KodCateg.DisplayMemberPath = "Nameteur";
            KodCateg.SelectedValuePath = "KodCateg";
            kindamountproduct.ItemsSource = await Global.proxy.GetListKindAmountProductAsync();
            kindamountproduct.DisplayMemberPath = "NameKindAmount";
            kindamountproduct.SelectedValuePath = "KodKindProduct";
        }



        private async void KodCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // הקטגוריה שנבחרה
            Global.currentCategory = ((ComboBox)sender).SelectedItem as Category;
            // כל המוצרים בקטגוריה זו
            List<Products> products = await Global.proxy.GetProductsExistsBySelectAsync("KodCateg", Global.currentCategory.KodCateg.ToString(), false);

            productsPanel.Children.Clear();
            // יצירת פקד דינאמי לכל מוצר
            foreach (var product in products)
            {
                productsPanel.Children.Add(CreateProductView(product));
            }
        }

        private StackPanel CreateProductView(Products product)
        {
            StackPanel panel = new StackPanel { Width =300, Height = 270 };
            TextBlock textBlock = new TextBlock() { Text = product.NameProduct };
            byte[] arrPic = Global.GetImage(product.Picture);
            BitmapImage img = new BitmapImage();
            if (arrPic != null)
            {
                Global.ConvertByteArrayToImage(arrPic, img);
            }
            Image image = new Image();
            image.Source = img;
            image.Margin = new Thickness(20, 20, 20, 20);
            image.Height = 100;
            image.Width = 100;
            Button b1 = new Button { Content = "שימוש", Width = 150, Height = 70, Tag = product };
            b1.Name = "b" + product.KodProduct.ToString();
            b1.Click += Button1_Click;
            b1.Margin = new Thickness(20, 20, 20, 20);
            panel.Children.Add(textBlock);
            panel.Children.Add(image); 
            panel.Children.Add(b1);
            return panel;
        }
        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string n = b.Name.Substring(1);

            Global.currentProduct = await Global.proxy.GetProductByCodeAsync(Convert.ToInt32(n));
            fl2.ShowAt(b);
        }


        private  void panel_SelectedItem(object sender, RoutedEventArgs e)
        {
            Products product = (sender as Button).Tag as Products;
            x = product;
        }

        private void textBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            KindAmountProduct kind = (sender as ComboBox).Tag as KindAmountProduct;
            e1 = kind;

        }
        private  void textBox2_TextChanged(object sender, RoutedEventArgs e)
        {
            double i = (double)((sender as TextBox).Tag);
            i1 = i;
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void btnUpdate_Click_1(object sender, RoutedEventArgs e)
        {
            KindAmountProduct k = kindamountproduct.SelectedItem as KindAmountProduct;
            Products products = Global.currentProduct;
            if (products.AmountGmMlay - Convert.ToDouble(amount.Text) * k.Grams > 0)
            {
                await Global.proxy.UpdateProductAmountAsync(Global.currentProduct, -Convert.ToDouble(amount.Text), kindamountproduct.SelectedItem as KindAmountProduct);
                kindamountproduct.SelectedItem = null;
                amount.Text = "" ;
                fl2.Hide();
            }
            else
            {
                if (products.AmountGmMlay - Convert.ToDouble(amount.Text) * k.Grams < 0)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Content = "לא ניתן לבצע פעולה זו  מפני חריגה בכמות גרם במלאי",
                        CloseButtonText = "ביטול"
                    };

                    await dialog.ShowAsync();

                    fl2.Hide();
                }
            }
        }

       
    }
}
