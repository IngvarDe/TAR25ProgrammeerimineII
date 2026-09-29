namespace ShopTARpe25.Models.Kindergarten
{
    public class KindergartenCreateViewModel
    {
        public string GroupName { get; set; } = string.Empty;
        public int? ChildrenCount { get; set; }
        public string KindergartenName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
    }
}