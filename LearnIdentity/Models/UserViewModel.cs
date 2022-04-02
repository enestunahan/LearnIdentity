using System.ComponentModel.DataAnnotations;
using System;
using LearnIdentity.Enums;

namespace LearnIdentity.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Booş Geçilemez")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email alanı gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Profil Fotoğrafı")]
        public string Picture { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }
    }
}
