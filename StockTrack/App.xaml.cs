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

            var trackData = TrackData.Instance;
            bool loadTrack = trackData.LoadAllTrack();
            if (!loadTrack)
            {
                log.Error("Load track failed!");

                System.Environment.Exit(0);
            }

            MainWindow window = new MainWindow();
            var mainVm = MainWindowViewModel.Instance;
          

          

            window.Closed += (sender,ev) =>
            {
                trackData.Save();
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
