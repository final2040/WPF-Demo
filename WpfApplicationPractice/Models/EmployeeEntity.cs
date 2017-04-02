
using System.ComponentModel.DataAnnotations;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.Models
{
    public class EmployeeEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ ]+$", ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Employee_Name_Format_Error")]
        [StringLength(90, ErrorMessageResourceType = typeof (ApplicationText), ErrorMessageResourceName = "Employee_Name_Length_Error")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [Range(18, 99, ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Employee_Age_Range_Error")]
        public int Age { get; set; }

        public DepartmentEntity Department { get; set; }
        public JobEntity Job { get; set; }
    }
}