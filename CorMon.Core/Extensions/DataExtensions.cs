using AspNetCore.Identity.Mongo.Model;
using CorMon.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Extensions
{
    public static class DataExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        public static bool Has<T>(this IList<string> collections, string name = "") 
        {
            var collection = name;
            if (string.IsNullOrEmpty(collection))
            {
                collection = typeof(T).Name;

                if (!collection.EndsWith("s"))
                    collection = collection + "s";
            }

            return collections.Contains(collection);
        }


    }
}
