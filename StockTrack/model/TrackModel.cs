using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockTrack.model
{
    public enum JugdeColor
    {
        Red,
        Yellow,
        Green,
        Cyan,
        Pink,
        Gray
    }
    public class TrackJson
    {
        public string Symbol { get; set; }
        public string Company { get; set; }
        public string ShortName { get; set; }
        public string CompanyFloor { get; set; }
        public decimal Ref { get; set; }
        public decimal Ceil { get; set; }
        public decimal Floor { get; set; }
        public decimal LastPrice { get; set; }
        public decimal LastVolume { get; set; }
        public string OT { get; set; }
        public bool Notify { get; set; }
        public decimal Target1 { get; set; }
        public decimal Target2 { get; set; }
        public JugdeColor JugdeColor { get; set; }
    }

    public class TrackModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (value != _symbol)
                {
                    _symbol = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _ref;
        public decimal Ref
        {
            get { return _ref; }
            set
            {
                if (value != _ref)
                {
                    _ref = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _ceil;
        public decimal Ceil
        {
            get { return _ceil; }
            set
            {
                if (value != _ceil)
                {
                    _ceil = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _floor;
        public decimal Floor
        {
            get { return _floor; }
            set
            {
                if (value != _floor)
                {
                    _floor = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _lastPrice;
        public decimal LastPrice
        {
            get { return _lastPrice; }
            set
            {
                if (value != _lastPrice)
                {
                    _lastPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _lastVolume;
        public decimal LastVolume
        {
            get { return _lastVolume; }
            set
            {
                if (value != _lastVolume)
                {
                    _lastVolume = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _ot;
        public string OT
        {
            get { return _ot; }
            set
            {
                if (value != _ot)
                {
                    _ot = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CompanyFloor { get; set; }
        public string Company { get; set; }
        public string ShortName { get; set; }

        private bool _notify;
        public bool Notify
        {
            get { return _notify; }
            set
            {
                if (value != _notify)
                {
                    _notify = value;
                    OnPropertyChanged();
                    OnUpdateTrack();
                }
            }
        }


        private decimal target_one;
        //Buy
        public decimal Target1
        {
            get { return target_one; }
            set
            {
                if (value != target_one)
                {
                    target_one = value;
                    OnPropertyChanged();
                    OnUpdateTrack();
                }
            }
        }

        private decimal target_two;
        //Sale
        public decimal Target2
        {
            get { return target_two; }
            set
            {
                if (value != target_two)
                {
                    target_two = value;
                    OnPropertyChanged();
                    OnUpdateTrack();
                }
            }
        }

        private JugdeColor _jugdeColor;
        public JugdeColor JugdeColor
        {
            get { return _jugdeColor; }
            set
            {
                if (value != _jugdeColor)
                {
                    _jugdeColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            //log.DebugFormat("name = {0}", name);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        private void OnUpdateTrack()
        {
            // log.DebugFormat("symbol = {2}, target1 = {0}; target2 = {1}", this.Target1,this.Target2,this.Symbol);
            TrackData trackData = TrackData.Instance;
            trackData.Update(this);
        }

    }
}
