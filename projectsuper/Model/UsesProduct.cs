using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class UsesProduct : BaseEntity
    {
        private int koduse;
        private Products kodproduct;
        private DateTime dateuse;
        private double amount;

        public int KodUse { get => koduse; set => koduse = value; }

        public DateTime DateUse { get => dateuse; set => dateuse = value; }
        public double Amount { get => amount; set => amount = value; }
        public Products KodProduct { get => kodproduct; set => kodproduct = value; }

        public override bool Equals(object obj)
        {
            return obj is UsesProduct product &&
                   koduse == product.koduse;
        }

        public override int GetHashCode()
        {
            return -272309976 + koduse.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodUse" };
        }

        public override string GetTableName()
        {
            return "UsesProduct";
        }
        public override string ToString()
        {
            return koduse.ToString();
        }

        public static bool operator ==(UsesProduct left, UsesProduct right)
        {
            return EqualityComparer<UsesProduct>.Default.Equals(left, right);
        }

        public static bool operator !=(UsesProduct left, UsesProduct right)
        {
            return !(left == right);
        }
    }
}
