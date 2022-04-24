using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace CorMon.Core.Extensions
{
    public static class EnumExtensions
    {



        /// <summary>
        /// 
        /// </summary>
        public static T ToEnum<T>(this string value)
        where T : struct
        {
            Debug.Assert(!string.IsNullOrEmpty(value));
            return (T)Enum.Parse(typeof(T), value, true);
        }






        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                        .ToDictionary(k => k.Key, k => k.Value);
        }





        /// <summary>
        /// 
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        
    }
}