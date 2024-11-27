using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
    public class ProductToBuyingDB :BaseDB
    {
        
       public ProductToBuyingDB():base("ProductToBuying")
    {
    }
    
            public override BaseEntity CreateModel()
            {
            ProductToBuying item = new ProductToBuying();
                item.Amount = Convert.ToInt32(reader["Amount"].ToString());
                item.KodKindBuying = MyDB.tblKindOfBuying.GetKindOfBuyingByCode(Convert.ToInt32(reader["KodKindBuying"].ToString()));
                item.KodProduct = MyDB.tblProducts.GetProductsByCode(Convert.ToInt32(reader["KodProduct"].ToString()));
            return item;
            

        }
            public List<ProductToBuying> GetList()
            {
                return base.list.Cast<ProductToBuying>().ToList();
            }
        public ProductToBuying GetProductToBuyingByCode(int code1,int code2)
        {

            return GetList().FirstOrDefault(x => x.KodKindBuying.KodKindBuying == code1
            && x.KodProduct.KodProduct== code2);
        }
        public List<ProductToBuying> GetProductToBuyingBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<ProductToBuying>().ToList();
        }
    }
    }
