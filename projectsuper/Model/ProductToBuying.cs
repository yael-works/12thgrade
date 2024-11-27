using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class ProductToBuying : BaseEntity
    {
        private KindOfBuying kodkindbuying;
        private Products kodproduct;
        private int amount;

        
       
        public int Amount { get => amount; set => amount = value; }
        public KindOfBuying KodKindBuying { get => kodkindbuying; set => kodkindbuying = value; }
        public Products KodProduct { get => kodproduct; set => kodproduct = value; }

        public override bool Equals(object obj)
        {
            return obj is ProductToBuying buying &&
                   EqualityComparer<KindOfBuying>.Default.Equals(kodkindbuying, buying.kodkindbuying) &&
                   EqualityComparer<Products>.Default.Equals(kodproduct, buying.kodproduct);
        }

        public override int GetHashCode()
        {
            int hashCode = 1378653579;
            hashCode = hashCode * -1521134295 + EqualityComparer<KindOfBuying>.Default.GetHashCode(kodkindbuying);
            hashCode = hashCode * -1521134295 + EqualityComparer<Products>.Default.GetHashCode(kodproduct);
            return hashCode;
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodKindBuying", "KodProduct" };
        }

        public override string GetTableName()
        {
            return "ProductToBuying";
        }
        public override string ToString()
        {
            return kodkindbuying + "\t" +kodproduct;
        }

        public static bool operator ==(ProductToBuying left, ProductToBuying right)
        {
            return EqualityComparer<ProductToBuying>.Default.Equals(left, right);
        }

        public static bool operator !=(ProductToBuying left, ProductToBuying right)
        {
            return !(left == right);
        }
    }
}
