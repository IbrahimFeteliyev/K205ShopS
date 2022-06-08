using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDal _productDal;
        private readonly IProductPictureManager _productPictureManager;

        public ProductManager(IProductDal productDal, IProductPictureManager productPictureManager)
        {
            _productDal = productDal;
            _productPictureManager = productPictureManager;
        }

        public void AddProduct(AddProductDTO productDTO)
        {
            
            Product product = new()
            {
                Name = productDTO.Name,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
                Price = productDTO.Price,
                SKU = productDTO.SKU,
                Summary = productDTO.Summary,
                CoverPhoto = productDTO.CoverPhoto,
            };
            _productDal.Add(product);
            

            for (int i = 0; i < productDTO.ProductPicture.Count; i++)
            {
                productDTO.ProductPicture[i].ProductId = product.Id;
                _productPictureManager.AddProductPicture(productDTO.ProductPicture[i]);
            }
            
            
        }

        public List<ProductDTO> GetAllProductList()
        {
            return _productDal.GetAllProduct();
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAll();
        }

        public ProductDTO GetProductById(int id)
        {
            return _productDal.FindById(id);
        }
    }
}
