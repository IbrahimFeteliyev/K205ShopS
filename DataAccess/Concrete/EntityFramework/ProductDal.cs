using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductDal : EfEntityRepositoryBase<Product, ShopDbContext>, IProductDal
    {
        public ProductDTO FindById(int id)
        {
            using (ShopDbContext context = new())
            {
                var product = context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
                var productPictures = context.ProductPicture.Where(x => x.ProductId == id).ToList();
                var comments = context.Comments.Where(x => x.ProductId == product.Id).ToList();


                decimal ratingSum = 0;
                int ratingCount = 0;

                List<CommentDTO> commentResult = new();

                for (int i = 0; i < comments.Count; i++)
                {
                    CommentDTO comment = new()
                    {
                        ProductId = product.Id,
                        UserName = comments[i].UserName,
                        UserEmail = comments[i].UserEmail,
                        Review = comments[i].Review,
                        Ratings = comments[i].Ratings
                    };
                    commentResult.Add(comment);
                }

                List<string> pictures = new();
                foreach (var picture in productPictures)
                {
                    pictures.Add(picture.PhotoUrl);
                }

                foreach (var rating in comments.Where(x=>x.Ratings != 0))
                {
                    ratingCount++;
                    ratingSum += rating.Ratings;
                }

                if (ratingCount == 0)
                    ratingSum = 0;
                else
                    ratingSum = ratingSum / ratingCount;

                ProductDTO result = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    SKU = product.SKU,
                    ReviewCount = ratingCount,
                    Summary = product.Summary,
                    ProductPictures = pictures,
                    CoverPhoto = product.CoverPhoto,
                    IsSlider = product.IsSlider,
                    Rating = Math.Round(ratingSum, 1),
                    Comments = commentResult,
                };
                return result;
            }
        }

        public List<ProductDTO> GetAllProduct()
        {
            using (ShopDbContext context = new())
            {

                var products = context.Products.Include(x => x.Category).Include(x => x.ProductPicture).ToList();
                var productPictures = context.ProductPicture;
                var ratings = context.Comments;

                List<ProductDTO> result = new();


                // id = 1 | 5 5 4 3 2 1
                // id = 2 | 2 3 4 2 2 1


                for (int i = 0; i < products.Count; i++)
                {
                    decimal ratingSum = 0;
                    int ratingCount = 0;
                    List<string> pictures = new();
                    foreach (var item in productPictures.Where(x => x.ProductId == products[i].Id))
                    {
                        pictures.Add(item.PhotoUrl);
                    }

                    foreach (var rating in ratings.Where(x => x.ProductId == products[i].Id && x.Ratings != 0))
                    {
                        ratingCount++;
                        ratingSum += rating.Ratings;
                    }

                    if (ratingCount == 0)
                        ratingSum = 0;
                    else
                        ratingSum = ratingSum / ratingCount;


                    ProductDTO productList = new()
                    {
                        Id = products[i].Id,
                        Name = products[i].Name,
                        Description = products[i].Description,
                        CategoryName = products[i].Category.Name,
                        Price = products[i].Price,
                        SKU = products[i].SKU,
                        Summary = products[i].Summary,
                        ProductPictures = pictures,
                        CoverPhoto = products[i].CoverPhoto,
                        IsSlider = products[i].IsSlider,
                        Rating = Math.Round(ratingSum, 1)
                    };
                    result.Add(productList);
                }




                return result;
            }
        }
    }
}
