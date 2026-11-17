
using System;
using System.Collections.Generic;

namespace ShopTARpe25.Core.Domain
{
    public class Spaceship
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Classification { get; set; } = string.Empty;
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public int? EnginePower { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public IEnumerable<FileToApi> FileToApiDtos { get; set; }
            = new List<FileToApi>();
    }
}
