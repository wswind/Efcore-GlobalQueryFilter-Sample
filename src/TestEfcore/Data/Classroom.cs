using System.Collections.Generic;

namespace TestEfcore.Data
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
