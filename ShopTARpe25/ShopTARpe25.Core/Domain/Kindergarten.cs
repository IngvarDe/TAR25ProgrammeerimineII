
using System;
using System.Collections.Generic;

namespace ShopTARpe25.Core.Domain
{
    public class Kindergarten
    {
        public Guid Id { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public int? ChildrenCount { get; set; }

        public string KindergartenName { get; set; } = string.Empty;

        public string TeacherName { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public IEnumerable<FileToApi> FileToApiDtos { get; set; }
            = new List<FileToApi>();
    }
}
