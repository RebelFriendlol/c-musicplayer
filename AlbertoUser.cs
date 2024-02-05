using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AlbertoPlayer;
    public class AlbertoUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDuser { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string PasswordUser { get; set; }

        [Required]
        public string LoginUser { get; set; }

        [Required]
        public string GenderUser { get; set; }
        
        public DateTime? DateOfBirthUser { get; set; }
        public DateTime DateOfCreateUser { get; set; }

        public string CountryUser { get; set; }
        public decimal Kwota { get; set; } = 50;
         public void SetPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                PasswordUser = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string inputHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return inputHash == PasswordUser;
            }
        }
    }