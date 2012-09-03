using System.Net.Mail;
using DA.Dinners.Domain;
using Mvc.Mailer;

namespace UI.Mailers
{
    public class UserMailer : MailerBase, IUserMailer
    {
        public UserMailer()
            : base()
        {
            MasterName = "_Layout";
        }

        public virtual MailMessage NegativeBalance(string userEmail, decimal balance)
        {
            var mailMessage = new MailMessage { Subject = "Напоминание:" };
            ViewBag.balance = balance;
            mailMessage.To.Add(userEmail);
            PopulateBody(mailMessage, viewName: "NegativeBalance");

            return mailMessage;
        }

        public virtual MailMessage OrderSent(string userEmail, Order order)
        {
            var mailMessage = new MailMessage { Subject = "Заказ отправлен" };
            ViewBag.order = order;
            mailMessage.To.Add(userEmail);
            PopulateBody(mailMessage, viewName: "OrderSent");

            return mailMessage;
        }
    }
}