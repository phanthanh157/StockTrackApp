using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockTrack.model
{
    public class StockCompany : INotifyPropertyChanged
    {
        private string _code;
        private string _companyName;
        private string _floor;
        private string _shortName;
        private string _companyNameEng;

        public string code
        {
            get { return _code; }
            set
            {
                if (value != _code)
                {
                    _code = value;
                    OnPropertyChanged();
                }
            }
        }
        public string companyName
        {
            get { return _companyName; }
            set
            {
                if (value != _companyName)
                {
                    _companyName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string floor
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
        public string shortName
        {
            get { return _shortName; }
            set
            {
                if (value != _shortName)
                {
                    _shortName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string companyNameEng
        {
            get { return _companyNameEng; }
            set
            {
                if (value != _companyNameEng)
                {
                    _companyNameEng = value;
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
    public class StockInfo : INotifyPropertyChanged
    {
        // private List<StockCompany> _data;
        private int _currentPage;
        private int _size;
        private int _totalElements;
        private int _totalPages;

        public List<StockCompany> data
        {
            get; set;
        }
        public int currentPage
        {
            get { return _currentPage; }
            set
            {
                if (value != _currentPage)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                }
            }
        }
        public int size
        {
            get { return _size; }
            set
            {
                if (value != _size)
                {
                    _size = value;
                    OnPropertyChanged();
                }
            }
        }
        public int totalElements
        {
            get { return _totalElements; }
            set
            {
                if (value != _totalElements)
                {
                    _totalElements = value;
                    OnPropertyChanged();
                }
            }
        }
        public int totalPages
        {
            get { return _totalPages; }
            set
            {
                if (value != _totalPages)
                {
                    _totalPages = value;
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
