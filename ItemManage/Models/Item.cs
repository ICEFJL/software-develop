using System;
using System.ComponentModel.DataAnnotations;

namespace ItemManage.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "物品编码不能为空")]
        [StringLength(50, ErrorMessage = "物品编码长度不能超过50个字符")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "物品名称不能为空")]
        [StringLength(100, ErrorMessage = "物品名称长度不能超过100个字符")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "类别长度不能超过50个字符")]
        public string? Category { get; set; }

        [StringLength(50, ErrorMessage = "产地长度不能超过50个字符")]
        public string? Origin { get; set; }

        [StringLength(100, ErrorMessage = "规格长度不能超过100个字符")]
        public string? Specification { get; set; }

        [StringLength(50, ErrorMessage = "型号长度不能超过50个字符")]
        public string? Model { get; set; }

        [StringLength(200, ErrorMessage = "图片路径长度不能超过200个字符")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "数量不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "数量必须为非负整数")]
        public int Quantity { get; set; }
    }
}