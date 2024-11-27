using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class KindAmount : BaseEntity
    {
        private int kodKindAmount;
        private string namekindamount;

      
        public string NameKindAmount { get => namekindamount; set => namekindamount = value; }
        public int KodKindAmount { get => kodKindAmount; set => kodKindAmount = value; }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodKindAmount" };
        }

        public override string GetTableName()
        {
            return "KindAmount";
        }
        public override string ToString()
        {
            return kodKindAmount.ToString();
        }
    }
}
