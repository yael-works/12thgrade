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
    public sealed partial class AddNewCategory : Page
    {
        private int kod = 0;
        public AddNewCategory()
        {
            this.InitializeComponent();
        }

        public async void AddCategory()
        {
            Global.currentCategory = new ServiceSuperShop.Category()
            {
                
              KodCateg= kod,
              Nameteur= Nameteur.Text,
            
            };
            await Global.proxy.AddCategoryAsync(Global.currentCategory);

        }
        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            this.AddCategory();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            kod = await Global.proxy.GetNextKeyCategoryAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
