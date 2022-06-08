
using Core.DataAccess.EntityFramework;
using Core.Entity.Models;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRoleDal : EfEntityRepositoryBase<UserRole, ShopDbContext>, IUserRoleDal
    {
        public List<UserRoleDTO> GetAllUserRole()
        {
            using (ShopDbContext context = new())
            {
                var userroles = context.UserRoles.Include(x => x.Role).Include(x => x.K205User).ToList();

                List<UserRoleDTO> result = new();


                for (int i = 0; i < userroles.Count; i++)
                {
                    UserRoleDTO user = new()
                    {
                        Id = userroles[i].Id,
                        K205UserName = userroles[i].K205User.FullName,
                        RoleName = userroles[i].Role.Name
                    };
                    result.Add(user);
                }
                return result;
            }
        }

        public UserRoleDTO GetUserRole(int id)
        {
            using (ShopDbContext context = new())
            {
                var userroles = context.UserRoles.Include(x => x.Role).Include(x => x.K205User).FirstOrDefault(x => x.Id == id);

                UserRoleDTO result = new()
                {
                    Id = userroles.Id,
                    K205UserName = userroles.K205User.FullName,
                    RoleName = userroles.Role.Name
                };

                return result;
            }
        }
    }
}
