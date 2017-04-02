using System.Security.Cryptography.X509Certificates;

namespace WpfApplicationPractice.Helpers
{
    public interface IView
    {
        void Close();
        bool? ShowDialog();
    }
}