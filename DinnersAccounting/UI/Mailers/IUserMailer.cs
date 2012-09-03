using System.Net.Mail;
using DA.Dinners.Domain;

namespace UI.Mailers
{
    public interface IUserMailer
    {
        MailMessage NegativeBalance(string userEmail, decimal balance);

        MailMessage OrderSent(string userEmail, Order order);
    }
}