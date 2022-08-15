using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using StockTrack.model;

namespace StockTrack
{
    public enum TrackDataResult
    {
        AddSuccess,
        AddFailed,
        EditSuccess,
        EditFailed,
        RemoveSuccess,
        RemoveFailed,
        RemovelAll
    }

    public class TrackDataArgs : EventArgs
    {
        public TrackDataResult Resutls { get; set; }
        public TrackModel Data { get; set; }
    }
    public class TrackData
    {
        private static string file_path_ = "trackdata.json";
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<TrackData> lazy_ = new Lazy<TrackData>(() => new TrackData());

        public event EventHandler<TrackDataArgs> EventTrackData;

        private List<TrackModel> _trackDatas { get; set; }

        private void OnEventTrackData(TrackDataArgs e)
        {
            EventTrackData?.Invoke(this, e);
        }
        private TrackData()
        {
            _trackDatas = new List<TrackModel>();
        }

        public static TrackData Instance
        {
            get
            {
                return lazy_.Value;
            }
        }

        public bool LoadAllTrack()
        {
            bool ret = true;
            try
            {
                bool fileExits = File.Exists(file_path_);
                if (!fileExits)
                {
                    using (File.Create(file_path_))
                    {
                        log.Info("Create file trackdata.json");
                    }
                }

                string json = File.ReadAllText(file_path_);
                _trackDatas = JsonConvert.DeserializeObject<List<TrackModel>>(json);
            }
            catch 
            {
                ret = false;
            }
            return ret;
        }


        internal List<TrackModel> GetData()
        {
            return _trackDatas;
        }


        internal void Save()
        {
            if (_trackDatas is null)
                return;

            log.Info("Save TrackData");

            string jsonOutput = JsonConvert.SerializeObject(_trackDatas, Formatting.Indented);
            File.WriteAllText(file_path_, jsonOutput);
        }

        internal void AddTrack(TrackModel trackModel)
        {
            if (_trackDatas is null)
                return;

            _trackDatas.Add(trackModel);
            OnEventTrackData(new TrackDataArgs()
            {
                Resutls = TrackDataResult.AddSuccess,
                Data = trackModel
            });
        }


        internal void RemoveAllTrack()
        {
            if (_trackDatas is null)
                return;

            _trackDatas.Clear();
        }

        internal void RemoveTrack(TrackModel trackModel)
        {
            if (_trackDatas is null)
                return;

            int index = _trackDatas.IndexOf(trackModel);

            if(index != -1)
            {
                _trackDatas.Remove(trackModel);
                OnEventTrackData(new TrackDataArgs()
                {
                    Resutls = TrackDataResult.RemoveSuccess,
                    Data = trackModel
                });
            }
            else
            {
                OnEventTrackData(new TrackDataArgs()
                {
                    Resutls = TrackDataResult.RemoveFailed,
                    Data = trackModel
                });
            }
        }

        internal void EditTrack(TrackModel trackModel)
        {
            if (_trackDatas is null)
                return;

            int index = _trackDatas.IndexOf(trackModel);

            if(index != -1)
            {
                _trackDatas[index].Notify  = trackModel.Notify;
                _trackDatas[index].Target1 = trackModel.Target1;
                _trackDatas[index].Target2 = trackModel.Target2;

                OnEventTrackData(new TrackDataArgs()
                {
                    Resutls = TrackDataResult.EditSuccess,
                    Data = trackModel
                });
            }
            else
            {
                OnEventTrackData(new TrackDataArgs()
                {
                    Resutls = TrackDataResult.EditFailed,
                    Data = trackModel
                });
            }
        }


        internal bool Exits(TrackModel model)
        {
            if (_trackDatas is null)
                return false;

            int index = _trackDatas.IndexOf(model);

            return (index == -1) ? false : true;
        }

        internal bool Exits(string symbol)
        {
            if (_trackDatas is null)
                return false;

            int index = _trackDatas.FindIndex(x => x.Symbol == symbol);

            return (index == -1) ? false : true;
        }


        public int TotalSymbols()
        {
            return _trackDatas is null ? 0 : _trackDatas.Count;
        }

    }
}
