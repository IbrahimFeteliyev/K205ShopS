using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ReviewCount { get; set; } // Camel Case => for api snake case = review_count => Kebab Case = review-count
        public string Summary { get; set; }
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string CoverPhoto { get; set; }
        public bool IsSlider { get; set; }
        public decimal Rating { get; set; }
        public List<string> ProductPictures { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}
