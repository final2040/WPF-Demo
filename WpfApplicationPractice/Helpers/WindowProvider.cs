using System;
using System.Windows;
using WpfApplicationPractice.ViewModels;
using WpfApplicationPractice.Views;

namespace WpfApplicationPractice.Helpers
{
    internal class WindowProvider
    {
        public static IView GetWindow(string windowName, Object entity)
        {
            
            switch (windowName)
            {
                case "EditEmployee":
                    Window editView = new EditEmployeeView();
                    editView.DataContext = new EditEmployeeViewModel(entity);
                    return (IView)editView;
                case "EditDepartment":
                    Window editDepartmentView = new EditDepartmentView();
                    editDepartmentView.DataContext = new EditDepartmentViewModel(entity);
                    return (IView)editDepartmentView;
                case "EditJob":
                    Window editJobView = new EditJobView();
                    editJobView.DataContext = new EditJobViewModel(entity);
                    return (IView)editJobView;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}