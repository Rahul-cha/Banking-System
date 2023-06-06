using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminCheckWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        //[DisplayName("Display Order")]
        //[Range(1,100,ErrorMessage ="Display Order must be between 1 and 100 only!!")]

        public string? Account { get; set; }

        public int DisplayOrder { get; set; }

        //public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string? Address { get; set; }

        public float OverDraftLimit { get; set; }

        public float InterestRate { get; set; }
        public string? Description { get; set; }

        public float Balance { get; set; }


    }


    //public string? AccountType {  get; set; }

}
