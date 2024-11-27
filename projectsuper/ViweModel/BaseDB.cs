using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using Model;
namespace ViewModel
{
    public abstract class BaseDB
    {
        protected string connectionString;
        protected OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;
        protected List<BaseEntity> list;
        protected List<BaseEntity> lstInserted = new List<BaseEntity>();
        protected List<BaseEntity> lstChanged = new List<BaseEntity>();
        protected List<BaseEntity> lstDeleted = new List<BaseEntity>();
        protected List<BaseEntity> lstDeleteStaus = new List<BaseEntity>();
        protected string tableName;
        public BaseDB(string tableName)
        {
            this.tableName = tableName;
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + GetCurrentDataPath() + "\\SuperShop.accdb";
            connection = new OleDbConnection(connectionString);
            command = new OleDbCommand
            {
                Connection = connection,
                CommandText = "Select * From " + tableName
            };
            list = Select();
            lstInserted = new List<BaseEntity>();
        }
     
        public List<BaseEntity> GetListBySelect(string nameField, string st, bool contains)
        {
            List<BaseEntity> listResult = new List<BaseEntity>();
            list = Select();
            foreach (BaseEntity item in list)
            {
                object value = item.GetType().GetProperty(nameField).GetValue(item);

                if (contains)
                {
                    if (value.ToString().Contains(st))
                        listResult.Add(item);
                }
                else
                {
                    if (value.ToString() == st)
                        listResult.Add(item);
                }
            }
            return listResult;
        }

        private List<BaseEntity> Select()
        {
            List<BaseEntity> lst = new List<BaseEntity>();
            try
            {
              
                connection.Open();
                command.CommandText = "Select * From " + tableName;
                reader = command.ExecuteReader();
                while (reader.Read())
                    lst.Add(CreateModel());
            }
            catch (Exception ex)
            { }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return lst;
        }

        public abstract BaseEntity CreateModel();

        public static string GetCurrentDataPath()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            int x = path.IndexOf(@"\Host");
            path = path.Substring(0, x) + @"\Data";
            return path;
        }

        public static string GetCurrentImagesPath()
        {
            return GetCurrentDataPath() + @"\Images";
        }

        public void Insert(BaseEntity item)
        {
            if (item != null)
                lstInserted.Add(item);
        }

        public void Update(BaseEntity item)
        {
            if (item != null)
                lstChanged.Add(item);
        }

        public void Delete(BaseEntity item)
        {
            if (item != null)
                lstDeleted.Add(item);
        }

        public void DeleteStatus(BaseEntity item)
        {
            if (item != null)
                lstDeleteStaus.Add(item);
        }

        public int SaveChanges()
        {
            int records = 0;
            try
            {
                command.Connection = connection;
                connection.Open();

                foreach (var item in lstInserted)
                {
                    command.CommandText = SQLBuilder.InsertSQL(item);
                    records += command.ExecuteNonQuery();
                    list.Add(item);
                }
                lstInserted.Clear();

                foreach (var item in lstChanged)
                {
                    command.CommandText = SQLBuilder.UpdateSQL(item);
                    records += command.ExecuteNonQuery();

                    list.Remove(item);
                    list.Add(item);
                }
                lstChanged.Clear();

                foreach (var item in lstDeleted)
                {
                    command.CommandText = SQLBuilder.DeleteSQL(item);
                    records += command.ExecuteNonQuery();
                    list.Remove(item);
                }
                lstDeleted.Clear();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n Command:" + command.CommandText);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            list = Select();
            return records;
        }
    }
}
