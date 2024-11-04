using System;
using System.ComponentModel.DataAnnotations;

namespace ItemManage.Models
{
    public class Inbound
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "物品编码不能为空")]
        [StringLength(50, ErrorMessage = "物品编码长度不能超过50个字符")]
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "购买日期不能为空")]
        [DataType(DataType.Date, ErrorMessage = "无效的日期格式")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "单价不能为空")]
        [Range(0.01, double.MaxValue, ErrorMessage = "单价必须大于0")]
        [DataType(DataType.Currency, ErrorMessage = "无效的货币格式")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "总价不能为空")]
        [Range(0.01, double.MaxValue, ErrorMessage = "总价必须大于0")]
        [DataType(DataType.Currency, ErrorMessage = "无效的货币格式")]
        public decimal TotalPrice { get; set; }
    }
}