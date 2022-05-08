using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StockTrack.command;
using StockTrack.view;

namespace StockTrack
{
    public class SettingsVm : Base
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private static readonly Lazy<SettingsVm> _lazy = new Lazy<SettingsVm>(() => new SettingsVm());

        public event EventHandler SaveChanged;
        private void OnSaveChanged(EventArgs e)
        {
            SaveChanged?.Invoke(this, e);
        }

        private SettingsVm()
        {
            InitClass();
        }

        public static SettingsVm Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        private string _urlCode;
        public string UrlCode
        {
            get { return _urlCode; }
            set
            {
                if(value != _urlCode)
                {
                    _urlCode = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _urlData;
        public string UrlData
        {
            get { return _urlData; }
            set
            {
                if (value != _urlData)
                {
                    _urlData = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _intervalRefresh;
        public int IntervalRefresh
        {
            get { return _intervalRefresh; }
            set
            {
                if (value != _intervalRefresh)
                {
                    _intervalRefresh = value;
                    OnPropertyChanged();
                }
            }
        }

  


        public void InitClass()
        {
            try
            {
                UrlCode = AppSettings.Read("STOCK_CODE");
                UrlData = AppSettings.Read("STOCK_DATA");
                IntervalRefresh = int.Parse(AppSettings.Read("PERIOD_TIME"));

            }
            catch (Exception ex)
            {
                log.Error("Parse Priod time failed.", ex);
                throw ex;
            }            
        }


        private ICommand _commandSave;
        public ICommand CommandSave
        {
            get
            {
                if(_commandSave == null)
                {
                     _commandSave = new RelayCommand((x) => CommandSaveHandle(x));
                }
                return _commandSave;
            }
        }

        private void CommandSaveHandle(object x)
        {
            if(x is null)
            {
                log.Error("Error not get object command save");
                return;
            }

            var settingDialog = x as DialogSettingsView;

            AppSettings.InsertOrUpdate("STOCK_CODE", UrlCode);
            AppSettings.InsertOrUpdate("STOCK_DATA", UrlData);
            AppSettings.InsertOrUpdate("PERIOD_TIME", IntervalRefresh.ToString());

            OnSaveChanged(EventArgs.Empty);

            settingDialog.Close();
        }


        private ICommand _commandDeleteDb;
        public ICommand CommandDeleteDb
        {
            get
            {
                if (_commandDeleteDb == null)
                {
                    _commandDeleteDb = new RelayCommand((x) => CommandDeleteDbHandle(x));
                }
                return _commandDeleteDb;
            }
        }

        private void CommandDeleteDbHandle(object x)
        {
            if (x is null)
            {
                log.Error("Error not get object command save");
                return;
            }

            var settingDialog = x as DialogSettingsView;
            var trackData = TrackData.Instance;

            var qa = MessageBox.Show("Do you want delete all symbols?", "Delete Symbols", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(qa == MessageBoxResult.Yes)
            {
                trackData.DeleteAll();
                settingDialog.Close();
            }
           
        }
    }
}
