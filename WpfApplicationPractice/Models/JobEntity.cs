using System.ComponentModel.DataAnnotations;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.Models
{
    public class JobEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [RegularExpression("^[a-zA-Z·ÈÌÛ˙¸¡…Õ”⁄‹ ]+$", ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Job_Name_Format_Error")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}