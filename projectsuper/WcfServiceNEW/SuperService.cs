using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model;
using ViewModel;
using ViweModel;

namespace WcfServiceNEW
{
    public class SuperService : ISuperService
    {
        #region Add

        public void AddkindAmountProduct(KindAmountProduct k)
        {
            MyDB.tblKindAmountProduct.Insert(k);
            MyDB.tblKindAmountProduct.SaveChanges();
        }

      

        public void AddProduct(Products p)
        {
            MyDB.tblProducts.Insert(p);
            MyDB.tblProducts.SaveChanges();
            AddUsesProduct(p);
        }

        public void AddUsesProduct(Products product)
        {
            UsesProduct uses = new UsesProduct
            {
                KodUse = GetNextKeyUsesProduct(),
                DateUse = DateTime.Now,
                KodProduct = product,
                Amount=0
            };
            MyDB.tblUsesProduct.Insert(uses);
            MyDB.tblUsesProduct.SaveChanges();
        }

        public void AddProductToBuying(ProductToBuying p)
        {
            MyDB.tblProductToBuying.Insert(p);
            MyDB.tblProductToBuying.SaveChanges();
        }

        public void AddProductToRecipe(ProductToRecipe p)
        {
            MyDB.tblProductToRecipe.Insert(p);
            MyDB.tblProductToRecipe.SaveChanges();
        }

        public void AddRecipe(Recipe r)
        {
            MyDB.tblRecipe.Insert(r);
            MyDB.tblRecipe.SaveChanges();
        }
        #endregion

        #region Delete

        public void DeleteKindAmountProduct(KindAmountProduct k)
        {
            MyDB.tblKindAmountProduct.Delete(k);
            MyDB.tblKindAmountProduct.SaveChanges();
        }

     

        public void DeleteProduct(Products p)
        {
            MyDB.tblProducts.Delete(p);
            MyDB.tblProducts.SaveChanges();
        }

        public void DeleteProductToBuying(ProductToBuying p)
        {
            MyDB.tblProductToBuying.Delete(p);
            MyDB.tblProductToBuying.SaveChanges();
        }

        public void DeleteProductToRecipe(ProductToRecipe p)
        {
            MyDB.tblProductToRecipe.Delete(p);
            MyDB.tblProductToBuying.SaveChanges();
        }

        public void DeleteRecipe(Recipe r)
        {
            MyDB.tblRecipe.Delete(r);
            MyDB.tblRecipe.SaveChanges();
        }


        public void DeleteUsesProduct(UsesProduct u)
        {
            MyDB.tblUsesProduct.Delete(u);
            MyDB.tblUsesProduct.SaveChanges();
        }
        public void DeleteStatus(Products p)
        {
            MyDB.tblProducts.DeleteStatus(p);
            MyDB.tblProducts.SaveChanges();
        }

        public void DeleteStatuseRecipe(Recipe r)
        {
            MyDB.tblRecipe.DeleteStatus(r);
            MyDB.tblRecipe.SaveChanges();

        }
       

        #endregion

        public KindAmountProduct GetKindAmountProductByCode(int code)
        {
            return MyDB.tblKindAmountProduct.GetKindAmountProductByCode(code);
        }
       
        public List<KindAmountProduct> GetListKindAmountProduct()
        {
            return MyDB.tblKindAmountProduct.GetList();
        }

      
        public List<Products> GetListProducts()
        {
            return MyDB.tblProducts.GetListProducts();
        }

        public List<Products> GetListProductsExists()
        {
            return MyDB.tblProducts.GetListProducts().Where(x => x.Status == true).ToList();
        }

        public List<ProductToBuying> GetListProductsToBuying()
        {
            return MyDB.tblProductToBuying.GetList();
        }

        public List<ProductToRecipe> GetListProductToRecipe()
        {
            return MyDB.tblProductToRecipe.GetList();
        }

        public List<Recipe> GetListRecipe()
        {
            return MyDB.tblRecipe.GetList();
        }

        public List<UsesProduct> GetListUsesProducts()
        {
            return MyDB.tblUsesProduct.GetList();
        }

        public Products GetProductByCode(int code)
        {
            return MyDB.tblProducts.GetProductsByCode(code);

        }

        public ProductToRecipe GetProducToRecipetByCode(int code1, int code2)
        {
            return MyDB.tblProductToRecipe.GetProductToRecipeByCode(code1, code2);

        }

        public ProductToBuying GetProductToBuyingByCode(int code1, int code2)
        {
            return MyDB.tblProductToBuying.GetProductToBuyingByCode(code1, code2);
        }


        public Recipe GetRecipeByCode(int code)
        {
            return MyDB.tblRecipe.GetRecipeByCode(code);
        }

        public UsesProduct GetUsesProductByCode(int code)
        {
            return MyDB.tblUsesProduct.GetUsesProductByCode(code);
        }


        public void UpdateAmountProductToBuying(ProductToBuying p)
        {
            MyDB.tblProductToBuying.Update(p);
            MyDB.tblProductToBuying.SaveChanges();

        }

        public void UpdateKindAmountProduct(KindAmountProduct k)
        {
            MyDB.tblKindAmountProduct.Update(k);
            MyDB.tblKindAmountProduct.SaveChanges();
        }

      

        public void UpdateProduct(Products p)
        {
            if (!p.Status)
            { 
                p.AmountMlay = 0;
                p.AmountGmMlay= 0;
                p.AmountGmBag=p.AmountGmBag;
            }
            MyDB.tblProducts.Update(p);
            MyDB.tblProducts.SaveChanges();
            UsesProduct uses = GetUsesProductBySelect("KodProduct", p.KodProduct.ToString(), false)
                .FirstOrDefault();
            if (uses == null)
                AddUsesProduct(p);
            else
            {
                uses.Amount++;
                uses.DateUse = DateTime.Now;
                UpdateUsesProduct(uses);
            }
        }
        public void UpdateProductAmount(Products p, double amount, KindAmountProduct kindAmount)
        {
          
            if (p != null)
            {
                if(kindAmount.KodKindProduct!=5)
                { 
                double amountGrams = kindAmount.Grams * amount;
                p.AmountGmMlay += amountGrams;
                double x = p.AmountGmMlay;
                p.AmountMlay = (int)(x / p.AmountGmBag);
                UpdateProduct(p);
                }
                else
                {
                    double a = p.AmountGmBag;
                    double amountGrams = a * amount;
                    p.AmountGmMlay += amountGrams;
                    double x = p.AmountGmMlay;
                    p.AmountMlay = (int)(x / p.AmountGmBag);
                    UpdateProduct(p);
                
            }
            }
        }
        public void UpdateUsesProduct(UsesProduct u)
        {
            MyDB.tblUsesProduct.Update(u);
            MyDB.tblUsesProduct.SaveChanges();
        }

        public void UpdateProductToRecipe(ProductToRecipe p)
        {
            MyDB.tblProductToRecipe.Update(p);
            MyDB.tblProductToRecipe.SaveChanges();
        }

        public void UpdateRecipe(Recipe r)
        {
            MyDB.tblRecipe.Update(r);
            MyDB.tblRecipe.SaveChanges();
        }

        public void AddCategory(Category k)
        {
            MyDB.tblCategory.Insert(k);
            MyDB.tblCategory.SaveChanges();
        }

        public void UpdateCategory(Category k)
        {
            MyDB.tblCategory.Update(k);
            MyDB.tblCategory.SaveChanges();
        }

        public Category GetCategory(int code)
        {
            return MyDB.tblCategory.GetCategoryByCode(code);
        }

        public List<Category> GetListCategory()
        {
            return MyDB.tblCategory.GetListCategory();
        }
       public void Hithul()
       {
            MyDB.tblCategory = new CategoryDB();
            MyDB.tblKindAmountProduct = new KindAmountProductDB();
            MyDB.tblKindOfBuying = new KindOfBuyingDB();
            MyDB.tblProducts = new ProductsDB();
            MyDB.tblProductToBuying = new ProductToBuyingDB();
            MyDB.tblRecipe = new RecipeDB();
            MyDB.tblProductToRecipe = new ProductToRecipeDB();
            MyDB.tblUsesProduct = new UsesProductDB();
        }

        public List<Category> GetCategoryBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblCategory.GetCategoryBySelect(nameField, st, contains);
        }

        public List<ProductToBuying> GetProductToBuyingBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblProductToBuying.GetProductToBuyingBySelect(nameField, st, contains);
        }

        public List<ProductToRecipe> GetProductToRecipeBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblProductToRecipe.GetProductToRecipeBySelect(nameField, st, contains);
        }

        public List<Recipe> GetRecipeBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblRecipe.GetRecipeBySelect(nameField, st, contains);
        }

        public List<UsesProduct> GetUsesProductBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblUsesProduct.GetUsesProductBySelect(nameField, st, contains);
        }



      


        public List<KindOfBuying> GetKindOfBuyingBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblKindOfBuying.GetKindOfBuyingBySelect(nameField, st, contains);
        }

        public void AddKindOfBuying(KindOfBuying k)
        {
            MyDB.tblKindOfBuying.Insert(k);
            MyDB.tblKindOfBuying.SaveChanges();
        }

        public void UpdateKindOfBuying(KindOfBuying k)
        {
            MyDB.tblKindOfBuying.Update(k);
            MyDB.tblKindOfBuying.SaveChanges();
        }

        public KindOfBuying GetKindOfBuyingByCode(int code)
        {
            return MyDB.tblKindOfBuying.GetKindOfBuyingByCode(code);
        }

        public void DeleteKindOfBuying(KindOfBuying k)
        {
            MyDB.tblKindOfBuying.Delete(k);
            MyDB.tblKindOfBuying.SaveChanges();
        }

        public List<KindOfBuying> GetListKindOfBuying()
        {
            return MyDB.tblKindOfBuying.GetList();
        }

        public List<KindAmountProduct> GetkindAmountProductBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblKindAmountProduct.GetKindAmountProductBySelect(nameField, st, contains);
        }

        public List<Products> GetProductsBySelect(string nameField, string st, bool contains)
        {
            return MyDB.tblProducts.GetProductsBySelect(nameField, st, contains);
        }

        public List<Products> GetProductsExistsBySelect(string nameField, string st, bool contains)
        {
            
            return MyDB.tblProducts.GetProductsExistsBySelect(nameField, st,contains).Where(x => x.Status == true).ToList();
        }


        public int GetNextKeyRecipe()
        {
            Recipe p = MyDB.tblRecipe.GetList().OrderBy(x => x.KodRecipe).LastOrDefault();
            if (p == null)
                return 1;
            return p.KodRecipe + 1;
        }
      
        public int GetNextKeyKindOfBuying()
        {
            KindOfBuying p = MyDB.tblKindOfBuying.GetList().OrderBy(x => x.KodKindBuying).LastOrDefault();
            if (p == null)
                return 1;
            return p.KodKindBuying + 1;
        }
        public int GetNextKeyProductToRecipe()
        {
            ProductToRecipe p = MyDB.tblProductToRecipe.GetList().OrderBy(x => x.KodRecipe).LastOrDefault();
            if (p == null)
                return 1;
            return Convert.ToInt32(p.KodRecipe) + 1;
        }

        public int GetNextKeyCategory()
        {
            Category p = MyDB.tblCategory.GetListCategory().OrderBy(x => x.KodCateg).LastOrDefault();
            if (p == null)
                return 1;
            return p.KodCateg + 1;
        }

        public int GetNextKeyKindAmountProduct()
        {
            KindAmountProduct p = MyDB.tblKindAmountProduct.GetList().OrderBy(x => x.KodKindProduct).LastOrDefault();
            if (p == null)
                return 1;
            return Convert.ToInt32(p.KodKindProduct) + 1;
        }

        public int GetNextKeyProduct()
        {
            Products p = MyDB.tblProducts.GetListProducts().OrderBy(x => x.KodProduct).LastOrDefault();
            if (p == null)
                return 1;
            return p.KodProduct + 1;
        }

        
        public List<Summery> GetUseByTime(string time, int month = 0, int year = 0)
        {
            List<Summery> lstS = new List<Summery>();
            List<UsesProduct> lst = null;

            if (year == 0)
                year = DateTime.Now.Year;

            if (month == 0)
                month = DateTime.Now.Month;

            if (time == "חודש")
            {
                lst = MyDB.tblUsesProduct.GetList().Where(x => x.DateUse.Month == month && x.DateUse.Year == year).ToList();
            }
            else if (time == "שנה")
            {
                lst = MyDB.tblUsesProduct.GetList().Where(x => x.DateUse.Year == year).ToList();
            }

            lstS = lst.Select(x => new Summery
            {
                ProductName = x.KodProduct.NameProduct,
                Amount = lst.Where(w => w.KodProduct.NameProduct == x.KodProduct.NameProduct)
                    .ToList().Sum(w => w.Amount)
            }).ToList();
            return lstS;
        }

        public int GetNextKeyUsesProduct()
        {
            UsesProduct p = MyDB.tblUsesProduct.GetList().OrderBy(x => x.KodUse).LastOrDefault();
            if (p == null)
                return 1;
            return p.KodUse + 1;
        }

        public byte[] GetImage(string fileName)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            int x = path.IndexOf("\\Host");
            path = path.Substring(0, x) + @"\\Pics" + fileName;
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            return null;
        }
    }

}

