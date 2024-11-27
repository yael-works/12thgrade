using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class KindAmountProduct : BaseEntity
    {
        private int kodKindProduct;
        private string nameKindAmount;
        private double grams;

        public int KodKindProduct { get => kodKindProduct; set => kodKindProduct = value; }
        public string NameKindAmount { get => nameKindAmount; set => nameKindAmount = value; }
        public double Grams { get => grams; set => grams = value; }

        public override bool Equals(object obj)
        {
            return obj is KindAmountProduct product &&
                   kodKindProduct == product.kodKindProduct;
        }

        public override int GetHashCode()
        {
            return 1275406686 + kodKindProduct.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodkindProduct" };
        }

        public override string GetTableName()
        {
            return "KindAmountProduct";
        }
        public override string ToString()
        {
            return kodKindProduct.ToString();
        }

        public static bool operator ==(KindAmountProduct left, KindAmountProduct right)
        {
            return EqualityComparer<KindAmountProduct>.Default.Equals(left, right);
        }

        public static bool operator !=(KindAmountProduct left, KindAmountProduct right)
        {
            return !(left == right);
        }
    }
}
