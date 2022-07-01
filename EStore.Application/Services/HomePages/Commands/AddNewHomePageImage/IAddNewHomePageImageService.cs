using EStore.Application.Interfaces.Contexts;
using EStore.Application.Services.HomePages.Commands.AddNewSlider;
using EStore.Common.Dto;
using EStore.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.HomePages.Commands.AddNewHomePageImage
{
    public interface IAddNewHomePageImageService
    {
        ResultDto Execute(AddNewHomePageImageDto addNewHomePageImageDto);
    }


    public class AddNewHomePageImageService : IAddNewHomePageImageService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public AddNewHomePageImageService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(AddNewHomePageImageDto request)
        {
            var resultUpload = UploadFile(request.fromFile);

            var homePage = new HomePageImages()
            {
                ImageLocation = request.ImageLocation,
                Link = request.Link,
                SourceImage = resultUpload.FileNameAddress

            };

            _context.HomePageImages.Add(homePage);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "",
            };


        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\HomePages\HomePage\";
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
    public class AddNewHomePageImageDto
    {
        public IFormFile fromFile { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
