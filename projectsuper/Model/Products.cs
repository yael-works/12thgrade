using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Products : BaseEntity
    {
        private int kodproduct;
        private string nameproduct;
        private string picture;
        private double amountmlay;
        private double amountgmbag;
        private double amountgmmlay;
        private bool status;
        private Category kodCateg;
        public int KodProduct { get => kodproduct; set => kodproduct = value; }
        public string NameProduct { get => nameproduct; set => nameproduct = value; }
        public string Picture { get => picture; set => picture = value; }
        public double AmountMlay { get => amountmlay; set => amountmlay = value; }
        public double AmountGmBag { get => amountgmbag; set => amountgmbag = value; }
        public double AmountGmMlay { get => amountgmmlay; set => amountgmmlay = value; }
        public bool Status { get => status; set => status = value; }
        public Category KodCateg { get => kodCateg; set => kodCateg = value; }

        public override bool Equals(object obj)
        {
            return obj is Products products &&
                   kodproduct == products.kodproduct;
        }

        public override int GetHashCode()
        {
            return 243943472 + kodproduct.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodProduct" };
        }

        public override string GetTableName()
        {
            return "Products";
        }
        public override string ToString()
        {
            return kodproduct.ToString();
        }

        public static bool operator ==(Products left, Products right)
        {
            return EqualityComparer<Products>.Default.Equals(left, right);
        }

        public static bool operator !=(Products left, Products right)
        {
            return !(left == right);
        }
    }
}
