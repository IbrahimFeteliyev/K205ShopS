using Business.Abstract;
using Core.Entity.Models;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserRoleManager : IUserRoleManager
    {

        private readonly IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public void AddUserRole(AddUserRoleDTO userrole)
        {
            UserRole userRole = new()
            {
                K205UserId = userrole.K205UserId,
                RoleId = userrole.RoleId,
            };
            _userRoleDal.Add(userRole);
        }

        public List<UserRoleDTO> GetAllUserRoles()
        {
            return _userRoleDal.GetAllUserRole();
        }

        public UserRoleDTO GetUserRoleById(int id)
        {
            return _userRoleDal.GetUserRole(id);
        }


    }
}
