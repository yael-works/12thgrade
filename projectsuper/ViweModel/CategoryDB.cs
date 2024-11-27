using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace ViweModel
{
   public class CategoryDB : BaseDB
    {
        public CategoryDB() : base("Category")
        {

        }
        public override BaseEntity CreateModel()
        {
            Category item = new Category();
            item.KodCateg = Convert.ToInt32(reader["KodCateg"].ToString());
            item.Nameteur = reader["Nameteur"].ToString();

            return item;
        }
        public List<Category> GetListCategory()
        {
            return base.list.Cast<Category>().ToList();
        }
        public Category GetCategoryByCode(int code)
        {
            return GetListCategory().FirstOrDefault(x => x.KodCateg == code);
        }
        public List<Category> GetCategoryBySelect(string nameField, string st, bool contains)
        {
            return base.GetListBySelect(nameField, st, contains).Cast<Category>().ToList();
        }
    }
}

