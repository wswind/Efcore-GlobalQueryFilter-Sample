namespace TestEfcore.Data
{
    public class Student : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
