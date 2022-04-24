using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Domain
{
   public class BaseEntity
    {
        public BaseEntity()
        {
            id = ObjectId.GenerateNewId().ToString();
            CreateDateTime = DateTime.Now;
            ModifiedDateTime = DateTime.Now;
        }



        /// <summary>
        /// تاریخ ایجاد 
        /// </summary>
        public DateTime CreateDateTime { get; set; }




        /// <summary>
        /// تاریخ آخرین ویرایش 
        /// </summary>
        public DateTime ModifiedDateTime { get; set; }



        public string Id
        {
            get { return id; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    id = ObjectId.GenerateNewId().ToString();
                else
                    id = value;
            }
        }

        private string id { get; set; }
    }
}
