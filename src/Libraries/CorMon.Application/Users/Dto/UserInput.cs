using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Application.Users.Dto
{
   public class UserInput
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string About { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUrl { get; set; }

    }
}
