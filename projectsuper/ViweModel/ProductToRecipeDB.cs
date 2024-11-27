using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
    public class ProductToRecipeDB : BaseDB
    {

        public ProductToRecipeDB() : base("ProductToRecipe")
        {

        }
        public override BaseEntity CreateModel()
        {
            ProductToRecipe item = new ProductToRecipe
            {
                KodProduct = MyDB.tblProducts.GetProductsByCode(Convert.ToInt32(reader["KodProduct"].ToString())),
                KodRecipe = MyDB.tblRecipe.GetRecipeByCode(Convert.ToInt32(reader["KodRecipe"].ToString())),
                AmountGrams = Convert.ToDouble(reader["AmountGrams"].ToString())
            };
            return item;



        }
        public List<ProductToRecipe> GetList()
        {
            return base.list.Cast<ProductToRecipe>().ToList();
        }
        public ProductToRecipe GetProductToRecipeByCode(int code1, int code2)
        {

            return GetList().FirstOrDefault(x => x.KodProduct.KodProduct == code1 && x.KodRecipe.KodRecipe == code2);
        }
        public List<ProductToRecipe> GetProductToRecipeBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<ProductToRecipe>().ToList();
        }
    }
}
