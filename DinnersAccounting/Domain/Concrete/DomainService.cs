using System.DirectoryServices;
using System.Text;

namespace DA.Dinners.Domain.Concrete
{
    public class DomainService
    {
        private static DomainService instance;

        private DomainService() { }

        public static DomainService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DomainService();
                }
                return instance;
            }
        }

        public string GetFullName(string username)
        {
            string name = GetProperty(username, "CN");
            if (name == string.Empty)
                name = username;
            return name;
        }

        public string GetCity(string username)
        {
            return GetProperty(username, "l");
        }

        public string GetPosition(string username)
        {
            return GetProperty(username, "title");
        }

        public string GetCountry(string username)
        {
            return GetProperty(username, "co");
        }

        public string GetEmail(string username)
        {
            return GetProperty(username, "userPrincipalName");
        }

        public string GetOffice(string username)
        {
            return GetProperty(username, "physicalDeliveryOfficeName");
        }

        private string GetProperty(string username, string key, int index = 0)
        {
            StringBuilder info = new StringBuilder();
            using (DirectoryEntry de = new DirectoryEntry("LDAP://" + GetDomain(username)))
            {
                using (DirectorySearcher adSearch = new DirectorySearcher(de))
                {
                    adSearch.Filter = "(sAMAccountName=" + GetLogin(username) + ")";

                    SearchResult adSearchResult = adSearch.FindOne();
                    if (adSearchResult != null)
                    {
                        DirectoryEntry de1 = adSearchResult.GetDirectoryEntry();
                        return de1.Properties[key][index].ToString();
                    }
                    else
                        return string.Empty;
                }
            }
        }

        private string GetDomain(string username)
        {
            string s = username;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(0, stop) : string.Empty;
        }

        private string GetLogin(string username)
        {
            string s = username;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : string.Empty;
        }
    }
}