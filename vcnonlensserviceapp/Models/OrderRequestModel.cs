using System.ComponentModel.DataAnnotations;

namespace vcnonlensserviceapp.Models
{
    public class OrderRequestModel
    {
        [Required]
        public string SessionId_ { get; set; }

        [Required]
        public string Filename { get; set; }

        [Required]
        public string Filecontent { get; set; }

        [Required]
        public string CompanyId { get; set; }
    }
}
