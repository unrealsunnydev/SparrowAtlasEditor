using System.Configuration;
using System.Data;
using System.Windows;

namespace SparrowEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Activated(object sender, EventArgs e)
        {

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Language.Load();
        }
    }

}
