using Enums;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public class DiscountCreationDto
    {
        public DiscountType Type { get; set; }
        public decimal Amount { get; set; }
        public DiscountTargetType TargetType { get; set; }
        public string TargetId { get; set; }
    }
}
