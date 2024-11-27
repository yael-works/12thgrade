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
    public sealed partial class Tafrit : Page
    {
        public Tafrit()
        {
            this.InitializeComponent();
        }

        private void menu1_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == t2)
                f1.Navigate(typeof(Productss));
            if (sender.SelectedItem == t3)
                f1.Navigate(typeof(UsesProductPage));
            if (sender.SelectedItem == t4)
                f1.Navigate(typeof(MissProducts));
            if (sender.SelectedItem == t5)
                f1.Navigate(typeof(KindOfBuyingg));
            if (sender.SelectedItem == t6)
                f1.Navigate(typeof(Recipet));
         
        }

        private void menu1_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
