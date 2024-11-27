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
    public sealed partial class UsesProductPage : Page
    {



        public UsesProductPage()
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

      



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Combo.Items.Add("חודש");
            Combo.Items.Add("שנה");
        }

        private async void City_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DateUse.Visibility = Visibility.Visible;
            if (Combo.SelectedItem.ToString() == "חודש")
                DateUse.MonthVisible = true;
            else
                DateUse.MonthVisible = false;

            lstV2.ItemsSource = await Global.proxy.GetUseByTimeAsync(Combo.SelectedItem.ToString(),
            DateUse.Date.Month, DateUse.Date.Year);
        }

        private async void DateUse_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            lstV2.ItemsSource = await Global.proxy.GetUseByTimeAsync(Combo.SelectedItem.ToString(),
            DateUse.Date.Month, DateUse.Date.Year);
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           List<Summery> l= new List<Summery>();
               l= await Global.proxy.GetUseByTimeAsync(Combo.SelectedItem.ToString(),
            DateUse.Date.Month, DateUse.Date.Year);
               l = l.Where(w => w.ProductName.StartsWith(NameProduct.Text)).ToList();
                lstV2.ItemsSource = l;
            
        }

        private async void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateUse.Visibility = Visibility.Visible;
            if (Combo.SelectedItem.ToString() == "חודש")
                DateUse.MonthVisible = true;
            else
                DateUse.MonthVisible = false;

            lstV2.ItemsSource = await Global.proxy.GetUseByTimeAsync(Combo.SelectedItem.ToString(),
            DateUse.Date.Month, DateUse.Date.Year);
        }
    }
}
