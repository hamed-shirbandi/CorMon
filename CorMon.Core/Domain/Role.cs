using AspNetCore.Identity.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Domain
{
    public class Role : MongoRole
    {
        #region Ctor

        public Role()
        {
            CreateDateTime = DateTime.Now;
            ModifiedDateTime = DateTime.Now;
        }

        #endregion

        #region Properties

        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }


        #endregion

        #region Nav Prop

    

        #endregion
    }
}
