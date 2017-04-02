using System.ComponentModel.DataAnnotations;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.Models
{
    public class DepartmentEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ ]+$", ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Department_Name_Format_Error")]
        public string Name { get; set; }
    }
}