using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Xml;
using TARge25Shop.Core.Domain;
using TARge25Shop.Core.Dto;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;
using TARge25SHop.Core.Domain;
using TARge25SHop.Core.Dto;
using TARge25SHop.Core.ServiceInterface;


namespace TARge25Shop.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly TARge25ShopContext _context;

        public FileServices
            (
                IHostEnvironment webHost,
                TARge25ShopContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }


        
        public void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain)
        {
            //toimub kontroll, kas on faile v]i ei ole
            if (dto.Files != null && dto.Files.Count > 0)
            {
                //tuleb kasutada foreachi, et mitu faili ülesse laadida
                foreach (var file in dto.Files)
                {
                    //teha muutuja, mis salvestab faili sisu
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            KindergartenId = domain.Id
                        };

                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.AddAsync(files);
                    }
                }
            }
        }

        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            _context.FileToDatabases.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            _context.FileToDatabases.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task RemoveImageFromDatabase(FileToDatabaseDto[] images)
        {
            foreach (var dto in images)
            {
                var image = await _context.FileToDatabases
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (image != null)
                {
                    _context.FileToDatabases.Remove(image);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}