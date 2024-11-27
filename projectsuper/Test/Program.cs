using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;
namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            print(MyDB.tblProducts.GetListProducts().Cast<BaseEntity>().ToList());
            print(MyDB.tblProductToRecipe.GetList().Cast<BaseEntity>().ToList());
            print(MyDB.tblProductToBuying.GetList().Cast<BaseEntity>().ToList());
        }
        public static void print(List<BaseEntity> bs)
        {
            foreach(BaseEntity item in bs)
                   Console.WriteLine(item);
            Console.WriteLine("-----------------------");
            Console.ReadLine();
        }
    }
}
