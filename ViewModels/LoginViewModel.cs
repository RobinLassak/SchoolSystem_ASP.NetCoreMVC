using System.ComponentModel.DataAnnotations;

namespace ASP.NetCoreMVC_SchoolSystem.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [Display(Name = "Return URL")]
        public string? ReturnURL { get; set; }
        public bool Remember { get; set; }
    }
}
