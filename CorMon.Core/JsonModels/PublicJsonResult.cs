using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.JsonModels
{
  public class PublicJsonResult
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
