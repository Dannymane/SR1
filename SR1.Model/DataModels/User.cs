using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class User : IdentityUser<int>
   {
      [Required]
      public string FirstName { get; set; }
      [Required]
      public string LastName { get; set; }
      public DateTime RegistrationDate { get; set; }
   }
}
