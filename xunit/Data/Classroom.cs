using System;
using System.Collections.Generic;
using System.Text;

namespace Efcore_Test_Sample.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
