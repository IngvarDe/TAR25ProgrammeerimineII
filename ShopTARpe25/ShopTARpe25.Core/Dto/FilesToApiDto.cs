using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.Core.Dto
{
    public class FilesToApiDto
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
