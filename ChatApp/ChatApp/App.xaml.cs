using System.Windows;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Run()
        {
            Bootstrap bs = new Bootstrap();
            bs.Run();
        }
       
    }
}
