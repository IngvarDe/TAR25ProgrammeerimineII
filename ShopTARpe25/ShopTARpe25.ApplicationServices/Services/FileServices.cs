
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARpe25.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARpe25Context _context;
        private readonly IHostEnvironment _webHost;

        public FileServices(
            ShopTARpe25Context context,
            IHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceship domain)
        {
            if (dto.Files == null || dto.Files.Count == 0)
                return;

            string uploadsFolder = GetUploadsFolder();
            Directory.CreateDirectory(uploadsFolder);

            foreach (var file in dto.Files)
            {
                if (file == null || file.Length == 0)
                    continue;

                string uniqueFileName =
                    Guid.NewGuid().ToString() + "_" +
                    Path.GetFileName(file.FileName);

                string filePath = Path.Combine(
                    uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(
                    filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                FileToApi image = new FileToApi
                {
                    Id = Guid.NewGuid(),
                    ExistingFilePath = uniqueFileName,
                    SpaceshipId = domain.Id
                };

                _context.FileToApis.Add(image);
            }

            _context.SaveChanges();
        }

        public async Task<FileToApi> RemoveImageFromApi(FilesToApiDto dto)
        {
            var image = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (image == null)
                return null;

            DeletePhysicalFile(image.ExistingFilePath);

            _context.FileToApis.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(
            FilesToApiDto[] dto)
        {
            var removedImages = new List<FileToApi>();

            if (dto == null || dto.Length == 0)
                return removedImages;

            foreach (var item in dto)
            {
                var image = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.Id == item.Id);

                if (image == null)
                    continue;

                DeletePhysicalFile(image.ExistingFilePath);

                _context.FileToApis.Remove(image);
                removedImages.Add(image);
            }

            await _context.SaveChangesAsync();

            return removedImages;
        }

        public void FilesToApi(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files == null || dto.Files.Count == 0)
                return;

            string uploadsFolder = GetUploadsFolder();
            Directory.CreateDirectory(uploadsFolder);

            foreach (var file in dto.Files)
            {
                if (file == null || file.Length == 0)
                    continue;

                string uniqueFileName =
                    Guid.NewGuid().ToString() + "_" +
                    Path.GetFileName(file.FileName);

                string filePath = Path.Combine(
                    uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(
                    filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                FileToApi image = new FileToApi
                {
                    Id = Guid.NewGuid(),
                    ExistingFilePath = uniqueFileName,
                    KindergartenId = domain.Id
                };

                _context.FileToApis.Add(image);
            }

            _context.SaveChanges();
        }

        private string GetUploadsFolder()
        {
            return Path.Combine(
                _webHost.ContentRootPath,
                "wwwroot",
                "multipleFileUpload");
        }

        private void DeletePhysicalFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            string filePath = Path.Combine(
                GetUploadsFolder(),
                Path.GetFileName(fileName));

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
