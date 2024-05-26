using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class Subject
   {
      public int Id {  get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      public string Description { get; set; } = "";
      public virtual IList<SubjectGroup>? SubjectGroups { get; set; }
      public virtual Teacher? Teacher { get; set; }
      public int? TeacherId { get; set; }
      public virtual IList<Grade>? Grades { get; set; }
   }
}
