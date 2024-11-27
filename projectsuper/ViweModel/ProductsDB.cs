using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductsDB : BaseDB
    {

        public ProductsDB() : base("Products")
        {

        }
        public override BaseEntity CreateModel()
        {
            Products item = new Products
            {
                NameProduct = reader["NameProduct"].ToString(),
                KodProduct = Convert.ToInt32(reader["KodProduct"].ToString()),
                AmountMlay = Convert.ToInt32(reader["AmountMlay"].ToString()),
                AmountGmMlay = Convert.ToInt32(reader["AmountGmMlay"].ToString()),
                AmountGmBag = Convert.ToInt32(reader["AmountGmBag"].ToString()),
                Picture = reader["Picture"].ToString(),
                KodCateg = MyDB.tblCategory.GetCategoryByCode(Convert.ToInt32(reader["KodCateg"].ToString())),
                Status = (bool)reader["Status"]
            };
            return item;
        }
        public List<Products> GetListProducts()
        {
            return list.Cast<Products>().ToList();
        }
        public Products GetProductsByCode(int code)
        {
            return GetListProducts().FirstOrDefault(x => x.KodProduct == code);
        }
        public List<Products> GetProductsBySelect(string nameField, string st, bool contains)
        {
            return GetListBySelect(nameField, st, contains).Cast<Products>().ToList();
        }

        public List<Products> GetProductsExistsBySelect(string nameField, string st, bool contains)
        {
            return GetListBySelect(nameField, st, contains)
                .Cast<Products>().Where(product => product.Status).ToList();
        }
    }

}
