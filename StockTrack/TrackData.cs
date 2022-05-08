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
        WriteSuccess,
        WriteFailed,
        DeleteSuccess,
        DeleteFailed,
        UpdateSuccess,
        UpdateFailed,
        DeleteAllSuccess,
        DeleteAllFailed
    }

    public class TrackDataArgs : EventArgs
    {
        public TrackDataResult Resutls { get; set; }
        public TrackModel Data { get; set; }
    }
    public class TrackData
    {
        private static string filePath = "trackdata.json";
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<TrackData> _lazy = new Lazy<TrackData>(() => new TrackData());

        public event EventHandler<TrackDataArgs> EventTrackData;

        private void OnEventTrackData(TrackDataArgs e)
        {
            EventTrackData?.Invoke(this, e);
        }
        private TrackData()
        {

        }

        public static TrackData Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        public void Write(TrackModel model)
        {
            try
            {
                var data = Read();

                if (data == null)
                {
                    data = new List<TrackModel>();
                }

                data.Add(model);

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
                OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.WriteSuccess, Data = model });
            }
            catch (Exception ex)
            {
                log.Error("error", ex);
                OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.WriteFailed, Data = null });
            }
        }

        public void Delete(TrackModel model)
        {
            try
            {
                if (CheckExits(model.Symbol))
                {
                    var data = Read();

                    int findIdx = data.FindIndex(it => it.Symbol == model.Symbol);
                    data.RemoveAt(findIdx);

                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.DeleteSuccess, Data = model });
                    return;

                }

            }
            catch (Exception ex)
            {
                log.Error("error", ex);

            }
            OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.DeleteFailed, Data = model });
        }

        public void Update(TrackModel model)
        {
            try
            {
                string json = File.ReadAllText(filePath);

                List<TrackJson> trackData = new List<TrackJson>();
                var data = JsonConvert.DeserializeObject<List<TrackJson>>(json);

                if (data is null)
                {
                    log.Warn("data is null");
                    return;
                }

                int findIdx = data.FindIndex(it => it.Symbol == model.Symbol);

                if (findIdx != -1)
                {
                    if (data[findIdx].Notify != model.Notify ||
                       data[findIdx].Target1 != model.Target1 ||
                       data[findIdx].Target2 != model.Target2)
                    {
                        data[findIdx].Notify = model.Notify;
                        data[findIdx].Target1 = model.Target1;
                        data[findIdx].Target2 = model.Target2;

                        string jsonOutput = JsonConvert.SerializeObject(data, Formatting.Indented);
                        File.WriteAllText(filePath, jsonOutput);
                        // OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.UpdateSuccess, Data = model });
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("error", ex);
                OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.UpdateFailed, Data = model });
            }

        }

        public List<TrackModel> Read()
        {
            List<TrackModel> data = new List<TrackModel>();
            try
            {
                bool fileExits = File.Exists(filePath);
                if (!fileExits)
                {
                    using (File.Create(filePath))
                    {
                        log.Info("Create file trackdata.json");
                    }
                }

                string json = File.ReadAllText(filePath);
                data = JsonConvert.DeserializeObject<List<TrackModel>>(json);
                return data;

            }
            catch (Exception ex)
            {
                log.Error("error", ex);
            }
            return data;
        }

        public void DeleteAll()
        {
            try
            {
                bool fileExits = File.Exists(filePath);
                if (!fileExits)
                {
                    using (File.Create(filePath))
                    {
                        log.Info("Create file trackdata.json");
                    }
                }

                string json = string.Empty;
                File.WriteAllText(filePath, json);
                OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.DeleteAllSuccess, Data = null });
            }
            catch (Exception ex)
            {
                log.Error("error", ex);
                OnEventTrackData(new TrackDataArgs() { Resutls = TrackDataResult.DeleteAllFailed, Data = null });
            }
        }


        public bool CheckExits(string symbol)
        {

            var data = Read();

            if (data == null)
            {
                data = new List<TrackModel>();
            }

            var filterTrack = data.Where((it) => it.Symbol == symbol).FirstOrDefault();

            if (filterTrack != null)
            {
                return true;
            }
            return false;
        }


        public int TotalSymbols()
        {
            var data = Read();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }

    }
}
