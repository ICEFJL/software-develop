using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密码长度必须在6到100个字符之间")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "学生姓名不能为空")]
        [StringLength(100, ErrorMessage = "学生姓名长度不能超过100个字符")]
        public string Name { get; set; }
        [Required(ErrorMessage = "性别不能为空")]
        [StringLength(10, ErrorMessage = "性别长度不能超过10个字符")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "出生日期不能为空")]
        [DataType(DataType.Date, ErrorMessage = "无效的日期格式")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "电话号码不能为空")]
        [Phone(ErrorMessage = "无效的电话号码格式")]
        [StringLength(15, ErrorMessage = "电话号码长度不能超过15个字符")]
        public string PhoneNumber { get; set; }
        public ICollection<TeachingRecord> Teachings { get; set; } = new List<TeachingRecord>();
    }
}
