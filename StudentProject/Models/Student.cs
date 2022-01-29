using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Students.Models
{
    [Index(nameof(SchoolMail), IsUnique = true)] //the email is unique for everyone
    public class Student
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "Necessario inserire il nome")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name {get; set;} = string.Empty;

        [Required(ErrorMessage = "Necessario inserire il cognome")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Surname {get; set;} = string.Empty;

        [Required(ErrorMessage = "Necessario inserire l'eta'")]
        public int Age {get; set;}

        [Required(ErrorMessage = "Necessario per ogni alunno avere la propria mail istituzionale")]
        [StringLength(maximumLength: 240, MinimumLength = 17)]
        public string SchoolMail{get; set;} = string.Empty;
    }
}