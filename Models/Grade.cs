namespace ASP.NetCoreMVC_SchoolSystem.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public string? Topic { get; set; } //Co (zkouseni, pisemka)
        public int Mark {  get; set; } //Znamka
        public DateTime Date { get; set; }
    }
}
