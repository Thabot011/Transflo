using System.ComponentModel.DataAnnotations;

namespace Transflo.Contracts.Contract
{
    public class DriverGetAllDto
    {
        public DriverForFilterDto DriverFilter { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }
        public string SortProperty { get; set; }
        public SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        asc = 1,
        desc = 2
    }
}
