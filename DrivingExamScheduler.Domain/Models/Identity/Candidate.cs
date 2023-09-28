using DrivingExamScheduler.Domain.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DrivingExamScheduler.Domain.Models.Identity
{
    public class Candidate : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? EMBG { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? DrivingSchool { get; set; }
        public int? Age { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? CurrentCategory { get; set; }
        public Appointment? Appointment { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }

        public static int CalculateAgeFromEMBG(string embg)
        {
            if (embg.Length != 13)
            {
                throw new ArgumentException("EMBG must have 13 digits.", nameof(embg));
            }
            string birthdatePart = embg.Substring(0, 7);


            string yearPart = birthdatePart.Substring(4, 3);

            string year = yearPart.ToCharArray().ElementAt(0) == '0' ? "2" + yearPart : "1" + yearPart;


            if (DateTime.TryParseExact(embg.Substring(0, 4) + year, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthdate))
            {
                int age = DateTime.Now.Year - birthdate.Year;

                if (birthdate.AddYears(age) > DateTime.Now)
                {
                    age--;
                }

                return age;
            }
            else
            {
                throw new ArgumentException("Invalid EMBG format.", nameof(embg));
            }
        }

    }
}
