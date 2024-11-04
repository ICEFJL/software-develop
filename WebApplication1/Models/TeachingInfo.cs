using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TeachingInfo
    {
        [Key]
        public int TeachID { get; set; }
        [Required(ErrorMessage = "学校名不能为空")]
        public string School { get; set; }
        [Required(ErrorMessage = "地名不能为空")]
        public string Location { get; set; }

        [Required(ErrorMessage = "开始日期不能为空")]
        [DataType(DataType.Date, ErrorMessage = "无效的日期格式")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "结束日期不能为空")]
        [DataType(DataType.Date, ErrorMessage = "无效的日期格式")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "科目不能为空")]
        [StringLength(50, ErrorMessage = "科目长度不能超过50个字符")]
        public string Subject { get; set; }
        [Required]
        public int mem { get; set; } = 0;//报名人数
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        public int maxmem {  get; set; }//最大人数
        public ICollection<TeachingRecord> Teachings { get; set; } = new List<TeachingRecord>();
    }
}
