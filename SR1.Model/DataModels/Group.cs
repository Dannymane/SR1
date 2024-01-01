using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class Group
   {
      public int Id { get; set; }
      public string Name { get; set; }
      // if Group will be created without Students, the Students will be null or, in this case - new List<Student>()
      // anyway EF will override student list after adding students to database (as students of group of course)
      public virtual IList<Student> Students { get; set; } = new List<Student>(); 
      public virtual IList<SubjectGroup> SubjectGroups { get; set; } = new List<SubjectGroup>();

   }
}
