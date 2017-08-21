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

        }

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
