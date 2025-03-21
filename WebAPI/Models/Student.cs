using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        public string GroupCode { get; set; }
    }
}
