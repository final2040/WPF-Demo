using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using WpfApplicationPractice.Helpers;
using WpfApplicationPractice.Models;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly EmployeeData _employeeData = new EmployeeData();
        private int _selectedTab;
        private string _textToFind;

        public MainViewModel()
        {
            FindCommand = new RelayCommand(Find, IsItemSelected);
            EditCommand = new RelayCommand(Edit, IsItemSelected);
            NewCommand = new RelayCommand(New, IsItemSelected);
            DeleteCommand = new RelayCommand(Delete, IsItemSelected);
            ClearFilterCommand = new RelayCommand(ClearFilters,IsItemSelected);
            CloseTabCommand = new RelayCommand(CloseTab, IsItemSelected);
            CreateEmployeeTabCommand = new RelayCommand(CreateEmployeeTab, () => true);
            CreateDepartmentTabCommand = new RelayCommand(CreateDepartmentTab, () => true);
            CreateJobTabCommand = new RelayCommand(CreateJobTab, () => true);

            OpenTabs.Add(new EmployeeListViewModel());
        }
        
        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<IListViewModel> OpenTabs { get; set; } =new ObservableCollection<IListViewModel>();
        public IListViewModel CurrentView { get { return OpenTabs[SelectedTab]; } }

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

        public ICommand FindCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand NewCommand { get;  }
        public ICommand DeleteCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand CloseTabCommand { get; }
        public ICommand CreateJobTabCommand { get; set; }
        public ICommand CreateDepartmentTabCommand { get; set; }
        public ICommand CreateEmployeeTabCommand { get; set; }

        private void Find(object parameter)
        {
            CurrentView.FilterCommand?.Execute(TextToFind);
        }

        private void Edit(object parameter)
        {
            CurrentView.EditCommand?.Execute(null);
        }

        private void Delete(object parameter)
        {
            CurrentView.DeleteCommand?.Execute(null);
        }

        private void New(object parameter)
        {
            CurrentView.AddCommand?.Execute(null);
        }

        private bool IsItemSelected()
        {
            return _selectedTab >= 0;
        }

        private void ClearFilters(object parameter)
        {
            TextToFind = string.Empty;
            CurrentView.ClearFilterCommand?.Execute(null);
        }

        private void Refresh()
        {
            CurrentView.UpdateViewCommand?.Execute(null);
        }

        private void CloseTab(object obj)
        {
            OpenTabs.RemoveAt(SelectedTab);
        }

        private void CreateDepartmentTab(object obj)
        {
            throw new System.NotImplementedException();
        }
        private void CreateJobTab(object obj)
        {
            throw new System.NotImplementedException();
        }
        private void CreateEmployeeTab(object obj)
        {
            OpenTabs.Add(new EmployeeListViewModel());
            SelectedTab = OpenTabs.Count - 1;
        }

    }
}