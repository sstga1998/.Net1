using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Modal
{
    public class student
    {
        public string IDStudent { get; set; } //get:chỉ cho đọc thôi, set: cho thêm phép chỉnh sửa
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GENDER Gender { get; set; }
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public ICollection<HistoryLearning> ListHistoryLearning { get; set; }
    }
    public enum GENDER
    {
        Male, Female, Other
    }
}
