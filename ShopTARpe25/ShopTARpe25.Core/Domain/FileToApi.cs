using System;

namespace ShopTARpe25.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }

        public string? ExistingFilePath { get; set; }

        public Guid? SpaceshipId { get; set; }
        public Spaceship? Spaceship { get; set; }

        public Guid? KindergartenId { get; set; }
        public Kindergarten? Kindergarten { get; set; }
    }
}
