using System;
using System.Windows.Input;
using StockTrack.command;

namespace StockTrack
{
    public class MainWindowViewModel : Base
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<MainWindowViewModel> _lazy = new Lazy<MainWindowViewModel>(() => new MainWindowViewModel());
        private int _totalSymbols;
        private StatusConnected _statusConnect;
        private MainWindowViewModel()
        {
            InitClass();

        }

        public static MainWindowViewModel Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        public StatusConnected StatusConnect
        {
            get { return _statusConnect; }
            set
            {
                if (value != _statusConnect)
                {
                    _statusConnect = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TotalSymbols
        {
            get { return _totalSymbols; }
            set
            {
                if (value != _totalSymbols)
                {
                    _totalSymbols = value;
                    OnPropertyChanged();
                }
            }
        }


        private void InitClass()
        {
            StatusConnect = StatusConnected.Disconnected;

            TrackData trackData = TrackData.Instance;
            trackData.EventTrackData += TrackData_EventTrackData;
            TotalSymbols = trackData.TotalSymbols();

        }

        private void TrackData_EventTrackData(object sender, TrackDataArgs e)
        {
            if(e is null)
            {
                log.Error("track data is null");
            }

            switch (e.Resutls)
            {
                case TrackDataResult.AddSuccess:
                    TotalSymbols += 1;
                    break;
                case TrackDataResult.RemoveSuccess:
                    TotalSymbols -= 1;
                    break;
                default:
                    break;
            }

        }

        public void ExcuteTask()
        {
            TrackVm track = TrackVm.Instance;


            track.RunTrack();

        }

        public void CloseTask()
        {
            TrackVm track = TrackVm.Instance;
            track.CancelTrack();
        }



    }
}
