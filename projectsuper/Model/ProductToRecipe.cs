using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class ProductToRecipe : BaseEntity
    {
        private Products kodproduct;
        private Recipe kodrecipe;
        private double amountGrams;

        
        public Products KodProduct { get => kodproduct; set => kodproduct = value; }
        public Recipe KodRecipe { get => kodrecipe; set => kodrecipe = value; }
        public double AmountGrams { get => amountGrams; set => amountGrams = value; }

        public override bool Equals(object obj)
        {
            return obj is ProductToRecipe recipe &&
                   EqualityComparer<Products>.Default.Equals(kodproduct, recipe.kodproduct) &&
                   EqualityComparer<Recipe>.Default.Equals(kodrecipe, recipe.kodrecipe);
        }

        public override int GetHashCode()
        {
            int hashCode = 1180275627;
            hashCode = hashCode * -1521134295 + EqualityComparer<Products>.Default.GetHashCode(kodproduct);
            hashCode = hashCode * -1521134295 + EqualityComparer<Recipe>.Default.GetHashCode(kodrecipe);
            return hashCode;
        }

        public override string[] GetKeyFields()
        {
            return new string[] { "KodProduct", "KodRecipe" };
        }

        public override string GetTableName()
        {
            return "ProductToRecipe";
        }
        public override string ToString()
        {
            return kodproduct+ "\t" + amountGrams;
        }

        public static bool operator ==(ProductToRecipe left, ProductToRecipe right)
        {
            return EqualityComparer<ProductToRecipe>.Default.Equals(left, right);
        }

        public static bool operator !=(ProductToRecipe left, ProductToRecipe right)
        {
            return !(left == right);
        }
    }
}
