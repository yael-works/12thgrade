using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViweModel;

namespace ViewModel
{
    public static class MyDB
    {
        public static CategoryDB tblCategory = new CategoryDB();
        public static ProductsDB tblProducts = new ProductsDB();
        public static KindAmountProductDB tblKindAmountProduct = new KindAmountProductDB();
        public static KindOfBuyingDB tblKindOfBuying = new KindOfBuyingDB();
        public static ProductToBuyingDB tblProductToBuying = new ProductToBuyingDB();
        public static RecipeDB tblRecipe = new RecipeDB();
        public static ProductToRecipeDB tblProductToRecipe = new ProductToRecipeDB();
        public static UsesProductDB tblUsesProduct = new UsesProductDB();
    }
}
