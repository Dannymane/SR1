using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class SubjectGroup
   {
      public virtual Subject Subject { get; set; }
      public int SubjectId { get; set; }
      public virtual Group Group { get; set; }
      public int GroupId { get; set; }
   }
}
