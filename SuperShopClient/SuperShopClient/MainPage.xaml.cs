﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SuperShopClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

      
        private  void txt2_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Child));
           
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Global.proxy.HithulAsync();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(sismaa.Text=="100")
                this.Frame.Navigate(typeof(Tafrit));
            else {
                fl2.Hide();
            ContentDialog dialog = new ContentDialog()
            {
                Content = "הסיסמא שהוקשה שגויה",
                CloseButtonText = "ביטול"
            };
            await dialog.ShowAsync();
            }
        }

     
    }
}
