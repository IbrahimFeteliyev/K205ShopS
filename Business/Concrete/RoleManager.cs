using Business.Abstract;
using Core.Entity.Models;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{   
    public class RoleManager : IRoleManager
    {
        private readonly IRoleDal _roledal;

        public RoleManager(IRoleDal roledal)
        {
            _roledal = roledal;
        }

        public void AddRole(Role role)
        {
            _roledal.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _roledal.Update(role);
        }

        public List<Role> GetAllRoles()
        {
            return _roledal.GetAll();
        }

        public Role GetRole(int userId)
        {
            return _roledal.GetUserRole(userId);
        }

        public void RemoveRole(Role role)
        {
            _roledal.Delete(role);
        }
    }
}
