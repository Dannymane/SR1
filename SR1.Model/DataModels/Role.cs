using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   //Role class needed only to extend default role, the default role is a IdentityRole<string>
   public class Role : IdentityRole<int>
   {
      public RoleValue RoleValue { get; set; }
      public Role(string name, RoleValue roleValue)
      {
         Name = name;
         RoleValue = roleValue;
      }
   }
}
