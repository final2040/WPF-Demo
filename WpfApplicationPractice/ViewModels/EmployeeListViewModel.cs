using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApplicationPractice.Helpers;
using WpfApplicationPractice.Models;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.ViewModels
{
    public class EmployeeListViewModel:ViewModelBase, IListViewModel
    {
        private int _selectedIndex;
        private readonly EmployeeData _dataContext = new EmployeeData();

        public EmployeeListViewModel()
        {
            Name = ApplicationText.Employee_View_Name;
            EditCommand = new RelayCommand(Edit, () => SelectedIndex >= 0);
            AddCommand = new RelayCommand(Add, () => true);
            DeleteCommand = new RelayCommand(Delete, () => SelectedIndex>=0);
            FilterCommand = new RelayCommand(Filter, () => true);
            UpdateViewCommand = new RelayCommand(UpdateView, () => Context.Count > 0);
            ClearFilterCommand = new RelayCommand(ClearFilter, () => true);

            UpdateView(null);
        }

        public EventHandler GridDoubleClick;
        private ObservableCollection<object> _context;


        public ObservableCollection<object> Context
        {
            get { return _context; }
            set
            {
                if (_context != value)
                {
                    _context = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public object SelectedEntity
        {
            get { return Context[SelectedIndex]; }
            
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

        public ICommand EditCommand { get;  }
        public ICommand AddCommand { get;  }
        public ICommand DeleteCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand UpdateViewCommand { get; }
        public ICommand ClearFilterCommand { get;  }
        


        private void UpdateView(object obj)
        {
            Context = new ObservableCollection<object>(_dataContext.GetAll());
        }

        private void Filter(object obj)
        {
            string filterText = obj as string;
            if (!string.IsNullOrWhiteSpace(filterText))
                Context = new ObservableCollection<object>(_dataContext.Get(
                    e => e.Name.ToLower().Contains(filterText.ToLower())));
            else
                UpdateView(null);
                
        }

        private void Delete(object obj)
        {
            _dataContext.Remove((EmployeeEntity)SelectedEntity);
            UpdateView(null);
        }

        private void Add(object obj)
        {
            IView view = WindowProvider.GetWindow("EditEmployee", new EmployeeEntity());
            view.ShowDialog();
            UpdateView(null);
        }

        private void Edit(object obj)
        {
            IView view = WindowProvider.GetWindow("EditEmployee", SelectedEntity);
            view.ShowDialog();
            UpdateView(null);
        }

        private void ClearFilter(object obj)
        {
            UpdateView(null);
        }
    }
}