using System;
using System.Windows;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace StockTrack
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        protected override void OnStartup(StartupEventArgs e)
        {
            log.Info("--START STOCKTRACK APPLICATION--");
            base.OnStartup(e);
          
            MainWindow window = new MainWindow();
            var mainVm = MainWindowViewModel.Instance;

            window.Closed += (sender,ev) =>
            {
                mainVm.CloseTask();
                log.Info("--END STOCKTRACK APPLICATION--");
            };

            MsgBox.Instance.InitClass();

         
            mainVm.ExcuteTask();
            window.DataContext = mainVm;

          
            window.Show();
        }
    }
}
