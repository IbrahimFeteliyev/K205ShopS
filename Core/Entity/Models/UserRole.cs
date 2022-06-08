using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Models
{
    public class UserRole : IEntity
    {
        public int Id { get; set; }
        public int K205UserId { get; set; }
        public K205User K205User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
