using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;

namespace SuperShopClient
{
    public class Global
    {
        public static ServiceSuperShop.SuperServiceClient proxy = new ServiceSuperShop.SuperServiceClient();
        public static ServiceSuperShop.Products currentProduct;
        public static ServiceSuperShop.Recipe currentRecipe;
        public static ServiceSuperShop.UsesProduct currentUsesProduct;
        public static ServiceSuperShop.ProductToRecipe currentProductToRecipe;
        public static ServiceSuperShop.ProductToBuying currentProductToBuying;
        public static ServiceSuperShop.KindOfBuying currentKindOfBuying;
        public static ServiceSuperShop.KindAmountProduct currentKindAmountProduct;
        public static ServiceSuperShop.Category currentCategory;
        public static ServiceSuperShop.Summery currentSummery;
        public async static void ConvertByteArrayToImage(byte[] myByterArayy, BitmapImage img)
        {
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(myByterArayy.AsBuffer());
                stream.Seek(0);
                await img.SetSourceAsync(stream);
            }
        }

        public static byte[] GetImage(string fileName)
        {
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\picture\" + fileName;
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            return null;
        }
    }
}
