using System;

namespace StockTrack
{
    public class MainWindowViewModel : Base
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<MainWindowViewModel> _lazy = new Lazy<MainWindowViewModel>(() => new MainWindowViewModel());

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

        private StatusConnected _statusConnect;
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

        private int _totalSymbols;
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
                case TrackDataResult.WriteSuccess:
                    TotalSymbols += 1;
                    break;
                case TrackDataResult.DeleteSuccess:
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
