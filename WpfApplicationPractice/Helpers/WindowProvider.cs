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
                    editView.DataContext = new EditEmployeeViewModelBase(entity);
                    return (IView)editView;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}