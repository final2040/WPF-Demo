using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApplicationPractice.Models;

namespace WpfApplicationPractice.ViewModels
{
    public interface IListViewModel 
    {
        ObservableCollection<object> Context { get; set; }
        string Name { get; }
        object SelectedEntity { get; }
        int SelectedIndex { get; set; }
        ICommand EditCommand { get; }
        ICommand AddCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand FilterCommand { get; }
        ICommand UpdateViewCommand { get; }
        ICommand ClearFilterCommand { get;  }
       
    }
}