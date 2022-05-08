using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using StockTrack.model;


namespace StockTrack
{
    public class TrackVm
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<TrackVm> _lazy = new Lazy<TrackVm>(() => new TrackVm());
        private readonly object _trackLock = new object();
        private ObservableCollection<TrackModel> _trackModels = null;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken _token = CancellationToken.None;
        public string[] Symbols { get; private set; }

        public ObservableCollection<TrackModel> TrackModels
        {
            get { return _trackModels; }
            set
            {
                _trackModels = value;
                BindingOperations.EnableCollectionSynchronization(_trackModels, _trackLock);
            }
        }

        public StockInfo StockInfos { get; set; }
        public static TrackVm Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        private TrackVm()
        {
            InitClass();
        }


        private void InitClass()
        {
            _token = _tokenSource.Token;
            TrackModels = new ObservableCollection<TrackModel>();
            var trackDataObj = TrackData.Instance;
            trackDataObj.EventTrackData += TrackDataObj_EventTrackData;

            var settingVm = SettingsVm.Instance;
            settingVm.SaveChanged += SettingVm_SaveChanged; ;

            LoadData();
        }

        private void SettingVm_SaveChanged(object sender, EventArgs e)
        {
            CancelTrack();
            RunTrack();
        }

        private void LoadData()
        {
            TrackData trackData = TrackData.Instance;

            var data = trackData.Read();
            if (data != null)
            {
                data.ForEach(x =>
                {
                    TrackModels.Add(x);
                });

                Symbols = data.Select(it => it.Symbol).ToArray();
            }

        }

        private void TrackDataObj_EventTrackData(object sender, TrackDataArgs e)
        {

            switch (e.Resutls)
            {
                case TrackDataResult.WriteSuccess:
                    MsgBox.Instance.Show("Add symbol [" + e.Data.Symbol + "] successfull!", TypeMsgBox.Success);
                    TrackModels.Add(e.Data);
                    Symbols = TrackModels.Select(it => it.Symbol).ToArray();
                    break;
                case TrackDataResult.WriteFailed:
                    MsgBox.Instance.Show("Add symbol [" + e.Data.Symbol + "] failed!", TypeMsgBox.Error);
                    break;
                case TrackDataResult.DeleteSuccess:
                    MsgBox.Instance.Show("Remove symbol [" + e.Data.Symbol + "] successfull!", TypeMsgBox.Success);
                    TrackModels.Remove(e.Data);
                    Symbols = TrackModels.Select(it => it.Symbol).ToArray();
                    break;
                case TrackDataResult.DeleteFailed:
                    MsgBox.Instance.Show("Remove symbol [" + e.Data.Symbol + "]  failed!", TypeMsgBox.Error);
                    break;
                case TrackDataResult.UpdateSuccess:
                    MsgBox.Instance.Show("Update symbol [" + e.Data.Symbol + "] successfull!", TypeMsgBox.Success);
                    break;
                case TrackDataResult.UpdateFailed:
                    MsgBox.Instance.Show("Update symbol [" + e.Data.Symbol + "] failed!", TypeMsgBox.Error);
                    break;
                case TrackDataResult.DeleteAllSuccess:
                    MsgBox.Instance.Show("Clear all symbols", TypeMsgBox.Information);
                    TrackModels.Clear();
                    break;
                default:
                    break;
            }

        }

        internal void RunTrack()
        {
            //New thread background
            try
            {
                var time = int.Parse(AppSettings.Read("PERIOD_TIME"));
                TimeSpan interval = new TimeSpan(0, 0, time);

                Task.Run(async () =>
                {
                    await PeriodTrackAsync(interval, _token);
                });
            }
            catch (OperationCanceledException ex)
            {
                log.Warn("Task cancel: ", ex);
            }
        }

        internal void CancelTrack()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
        }

        private async Task PeriodTrackAsync(TimeSpan interval, CancellationToken cancellationToken)
        {
            //get stock info
            StockInfos = await CollectData.GetStockInfo();

            while (true)
            {
                //log.Info("task running ....");

                await Monitoring();
                Notify();
                await Task.Delay(interval, cancellationToken); //0.5s
            }
        }

        private void Notify()
        {

            foreach (var track in TrackModels)
            {
                decimal lastPrice = track.LastPrice;

                if (!track.Notify) return;

                if (MainWindowViewModel.Instance.StatusConnect == StatusConnected.Disconnected) return;

                if (track.Target1 >= lastPrice &&
                   track.Target1 > 0 )
                {
                    Notification.Instance.ShowSucess("Buy : " + track.Symbol +
                                                     "__" + track.LastPrice + "/" + track.Target1 +
                                                     "__" + track.LastVolume +
                                                     "__" + track.OT,
                                                "" + track.Company, true);
                }
               
                if (track.Target2 <= lastPrice &&
                         track.Target2 > 0)
                {
                    Notification.Instance.ShowError("Sale : " + track.Symbol +
                                                     "__" + track.LastPrice + "/" + track.Target2 +
                                                     "__" + track.LastVolume +
                                                     "__" + track.OT,
                                                "" + track.Company, true);
                }
            }

        }

        private async Task Monitoring()
        {
            var newPrice = await CollectData.GetStockData(Symbols);

            if (newPrice is null)
            {
                log.Error("get data prices is null");
                MainWindowViewModel.Instance.StatusConnect = StatusConnected.Disconnected;
                return;
            }
            MainWindowViewModel.Instance.StatusConnect = StatusConnected.Connected;

            lock (_trackLock)
            {
                for (int idx = 0; idx < TrackModels.Count; idx++)
                {
                    var stockPrice = newPrice.Where(x => x.sym == TrackModels[idx].Symbol).FirstOrDefault();

                    if (stockPrice == null)
                    {
                        log.Warn("Stock Price Not Found");
                        continue;
                    }
                    TrackModels[idx].Ref = stockPrice.r;
                    TrackModels[idx].Ceil = stockPrice.c;
                    TrackModels[idx].Floor = stockPrice.f;

                    TrackModels[idx].LastPrice = stockPrice.lastPrice;
                    TrackModels[idx].LastVolume = stockPrice.lastVolume;
                    TrackModels[idx].OT = stockPrice.ot;

                    //Jugde Color
                    if(stockPrice.lastPrice == stockPrice.c)
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Cyan;
                    }
                    else if(stockPrice.lastPrice < stockPrice.c &&
                             stockPrice.lastPrice > stockPrice.r)
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Green;
                    }
                    else if(stockPrice.lastPrice == stockPrice.r)
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Yellow;
                    }
                    else if (stockPrice.lastPrice < stockPrice.r &&
                            stockPrice.lastPrice > stockPrice.f)
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Red;
                    }
                    else if(stockPrice.lastPrice == stockPrice.f)
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Pink;
                    }
                    else
                    {
                        TrackModels[idx].JugdeColor = JugdeColor.Gray;
                    }
                }
            }
        }


        public StockCompany GetCompany(string symbols)
        {
            if (StockInfos == null) return null;

            var companies = StockInfos.data;

            var filter = companies.Where((it) => it.code == symbols).FirstOrDefault();

            return filter;
        }


        public void RemoveTrack(TrackModel model)
        {
            var trackData = TrackData.Instance;
            trackData.Delete(model);
        }

    }
}
