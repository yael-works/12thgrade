using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
    public class UsesProductDB : BaseDB
    {
        public UsesProductDB() : base("UsesProduct")
        {

        }
        public override BaseEntity CreateModel()
        {
            UsesProduct item = new UsesProduct
            {
                KodUse = Convert.ToInt32(reader["KodUse"].ToString()),
                KodProduct = MyDB.tblProducts.GetProductsByCode(Convert.ToInt32(reader["KodProduct"].ToString())),
                Amount = Convert.ToDouble(reader["Amount"].ToString()),
                DateUse = Convert.ToDateTime(reader["DateUse"].ToString())
            };
            return item;
        }
        public List<UsesProduct> GetList()
        {
            return base.list.Cast<UsesProduct>().ToList();
        }
        public UsesProduct GetUsesProductByCode(int code)
        {
            return GetList().FirstOrDefault(x => x.KodUse == code);
        }
        public List<UsesProduct> GetUsesProductBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<UsesProduct>().ToList();
        }
    }
}


