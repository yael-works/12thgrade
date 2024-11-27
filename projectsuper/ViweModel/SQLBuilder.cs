using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Model;
using ViewModel;

namespace ViewModel
{
    static class SQLConverter
    {
       

        public static string ToSQLString(this object value)
        {
            if (value is int || value is double || value is bool)
                return value.ToString();
            if (value is string)
                return "'" + value + "'";
            if (value is DateTime date)
                return date.ToString("#dd/MM/yyyy HH:mm:ss#");
            if (value is TimeSpan time)
                return "#" + time.Hours.ToString("00") + ":" + time.Minutes.ToString("00") + "#";
            return "";
        }

       
    }
}
class SQLBuilder
{
    public static string InsertSQL(BaseEntity entity)
    {
        Type type = entity.GetType();
        string command = "Insert Into " + entity.GetTableName() + " (";
        string values = " Values (";
        foreach (var item in type.GetProperties())
        {
            string name = item.Name;
            object value = item.GetValue(entity);

            if (value != null)
            {
                command += name + ", ";
                if (value is BaseEntity)
                {
                    values += (value as BaseEntity).GetKeyValues();
                }
                else 
                {
                    values += value.ToSQLString();
                }
                values += ", ";
            }
        }
        command = command.Substring(0, command.Length - 2) + ")";
        values = values.Substring(0, values.Length - 2) + ")";
        return command + values;
    }

    public static string UpdateSQL(BaseEntity entity)
    {
        Type type = entity.GetType();
        string command = "Update " + entity.GetTableName() + " set ";
        foreach (var item in type.GetProperties())
        {
            string name = item.Name;
            var value = item.GetValue(entity);

            if (!entity.GetKeyFields().Contains(name))
            {
                if(value!=null)
                { 
                if (value is BaseEntity entityValue)
                {
                    string k = entityValue.GetKeyFields()[0];
                    command += k + " = ";

                    object keyValue = value.GetType().GetProperty(k).GetValue(value);

                    if (keyValue is string)
                        value = "'" + keyValue + "'";
                    else
                        value = keyValue;

                    command += value + ", "; ;
                }
                else
                {
                    command += name + " = " + value.ToSQLString() + " , ";
                }
            }
        }
        }
        string where = string.Empty;
        foreach (var item in entity.GetKeyFields())
        {
            if (where != string.Empty)
                where += " And ";
            object value = entity.GetType().GetProperty(item).GetValue(entity);
            if (value is string)
                where += item + " = '" + value + "' ";
            else
                where += item + " = " + value;
        }
        command = command.Substring(0, command.Length - 2) + " Where " + where;
        return command;
    }

    public static string DeleteSQL(BaseEntity entity)
    {
        Type type = entity.GetType();
        string command = "Delete From " + entity.GetTableName() + " Where ";
        string where = string.Empty;
        foreach (var item in entity.GetKeyFields())
        {
            if (where != string.Empty)
                where += " And ";
            object value = entity.GetType().GetProperty(item).GetValue(entity);
            where += item + " = " + value.ToSQLString();
        }
        command += where;
        return command;
    }

    public static string DeleteStatusSQL(BaseEntity entity)
    {
        Type type = entity.GetType();
        string command = "Update " + entity.GetTableName() + " set ";
        foreach (var item in type.GetProperties())
        {
            string name = item.Name;
            var value = item.GetValue(entity);
            if (value is BaseEntity)
            {
                string k = ((BaseEntity)value).GetKeyFields()[0];
                command += k + " = ";
                object keyValue = value.GetType().GetProperty(k).GetValue(value);

                if (keyValue is string)
                    value = "'" + keyValue + "'";
                else
                    value = keyValue;
                command += value + ", "; ;
            }
            else
            {
                command += name + " = ";
                if (value is string)
                    command += "'" + value + "', ";
                else if (value is int || value is double)
                    command += name + " = " + value + ", ";
                else if (value is bool)
                    command += name + " = " + false + ", ";
                else if (value is DateTime)
                    if (value.ToString().IndexOf("00:00:00") < 0)
                        command += name + " = " + ((DateTime)value).ToString("#HH:mm:ss#") + " , ";
                    else
                        command += name + " = " + ((DateTime)value).ToString("#dd/MM/yyyy#") + " , ";
                else if (value is TimeSpan)
                    command += "'" + ((TimeSpan)value).Hours.ToString("00") + ":" + ((TimeSpan)value).Minutes.ToString("00") + "' , ";
                else
                    command += "'" + value + "', ";
            }
        }
        string where = string.Empty;
        foreach (var item in entity.GetKeyFields())
        {
            if (where != string.Empty)
                where += " And ";
            if (entity.GetType().GetProperty(item).GetValue(entity) is string)
                where += item + " = '" + entity.GetType().GetProperty(item).GetValue(entity) + "' ";
            else
                where += item + " = " + entity.GetType().GetProperty(item).GetValue(entity);
        }
        command = command.Substring(0, command.Length - 2) + " Where " + where;
        return command;
    }
}