using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentDTO
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Review { get; set; }
        public int Ratings { get; set; }
        public int ProductId { get; set; }
    }
}
