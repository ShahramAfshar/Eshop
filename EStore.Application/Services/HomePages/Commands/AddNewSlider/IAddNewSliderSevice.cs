using EStore.Application.Interfaces.Contexts;
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

namespace EStore.Application.Services.HomePages.Commands.AddNewSlider
{
    public interface IAddNewSliderSevice
    {
        ResultDto Execute(IFormFile fromFile,string link);
    }

    public class AddNewSliderSevice : IAddNewSliderSevice
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;

        public AddNewSliderSevice(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(IFormFile fromFile, string link)
        {
           var resultUpload= UploadFile(fromFile);

            Slider slider = new Slider() { 
            
                Link=link,
                Src=resultUpload.FileNameAddress            
            };

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return new ResultDto() { IsSuccess=true,Message="اسلایدر با موفقیت اضافه شد"};


        }



        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\HomePages\Slider\";
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

    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }



}
