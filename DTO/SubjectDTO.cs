using System.ComponentModel.DataAnnotations;

namespace ASP.NetCoreMVC_SchoolSystem.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }

        //Validace - delka retezce, dva stejne fungujici zapisy
        [MinLength(2), MaxLength(50)]
        [StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }
    }
}
