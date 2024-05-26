using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class Teacher : User
   {
      [Required]
      public string Title { get; set; } = "";
      public virtual IList<Subject> Subjects { get; set; } = new List<Subject>();
   }
}
