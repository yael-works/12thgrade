using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
  public  class KindOfBuyingDB:BaseDB
    {
        public KindOfBuyingDB() : base("KindOfBuying")
        {

        }
        public override BaseEntity CreateModel()
        {
            KindOfBuying item = new KindOfBuying();
            item.KodKindBuying = Convert.ToInt32(reader["KodKindBuying"].ToString());
            item.NameKindBuying = reader["NameKindBuying"].ToString();
            return item;
        }
        public List<KindOfBuying> GetList()
        {
            return base.list.Cast<KindOfBuying>().ToList();
        }
        public KindOfBuying GetKindOfBuyingByCode(int code)
        {

            return GetList().FirstOrDefault(x => x.KodKindBuying == code);
        }
        public List<KindOfBuying> GetKindOfBuyingBySelect(string nameField, string st , bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<KindOfBuying>().ToList();
        }
    }
}
