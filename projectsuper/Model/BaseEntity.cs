using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class BaseEntity
    {
        public abstract string GetTableName();
        public abstract string[] GetKeyFields();

        public string GetKeyNames()
        {
            return string.Join(", ", GetKeyFields());
        }

        public string GetKeyValues()
        {
            string result = "";
            foreach (var key in GetKeyFields())
            {
                if (result.Length > 0)
                    result += ", ";

                PropertyInfo keyProperty = this.GetType().GetProperty(key);

                object keyValue = keyProperty.GetValue(this);

                if (keyValue is string)
                    result += "'" + keyValue + "'";
                else
                    result += keyValue;
            }
            return result;
        }

        public string GetFullKeys()
        {
            string result = "";
            foreach (var key in GetKeyFields())
            {
                if (result.Length > 0)
                    result += ", ";

                PropertyInfo keyProperty = this.GetType().GetProperty(key);

                string keyValue = keyProperty.GetValue(this).ToString();

                result += key + " = ";
                if (keyValue is string)
                    result += "'" + keyValue + "'";
                else
                    result += keyValue;
            }
            return result;
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return EqualityComparer<BaseEntity>.Default.Equals(left, right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
    }
}
