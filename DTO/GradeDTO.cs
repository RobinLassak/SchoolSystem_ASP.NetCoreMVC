using ASP.NetCoreMVC_SchoolSystem.Models;

namespace ASP.NetCoreMVC_SchoolSystem.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string? Topic { get; set; } //Co (zkouseni, pisemka)
        public int Mark { get; set; } //Znamka
        public DateTime Date { get; set; }
    }
}
