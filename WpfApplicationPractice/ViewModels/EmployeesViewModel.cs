using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApplicationPractice.Helpers;
using WpfApplicationPractice.Models;
using WpfApplicationPractice.Properties;
using WpfApplicationPractice.Views;

namespace WpfApplicationPractice.ViewModels
{
    public class EmployeesViewModel:ViewModel
    {
        private readonly EmployeeData _employeeData = new EmployeeData();
        private int _selectedIndex;
        private string _textToFind;
        private ObservableCollection<EmployeeEntity> _employees;

        public EmployeesViewModel()
        {
            Refresh();
            Find = new RelayCommand(FindEntity, () => true);
            Edit = new RelayCommand(EditEntity, IsItemSelected);
            New = new RelayCommand(NewEntity, () => true);
            Delete = new RelayCommand(DeleteEntity, IsItemSelected);
            ClearFilter = new RelayCommand(ClearFilters, () => true);
        }

        private void Refresh()
        {
            Employees = new ObservableCollection<EmployeeEntity>(_employeeData.GetAll());
        }

        public ObservableCollection<EmployeeEntity> Employees
        {
            get { return _employees; }
            private set
            {
                if (_employees != value)
                {
                    _employees = value;
                    NotifyPropertyChanged();
                }
                
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Required(ErrorMessageResourceType = typeof(ApplicationText), ErrorMessageResourceName = "Required_Error")]
        public string TextToFind
        {
            get { return _textToFind; }
            set
            {
                if (_textToFind != value)
                {
                    _textToFind = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public EmployeeEntity SelectedEmployee
        {
            get
            {
                if (SelectedIndex >= 0)
                    return Employees[SelectedIndex];
                return null;
            }
        }

        public ICommand Find { get; }
        public ICommand Edit { get; }
        public ICommand New { get;  }
        public ICommand Delete { get; }
        public ICommand ClearFilter { get; }

        private void FindEntity(object parameter)
        {
            if (string.IsNullOrWhiteSpace(TextToFind))
            {
                Refresh();
            }
            Employees = new ObservableCollection<EmployeeEntity>(
                _employeeData.Get(e => e.Name.ToLower().Contains(TextToFind.ToLower())));
        }

        private void EditEntity(object parameter)
        {
            throw new NotImplementedException();
        }

        private void DeleteEntity(object parameter)
        {
            throw new NotImplementedException();
        }

        private void NewEntity(object parameter)
        {
            IView view = WindowProvider.GetWindow("EditEmployee", new EmployeeEntity());
            view.DataContext = new EditEmployeeViewModel(new EmployeeEntity());
            view.ShowDialog();
            Refresh();
        }

        private bool IsItemSelected()
        {
            return _selectedIndex >= 0;
        }

        private void ClearFilters(object parameter)
        {
            TextToFind = string.Empty;
            Refresh();
        }

    }

    internal class WindowProvider
    {
        public static IView GetWindow(string windowName, Object entity)
        {
            switch (windowName)
            {
                case "EditEmployee":
                    return new EditEmployeeView();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}