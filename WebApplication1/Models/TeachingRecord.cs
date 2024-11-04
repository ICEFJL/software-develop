using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TeachingRecord
    {
        [Key]
        public int RecordID { get; set; }
        [Required(ErrorMessage = "支教ID不能为空")]
        public int TeachID { get; set; }

        [ForeignKey("TeachID")]
        public TeachingInfo TeachingInfo { get; set; }

        [Required(ErrorMessage = "学生ID不能为空")]
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        [Required]
        public string Evaluation { get; set; } = "暂无";

        [Required]
        public string Status { get; set; } = "申请";
    }
}
