using SuperShopClient.ServiceSuperShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Devices.Lights;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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
    public sealed partial class AddAmountProductPage : Page
    {

        public AddAmountProductPage()
        {
            this.InitializeComponent();

        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Products p = Global.currentProduct;
            NameP.Text = p.NameProduct;
            AmountMlay.Text += Global.currentProduct.AmountMlay.ToString();
            AmountGmMlay.Text += Global.currentProduct.AmountGmMlay.ToString();
            AmountGmBag.Text += Global.currentProduct.AmountGmBag.ToString();
            kindamountproduct.ItemsSource = await Global.proxy.GetListKindAmountProductAsync();
            kindamountproduct.DisplayMemberPath = "NameKindAmount";
            kindamountproduct.SelectedValuePath = "KodKindProduct";
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            KindAmountProduct k = kindamountproduct.SelectedItem as KindAmountProduct;
            Products products = Global.currentProduct;
            if (products.AmountGmMlay - Convert.ToDouble(amount.Text) * k.Grams > 0)
            {
                await Global.proxy.UpdateProductAmountAsync(Global.currentProduct, -Convert.ToDouble(amount.Text), kindamountproduct.SelectedItem as KindAmountProduct);
                ContentDialog dialog = new ContentDialog()
                {
                    Content = "המוצר עודכן בהצלחה!",
                    CloseButtonText = "OK"
                };
                this.Frame.GoBack();
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
                    this.Frame.GoBack();

                }

            }
        }
    }
    }


