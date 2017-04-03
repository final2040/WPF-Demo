using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApplicationPractice.Helpers;
using WpfApplicationPractice.Models;
using WpfApplicationPractice.Properties;

namespace WpfApplicationPractice.ViewModels
{
    public class DepartmentListViewModel :ViewModelBase, IListViewModel
    {
        private readonly DepartmentData _departmentDataContext = new DepartmentData();
        private ObservableCollection<object> _context;

        public DepartmentListViewModel()
        {
            ViewName = ApplicationText.Department_ViewName;
            EditCommand = new RelayCommand(Edit, () => SelectedIndex >= 0);
            AddCommand = new RelayCommand(Add, () => true);
            DeleteCommand = new RelayCommand(Delete, () => SelectedIndex >= 0);
            FilterCommand = new RelayCommand(Filter, () => true);
            UpdateViewCommand = new RelayCommand(UpdateView, () => Context.Count > 0);
            ClearFilterCommand = new RelayCommand(ClearFilter, () => true);

            UpdateView(null);
        }

        public ObservableCollection<object> Context
        {
            get { return _context; }
            set
            {
                if (_context !=value)
                {
                    _context = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ViewName { get; set; }
        public object SelectedEntity { get { return Context[SelectedIndex]; } }
        public int SelectedIndex { get; set; }
        public ICommand EditCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand UpdateViewCommand { get; }
        public ICommand ClearFilterCommand { get; }

        private void ClearFilter(object obj)
        {
            UpdateView(obj);
        }

        private void UpdateView(object obj)
        {
            Context = new ObservableCollection<object>(_departmentDataContext.GetAll());
        }

        private void Filter(object obj)
        {
            string textToFind = obj as string;
            if (!string.IsNullOrWhiteSpace(textToFind))
                Context= new ObservableCollection<object>(_departmentDataContext.Get(
                    d => d.Name.ToLower().Contains(textToFind.ToLower())));
            else
                UpdateView(null);
        }

        private void Delete(object obj)
        {
            _departmentDataContext.Remove(SelectedEntity as DepartmentEntity);
            UpdateView(null);
        }

        private void Add(object obj)
        {
            IView view = WindowProvider.GetWindow("EditDepartment", new DepartmentEntity());
            view.ShowDialog();
            UpdateView(null);
        }

        private void Edit(object obj)
        {
            IView view = WindowProvider.GetWindow("EditDepartment", SelectedEntity);
            view.ShowDialog();
            UpdateView(null);
        }

    }
}