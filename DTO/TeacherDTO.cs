namespace ASP.NetCoreMVC_SchoolSystem.DTO
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Salary { get; set; }
    }
}
