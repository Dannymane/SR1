using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class User : IdentityUser<int>
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime RegistrationDate { get; set; }
   }
}
