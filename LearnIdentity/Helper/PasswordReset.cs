using System.Net.Mail;

namespace LearnIdentity.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link,string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("netcoreidentitylearn@gmail.com");
            mail.To.Add(email);

            mail.Subject = $"Şifre Sıfırlama";
            mail.Body = "<h2>Şifrenizi sıfırlamak için lütfen aşağıdaki linke tıklayınız</h2><hr/>";
            mail.Body += $"<a href='{link}'>Şifre yenileme linki</a>";
            mail.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("netcoreidentitylearn@gmail.com", "smtpsifre67");

            smtp.Send(mail);
        }
    }
}
