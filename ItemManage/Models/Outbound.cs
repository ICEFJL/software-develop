using System;
using System.ComponentModel.DataAnnotations;

namespace ItemManage.Models
{
    public class Outbound
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "物品编码不能为空")]
        [StringLength(50, ErrorMessage = "物品编码长度不能超过50个字符")]
        public string? ItemCode { get; set; }

        [Required(ErrorMessage = "申请日期不能为空")]
        [DataType(DataType.Date, ErrorMessage = "无效的日期格式")]
        public DateTime ApplyDate { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "用户ID不能为空")]
        [StringLength(50, ErrorMessage = "用户ID长度不能超过50个字符")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "状态不能为空")]
        [StringLength(20, ErrorMessage = "状态长度不能超过20个字符")]
        public string? Status { get; set; }
    }
}
