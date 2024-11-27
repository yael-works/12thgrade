using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{

        
        public class RecipeDB : BaseDB
        {
            public RecipeDB() : base("Recipe")
            {

            }
            public override BaseEntity CreateModel()
            {
                Recipe item = new Recipe();
                item.KodRecipe = Convert.ToInt32(reader["KodRecipe"].ToString());
                item.NameRecipe = reader["NameRecipe"].ToString();
                item.AmountMana = Convert.ToInt32(reader["AmountMana"].ToString());
                item.Status = (bool)reader["Status"];
                return item;
                
       
            }
            public List<Recipe> GetList()
            {
                return base.list.Cast<Recipe>().ToList();
            }
        public Recipe GetRecipeByCode(int code)
        {

            return GetList().FirstOrDefault(x => x.KodRecipe == code);
        }
        public List<Recipe> GetRecipeBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<Recipe>().ToList();
        }
    }
    }

