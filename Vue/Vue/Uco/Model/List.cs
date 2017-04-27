using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace Vue.lib
{
    internal static class List
    {
        public static List<T> DataTableToList<T>(this DataTable dt) where T : class, new()
        {
            List<T> Result = new List<T>();
            List<PropertyInfo> ps = new List<PropertyInfo>();

            foreach (PropertyInfo p in typeof(T).GetProperties())
                ps.Add(p);

            foreach (DataRow Row in dt.Rows)
            {
                T obj = new T();
                foreach (PropertyInfo p in ps)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && Row[p.Name] != DBNull.Value) 
                        p.SetValue(obj, Row[p.Name], null);
                }
                Result.Add(obj);
            }

            return Result;
        }

        public static List<T> DataRowToList<T>(this DataRow[] Rows) where T : class, new()
        {
            List<T> Result = new List<T>();
            List<PropertyInfo> ps = new List<PropertyInfo>();

            foreach (PropertyInfo p in typeof(T).GetProperties())
                ps.Add(p);

            foreach (DataRow Row in Rows)
            {
                T obj = new T();
                foreach (PropertyInfo p in ps)
                {
                    if (Row.Table.Columns.IndexOf(p.Name) != -1 && Row[p.Name] != DBNull.Value)
                        p.SetValue(obj, Row[p.Name], null);
                }
                Result.Add(obj);
            }

            return Result;
        }

        public static T DataTableToObj<T>(this DataTable dt) where T : class, new() 
        {
            T obj = new T();

            if (dt.Rows.Count > 0) {
                List<PropertyInfo> ps = new List<PropertyInfo>();
                foreach (PropertyInfo p in typeof(T).GetProperties())
                    ps.Add(p);


                foreach (PropertyInfo p in ps)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && dt.Rows[0][p.Name] != DBNull.Value)
                        p.SetValue(obj, dt.Rows[0][p.Name], null);
                }
            }
            
            return obj;
        }
    }

    internal static class EnumUtil
    {
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
                return string.Empty;
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return string.Empty;
            }
        }

        public static object EnumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (StringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

        public static IEnumerable<T> MaskToList<T>(Enum mask)
        {
            if (typeof(T).IsSubclassOf(typeof(Enum)) == false)
                throw new ArgumentException();

            return Enum.GetValues(typeof(T))
                                 .Cast<Enum>()
                                 .Where(m => mask.HasFlag(m))
                                 .Cast<T>();
        }
    }
}
