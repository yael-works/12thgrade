using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category : BaseEntity
    {
        private int kodCateg;
        private string nameteur;
        public int KodCateg { get => kodCateg; set => kodCateg = value; }
        public string Nameteur { get => nameteur; set => nameteur = value; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   kodCateg == category.kodCateg;
        }

        public override int GetHashCode()
        {
            return 572952967 + kodCateg.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodCateg" };
        }

        public override string GetTableName()
        {
            return "Category";
        }
        public override string ToString()
        {
            return kodCateg.ToString();
        }

        public static bool operator ==(Category left, Category right)
        {
            return EqualityComparer<Category>.Default.Equals(left, right);
        }

        public static bool operator !=(Category left, Category right)
        {
            return !(left == right);
        }
    }
}

