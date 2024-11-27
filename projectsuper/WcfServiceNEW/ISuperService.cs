using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Model;
namespace WcfServiceNEW
{

    [ServiceContract]
    public interface ISuperService
    {
        [OperationContract]
        void Hithul();
        //מוצרים
        [OperationContract]
        int GetNextKeyProduct();
        [OperationContract]
        void AddProduct(Products p);
        [OperationContract]
        void UpdateProduct(Products p);
        [OperationContract]
        void UpdateProductAmount(Products p, double amount, KindAmountProduct kindAmount);
        [OperationContract]
        List<Products> GetProductsBySelect(string nameField, string st, bool contains);
        [OperationContract]
        List<Products> GetProductsExistsBySelect(string nameField, string st, bool contains);
        [OperationContract]
        List<Products> GetListProducts();
        [OperationContract]
        List<Products> GetListProductsExists();
        [OperationContract]
        Products GetProductByCode(int code);
        [OperationContract]
        void DeleteProduct(Products p);
        [OperationContract]
        byte[] GetImage(string fileName);


        //מוצרים לקניה
        [OperationContract]
        List<ProductToBuying> GetProductToBuyingBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddProductToBuying(ProductToBuying p);
        [OperationContract]
        void UpdateAmountProductToBuying(ProductToBuying p);
        [OperationContract]
        ProductToBuying GetProductToBuyingByCode(int code,int code1);
        [OperationContract]
        void DeleteProductToBuying(ProductToBuying p);
        [OperationContract]
        List<ProductToBuying> GetListProductsToBuying();


        //מוצרים למתכונים
        [OperationContract]
        List<ProductToRecipe> GetProductToRecipeBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddProductToRecipe(ProductToRecipe p);
        [OperationContract]
        void UpdateProductToRecipe(ProductToRecipe p);
        [OperationContract]
         ProductToRecipe GetProducToRecipetByCode(int code, int code1);
        [OperationContract]
        void DeleteProductToRecipe(ProductToRecipe p);
        [OperationContract]
        List<ProductToRecipe> GetListProductToRecipe();
        [OperationContract]
        int GetNextKeyProductToRecipe();

        //מתכונים
        [OperationContract]
        int GetNextKeyRecipe();
        [OperationContract]
        List<Recipe> GetRecipeBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddRecipe(Recipe r);
        [OperationContract]
        void UpdateRecipe(Recipe r);
        [OperationContract]
        Recipe GetRecipeByCode(int code);
        [OperationContract]
        void DeleteRecipe(Recipe r);
        [OperationContract]
        List<Recipe> GetListRecipe();
        [OperationContract]
        void DeleteStatuseRecipe(Recipe r);

        //שימוש במוצרים
        [OperationContract]
        List<UsesProduct> GetUsesProductBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void UpdateUsesProduct(UsesProduct u);
        [OperationContract]
        UsesProduct GetUsesProductByCode(int code);
        [OperationContract]
        void DeleteUsesProduct(UsesProduct u);
        [OperationContract]
        List<UsesProduct> GetListUsesProducts();
        [OperationContract]
        int GetNextKeyUsesProduct();

        //סוג כמות
        [OperationContract]
        List<KindAmountProduct> GetkindAmountProductBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddkindAmountProduct(KindAmountProduct k);
        [OperationContract]
        void UpdateKindAmountProduct(KindAmountProduct k);
        [OperationContract]
        KindAmountProduct GetKindAmountProductByCode(int code);
        [OperationContract]
        void DeleteKindAmountProduct(KindAmountProduct k);
        [OperationContract]
        List<KindAmountProduct> GetListKindAmountProduct();
        [OperationContract]
        int GetNextKeyKindAmountProduct();


        //סוג קניה
        [OperationContract]
        int GetNextKeyKindOfBuying();
        [OperationContract]
        List<KindOfBuying> GetKindOfBuyingBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddKindOfBuying(KindOfBuying k);
        [OperationContract]
        void UpdateKindOfBuying(KindOfBuying k);
        [OperationContract]
        KindOfBuying GetKindOfBuyingByCode(int code);
        [OperationContract]
        void DeleteKindOfBuying(KindOfBuying k);
        [OperationContract]
        List<KindOfBuying> GetListKindOfBuying();

        //קטגוריות
        [OperationContract]
        int GetNextKeyCategory();
        [OperationContract]
        List<Category> GetCategoryBySelect(string nameField, string st, bool contains);
        [OperationContract]
        void AddCategory(Category k);
        [OperationContract]
        void UpdateCategory(Category k);
        [OperationContract]
        Category GetCategory(int code);
        
        [OperationContract]
        List<Category> GetListCategory();
       // סיכום שימושים במוצר
       
        [OperationContract]
        List<Summery> GetUseByTime(string time, int month = 0, int year = 0);
       
       
    }
}
