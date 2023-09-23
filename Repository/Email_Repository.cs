namespace Ecommerce_Api.Repository
{
    public class Email_Repository
    {
        //private string GenerateNewPassword()
        //{
        //    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    Random random = new Random();
        //    string newpassword = new string(Enumerable.Repeat(chars, 10)
        //        .Select(s => s[random.Next(s.Length)]).ToArray());
        //    return newpassword;
        //}


        //private void sendEmail(string email, string newPassword)
        //{
        //    // Implement your logic to send the new password to the user via email or SMS
        //    // For example, using an email sending library like SendGrid
        //    // This is a simplified example and you should integrate a proper email or SMS service
        //    var apiKey = "YOUR_SENDGRID_API_KEY";
        //    var client = new SendGridClient(apiKey);
        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress("from@example.com", "Example User"),
        //        Subject = "Your New Password",
        //        PlainTextContent = $"Your new password is: {newPassword}",
        //        HtmlContent = $"<strong>Your new password is: {newPassword}</strong>"
        //    };
        //    msg.AddTo(new EmailAddress(email));
        //    var response = client.SendEmailAsync(msg).Result;

        //    // You should handle errors and responses from the email service in a real implementation
        //}
    }
}
