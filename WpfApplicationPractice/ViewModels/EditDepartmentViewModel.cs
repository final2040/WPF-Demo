using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApplicationPractice.Helpers;
using WpfApplicationPractice.Models;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.ViewModels
{
    public class EditDepartmentViewModel:ViewModelBase
    {
        private readonly DepartmentData _departmentDataContext = new DepartmentData();
        private string _name;
        private readonly JobData _jobData = new JobData();
        private readonly DepartmentData _departmentData = new DepartmentData();

        public EditDepartmentViewModel()
        {
            
        }

        public EditDepartmentViewModel(object entity)
        {
            if (entity.GetType() != typeof(DepartmentEntity))
                throw new ArgumentException(ApplicationText.InvalidTypeErrror,nameof(entity));

            DepartmentEntity department = (DepartmentEntity) entity;
            Id = department.Id;
            Name = department.Name;
            Save = new RelayCommand(SaveEntity, ValidateViewModel);
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof (ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ ]+$", ErrorMessageResourceType = typeof (ApplicationText),
            ErrorMessageResourceName = "Employee_Name_Format_Error")]
        [StringLength(90, ErrorMessageResourceType = typeof (ApplicationText),
            ErrorMessageResourceName = "Employee_Name_Length_Error")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand Save { get; set; }

        public void SaveEntity(object window)
        {
            try
            {
                DepartmentEntity department = GetEmployee();
                _departmentDataContext.AddOrUpdate(department);
                ((IView)window).Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void ShowErrorMessage(string error)
        {
            MessageBox.Show(string.Format(ApplicationText.UnexpectedError_Text, error),
                ApplicationText.UnexpectedError_Caption, MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private DepartmentEntity GetEmployee()
        {
            return new DepartmentEntity()
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}