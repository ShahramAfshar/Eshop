using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace EStore.Application.Services.Products.Commands.AddNewProduct
{
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public AddNewProductService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public ResultDto Execute(RequstAddNewProductDto request)
        {
            try
            {
                var category = _context.Categories.Find(request.CategoryId);

                var product = new Product()
                {
                    Name = request.Name,
                    Brand = request.Brand,
                    Price = request.Price,
                    Description = request.Description,
                    Inventory = request.Inventory,
                    Category = category,
                    IsDisplay = request.IsDisplay
                };

                _context.Products.Add(product);

                List<ProductImage> productImages = new List<ProductImage>();

                foreach (var item in request.Images)
                {
                    var uploadResult = UploadFile(item);

                    productImages.Add(new ProductImage()
                    {
                        Product = product,
                        SourceImage = uploadResult.FileNameAddress

                    });

                }
                _context.ProductImages.AddRange(productImages);


                List<ProductFeature> productFeatures = new List<ProductFeature>();
                foreach (var item in request.Feature)
                {
                    productFeatures.Add(new ProductFeature()
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product

                    });

                }
                _context.ProductFeatures.AddRange(productFeatures);

                _context.SaveChanges();

                return new ResultDto()
                {

                    IsSuccess = true,
                    Message = "محصول با موفیت اضافه شد"
                };



            }
            catch (Exception)
            {

                throw;
            }


        }


        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }

    }




}
