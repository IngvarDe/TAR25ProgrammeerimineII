using ShopTARpe25.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }

        //see muutuja hakkab näitama, kus asub meie file
        public string? ExistingFilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public IEnumerable<FileToApi> FileToApiDtos { get; set; }
           = new List<FileToApi>();
    }
}
