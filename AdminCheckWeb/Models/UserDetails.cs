using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminCheckWeb.Models
{
    public class UserDetails
    {
        [Key]
        public int Account_Id { get; set; }

        public string? Account_Type { get; set; }
        public float Balance { get; set; }

        //public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public float Interest { get; set; }



        public int LoginId { get; set; }

        //public string? AccountType {  get; set; }

      
    }
}

