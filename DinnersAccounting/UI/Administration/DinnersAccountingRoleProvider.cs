using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using DA.Dinners.Domain;
using DA.Dinners.Domain.Abstract;

namespace UI.Administration
{
    public class DinnersAccountingRoleProvider : RoleProvider
    {
        private IRoleRepository RolesRepository;
        private IPersonRepository PersonRepository;

        public DinnersAccountingRoleProvider()
            : this(DependencyResolver.Current.GetService<IRoleRepository>(), DependencyResolver.Current.GetService<IPersonRepository>())
        { }

        public DinnersAccountingRoleProvider(IRoleRepository rolesRepository, IPersonRepository personRepository)
        {
            this.RolesRepository = rolesRepository;
            this.PersonRepository = personRepository;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //TODO: Change it for better:
            this.PersonRepository = DependencyResolver.Current.GetService<IPersonRepository>();
            Person person = null;

            foreach (Person person_ in PersonRepository.All)
                if (username.ToLower().Equals(person_.DomainName.ToLower()))
                {
                    person = person_;
                    break;
                }

            if (person == null)
                return new string[] { };

            List<string> roles_ = new List<string>();
            foreach (var role in person.Roles)
                roles_.Add(role.RoleName);
            return roles_.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}