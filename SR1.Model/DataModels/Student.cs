using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class Student : User
   {
      public virtual IList<Grade> Grades { get; set; } = new List<Grade>();
      public virtual Parent? Parent { get; set; }
      public int? ParentId { get; set; }
      //In this case, the student can be assigned to existing group in Groups or null
      public virtual Group? Group { get; set; }
      public int? GroupId { get; set; }

      //Same, but null not allowed, must be assigned 
      //public Group Group { get; set; }
      //public int GroupId { get; set; }
      //             what about strings and reference types?
      public IDictionary<string, List<GradeScale>> GradesPerSubject
      {
         get
         {
            return Grades?.GroupBy(g => g.Subject.Name)
                         .ToDictionary(
                              el => el.Key,
                              el => el.Select(g => g.GradeValue).ToList()
                          ) 
                         ?? new Dictionary<string, List<GradeScale>>();
         }
      }
      public IDictionary<string, double> AverageGradePerSubject
      {
         get
         {
            return GradesPerSubject.ToDictionary(
                  el => el.Key,
                  el => el.Value.Average(gs => (byte)gs)
               );

         }
      }
      public double AverageGrade => AverageGradePerSubject.Values.Average();

   }
}
