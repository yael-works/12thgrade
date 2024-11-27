using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
    public class KindAmountProductDB : BaseDB
    {
        public KindAmountProductDB() : base("KindAmountProduct")
        { }
        public override BaseEntity CreateModel()
        {
            KindAmountProduct item = new KindAmountProduct
            {
                KodKindProduct = Convert.ToInt32(reader["KodKindProduct"].ToString()),
                NameKindAmount = reader["NameKindAmount"].ToString(),
                Grams = Convert.ToDouble(reader["Grams"].ToString())
            };
            return item;

        }
        public List<KindAmountProduct> GetList()
        {
            return base.list.Cast<KindAmountProduct>().ToList();
        }
        public KindAmountProduct GetKindAmountProductByCode(int code)
        {
            return GetList().FirstOrDefault(x => x.KodKindProduct == code);
        }
        public List<KindAmountProduct> GetKindAmountProductBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<KindAmountProduct>().ToList();
        }
    }
}
