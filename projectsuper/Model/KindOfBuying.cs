using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class KindOfBuying : BaseEntity
    {
        private int kodkindbuying;
        private string namekindbuying;

        public int KodKindBuying { get => kodkindbuying; set => kodkindbuying = value; }
        public string NameKindBuying { get => namekindbuying; set => namekindbuying = value; }

        public override bool Equals(object obj)
        {
            return obj is KindOfBuying buying &&
                   kodkindbuying == buying.kodkindbuying;
        }

        public override int GetHashCode()
        {
            return 124729563 + kodkindbuying.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodKindBuying" };
        }

        public override string GetTableName()
        {
            return "KindOfBuying";
        }
        public override string ToString()
        {
            return kodkindbuying.ToString();
        }

        public static bool operator ==(KindOfBuying left, KindOfBuying right)
        {
            return EqualityComparer<KindOfBuying>.Default.Equals(left, right);
        }

        public static bool operator !=(KindOfBuying left, KindOfBuying right)
        {
            return !(left == right);
        }
    }
}
