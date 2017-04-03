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
    public class EditEmployeeViewModel:ViewModelBase
    {
        private readonly EmployeeData _employeeData = new EmployeeData();
        private string _name;
        private int _age;
        private int _job;
        private int _department;
        private readonly JobData _jobData = new JobData();
        private readonly DepartmentData _departmentData = new DepartmentData();

        public EditEmployeeViewModel()
        {
            
        }

        public EditEmployeeViewModel(object entity)
        {
            if (entity.GetType() != typeof(EmployeeEntity))
                throw new ArgumentException(ApplicationText.InvalidTypeErrror,nameof(entity));

            EmployeeEntity employee = (EmployeeEntity) entity;
            Id = employee.Id;
            Name = employee.Name;
            Age = employee.Age;
            Job = employee.Job?.Id ?? 1;
            Department = employee.Department?.Id ?? 1;
            JobList = _jobData.GetAll().Select(j => new KeyValuePair<int,string>(j.Id,j.Name)).ToArray();
            DepartmentList = _departmentData.GetAll().Select(j=> new KeyValuePair<int,string>(j.Id,j.Name)).ToArray();
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

        [Required(ErrorMessageResourceType = typeof (ApplicationText), ErrorMessageResourceName = "Required_Error")]
        [Range(18, 99, ErrorMessageResourceType = typeof (ApplicationText),
            ErrorMessageResourceName = "Employee_Age_Range_Error")]
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Required(ErrorMessageResourceType = typeof (ApplicationText), ErrorMessageResourceName = "Required_Error")]
        public int Job
        {
            get { return _job; }
            set
            {
                if (_job != value)
                {
                    _job = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Required(ErrorMessageResourceType = typeof (ApplicationText), ErrorMessageResourceName = "Required_Error")]
        public int Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public KeyValuePair<int, string>[] JobList { get; private set; }
        public KeyValuePair<int, string>[] DepartmentList { get; private set; }

        public ICommand Save { get; set; }

        public void SaveEntity(object window)
        {
            try
            {
                EmployeeEntity employee = GetEmployee();
                _employeeData.AddOrUpdate(employee);
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

        private EmployeeEntity GetEmployee()
        {
            return new EmployeeEntity()
            {
                Id = this.Id,
                Name = this.Name,
                Age = this.Age,
                Department = _departmentData.GetFirst(d => d.Id== Department),
                Job = _jobData.GetFirst(j => j.Id == Job)
            };
        }
    }
}