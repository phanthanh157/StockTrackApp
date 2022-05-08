using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockTrack.model
{
    public class StockPrices : INotifyPropertyChanged
    {
        #region Fields
        private string _sym;
        private string _symDescr;
        private decimal _lastPrice;
        private decimal _c;
        private decimal _f;
        private decimal _r;
        private decimal _lastVolume;
        private double _lot;
        private string _ot;
        private double _ot_percent;

        private string _avePrice;
        private string _highPrice;
        private string _lowPrice;

        private string _g1;
        private string _g2;
        private string _g3;
        private string _g4;
        private string _g5;
        private string _g6;

        private string _judgeColor = "White";

        #endregion //Fields

        /// <summary>
        /// Số định danh
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Mã CK
        /// </summary>
        public string sym
        {
            get { return _sym; }
            set
            {
                if (value != _sym)
                {
                    _sym = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Description symbol
        /// </summary>
        public string symDescr
        {
            get { return _symDescr; }
            set
            {
                if (value != _symDescr)
                {
                    _symDescr = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string mc { get; set; }

        /// <summary>
        /// Trần
        /// </summary>
        public decimal c
        {
            get { return _c; }
            set
            {
                if (value != _c)
                {
                    _c = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Sàn
        /// </summary>
        public decimal f
        {
            get { return _f; }
            set
            {
                if (value != _f)
                {
                    _f = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// TC
        /// </summary>
        public decimal r
        {
            get { return _r; }
            set
            {
                if (value != _r)
                {
                    _r = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá (Khớp lệnh)
        /// </summary>
        public decimal lastPrice
        {
            get { return _lastPrice; }
            set
            {
                if (_lastPrice != value)
                {
                    _lastPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// KL (Khớp lệnh)
        /// </summary>
        public decimal lastVolume
        {
            get { return _lastVolume; }
            set
            {
                if (_lastVolume != value)
                {
                    _lastVolume = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Tổng KL
        /// </summary>
        public double lot
        {
            get { return _lot; }
            set
            {
                if (_lot != value)
                {
                    _lot = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// -+ (Khớp lệnh)
        /// </summary>
        public string ot
        {
            get { return _ot; }
            set
            {
                if (_ot != value)
                {
                    _ot = value;
                    OnPropertyChanged();
                }
            }
        }

        public double ot_percent
        {
            get
            {
                return _ot_percent;
                //if (r != 0)
                //    //return Math.Round(((lastPrice - r) / r) * 100, 2);
                //else
                //    return 0;
            }

            set
            {
                if (_ot_percent != value)
                {
                    _ot_percent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string changePc { get; set; }

        /// <summary>
        /// (TB)
        /// </summary>
        public string avePrice
        {
            get { return _avePrice; }
            set
            {
                if (_avePrice != value)
                {
                    _avePrice = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Cao
        /// </summary>
        public string highPrice
        {
            get { return _highPrice; }
            set
            {
                if (_highPrice != value)
                {
                    _highPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Thấp
        /// </summary>
        public string lowPrice
        {
            get { return _lowPrice; }
            set
            {
                if (_lowPrice != value)
                {
                    _lowPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Mua (DTNN)
        /// </summary>
        public string fBVol { get; set; }
        public string fBValue { get; set; }

        /// <summary>
        /// Ban (DTNN)
        /// </summary>
        public string fSVolume { get; set; }
        public string fSValue { get; set; }
        public string fRoom { get; set; }

        /// <summary>
        /// Giá 1 | KL 1 | ?? (Bên mua)
        /// </summary>
        public string g1
        {
            get { return _g1; }
            set
            {
                if (_g1 != value)
                {
                    _g1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá 2 | KL 2 | ?? (Bên mua)
        /// </summary>
        public string g2
        {
            get { return _g2; }
            set
            {
                if (_g2 != value)
                {
                    _g2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá 3 | KL 3 | ?? (Bên mua)
        /// </summary>
        public string g3
        {
            get { return _g3; }
            set
            {
                if (_g3 != value)
                {
                    _g3 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá 1 | KL 1 | ?? (Bên bán)
        /// </summary>
        public string g4
        {
            get { return _g4; }
            set
            {
                if (_g4 != value)
                {
                    _g4 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá 2 | KL 2 | ?? (Bên bán)
        /// </summary>
        public string g5
        {
            get { return _g5; }
            set
            {
                if (_g5 != value)
                {
                    _g5 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá 3 | KL 3 | ?? (Bên bán)
        /// </summary>
        public string g6
        {
            get { return _g6; }
            set
            {
                if (_g6 != value)
                {
                    _g6 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Giá ?? | KL ?? | ?? (Bên ??)
        /// </summary>
        public string g7 { get; set; }

        /// <summary>
        /// Trả về %
        /// </summary>
        public string mp { get; set; }
        public string CWUnderlying { get; set; }
        public string CWIssuerName { get; set; }
        public string CWType { get; set; }
        public string CWMaturityDate { get; set; }
        public string CWLastTradingDate { get; set; }
        public string CWExcersisePrice { get; set; }
        public string CWExerciseRatio { get; set; }
        public string CWListedShare { get; set; }
        public string sType { get; set; }
        public string sBenefit { get; set; }
        public string JudgeColor
        {
            get { return _judgeColor; }
            set
            {
                if (_judgeColor != value)
                {
                    _judgeColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
