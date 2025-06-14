namespace ASP.NetCoreMVC_SchoolSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Salary { get; set; }

    }
}
