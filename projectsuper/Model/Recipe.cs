using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class Recipe : BaseEntity
    {
        private int kodrecipe;
        private string namerecipe;
        private int amountmana;
        private bool status;

        public int KodRecipe { get => kodrecipe; set => kodrecipe = value; }
        public string NameRecipe { get => namerecipe; set => namerecipe = value; }
        public int AmountMana { get => amountmana; set => amountmana = value; }
        public bool Status { get => status; set => status = value; }

        public override bool Equals(object obj)
        {
            return obj is Recipe recipe &&
                   kodrecipe == recipe.kodrecipe;
        }

        public override int GetHashCode()
        {
            return 454684083 + kodrecipe.GetHashCode();
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodRecipe" };
        }

        public override string GetTableName()
        {
            return "Recipe";
        }
        public override string ToString()
        {
            return kodrecipe.ToString();
        }

        public static bool operator ==(Recipe left, Recipe right)
        {
            return EqualityComparer<Recipe>.Default.Equals(left, right);
        }

        public static bool operator !=(Recipe left, Recipe right)
        {
            return !(left == right);
        }
    }
}
