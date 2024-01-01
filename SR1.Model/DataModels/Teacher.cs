﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1.Model.DataModels
{
   public class Teacher : User
   {
      public string Title { get; set; } = "";
      public IList<Subject> Subjects { get; set; } = new List<Subject>();
   }
}
