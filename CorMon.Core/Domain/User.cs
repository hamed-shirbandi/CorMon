using AspNetCore.Identity.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Domain
{
   public class User: MongoUser
    {
        public User()
        {
           
        }
        public string DisplayName { get; set; }
        public string About { get; set; }
        public string AvatarUrl { get; set; }
    }
}
