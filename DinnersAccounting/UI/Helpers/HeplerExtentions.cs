using System.Text;
using System.Web;
using DA.Dinners.Domain.Concrete;

namespace UI.Heplpers
{
    public static class HeplerExtentions
    {
        public static HtmlString ADUserInfo(this System.Web.Mvc.HtmlHelper helper, System.Security.Principal.IPrincipal user)
        {
            StringBuilder info = new StringBuilder();
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Имя", DomainService.Instance.GetFullName(user.Identity.Name));
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Город", DomainService.Instance.GetCity(user.Identity.Name));
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Позиция", DomainService.Instance.GetPosition(user.Identity.Name));
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Оффис", DomainService.Instance.GetOffice(user.Identity.Name));
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Страна", DomainService.Instance.GetCountry(user.Identity.Name));
            info.AppendFormat("<b>{0}</b>: {1}<br/>", "Email", DomainService.Instance.GetEmail(user.Identity.Name));
            return new HtmlString(info.ToString());
        }
    }
}