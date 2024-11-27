using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SuperShopClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewProduct : Page
    {

        StorageFile file;
        BitmapImage image = new BitmapImage();
        int a = 0;
        public AddNewProduct()
        {
            this.InitializeComponent();


        }

        private async void newProduct_Click(object sender, RoutedEventArgs e)
        {
            if (NameProduct.Text == "" || Amountgmbag.Text == "" || AmountMlay.Text == "")
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "יש למלא את כל השדות",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else
            {
                if (file == null)

                    a = 1;
                this.AddProduct();
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "המוצר נשמר בהצלחה!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
                this.Frame.GoBack();
            }

        }

        private async void AddProduct()
        {
           
            if (a==1)
            {
                Global.currentProduct = new ServiceSuperShop.Products()
                {
                    AmountGmBag = Convert.ToInt32(Amountgmbag.Text),
                    AmountMlay = Convert.ToInt32(AmountMlay.Text),
                    Status = true,
                    AmountGmMlay = Convert.ToInt32(AmountMlay.Text) * Convert.ToInt32(Amountgmbag.Text),
                    KodCateg = Global.currentCategory,
                    NameProduct = NameProduct.Text,
                    KodProduct = Convert.ToInt32(KodProduct.Text),
                    Picture = ""
                };
                await Global.proxy.AddProductAsync(Global.currentProduct);
            }
            else
            {

            Global.currentProduct = new ServiceSuperShop.Products()
            {
                AmountGmBag = Convert.ToInt32(Amountgmbag.Text),
                AmountMlay = Convert.ToInt32(AmountMlay.Text),
                Status = true,
                AmountGmMlay = Convert.ToInt32(AmountMlay.Text) * Convert.ToInt32(Amountgmbag.Text),
                KodCateg = Global.currentCategory,
                NameProduct = NameProduct.Text,
                KodProduct = Convert.ToInt32(KodProduct.Text),
                Picture = file.Name,
            };
            await Global.proxy.AddProductAsync(Global.currentProduct);
            }
            Global.currentProductToRecipe = new ServiceSuperShop.ProductToRecipe()
            {

                KodProduct = Global.currentProduct,
                AmountGrams = 0,
                KodRecipe = Global.currentRecipe
            };
            await Global.proxy.AddProductToRecipeAsync(Global.currentProductToRecipe);

        }
        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Category g = Global.currentCategory;
            string st = g.Nameteur;
            txtCategoryName.Text = st;

            KodProduct.Text = (await Global.proxy.GetNextKeyProductAsync()).ToString();

        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }



        private async void btt33_Click(object sender, RoutedEventArgs e)
        {


            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                image = new BitmapImage(new Uri("ms-appx://" + file.Path));

            }
            showPic();
            SaveP();

        }
        private void showPic()
        {
            img1.Source = image;
            img1.Visibility = Visibility.Visible;
            img1.FlowDirection = FlowDirection.RightToLeft;

        }

        private async void SaveP()
        {

            StorageFolder folder;
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            //int x = root.IndexOf(@"\bin");
            //root = root.Substring(0, x) + @"\picture\";
            // string path = root;
            string path = root + @"\Assets\picture\";
            folder = await StorageFolder.GetFolderFromPathAsync(path);
            try
            {
                await file.CopyAsync(folder);
                img1.Source = image;
            }
            catch
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "התמונה כבר קיימת!",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }


        }

    }
}



