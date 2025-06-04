using System.ComponentModel.DataAnnotations;

namespace ASP.NetCoreMVC_SchoolSystem.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }

        //Validace - delka retezce, dva stejne fungujici zapisy
        [MinLength(2), MaxLength(50)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Zadej nejmene dva a nejvice 50 znaku")]

        //Validace - pouze pismena
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Neplatný název")]
        public string? Name { get; set; }
    }
}
