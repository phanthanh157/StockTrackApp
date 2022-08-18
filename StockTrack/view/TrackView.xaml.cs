using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StockTrack.model;

namespace StockTrack.view
{
    /// <summary>
    /// Interaction logic for TrackView.xaml
    /// </summary>
    public partial class TrackView : UserControl
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        private TrackVm _trackVm = null;
        public TrackView()
        {
            InitializeComponent();
            _trackVm = TrackVm.Instance;
            this.DataContext = _trackVm;
        }

        private void txtSearchSymbol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string symbol = txtSearchSymbol.Editor.Text;

                var company = _trackVm.GetCompany(symbol);

                if (company is null)
                {
                    log.Error("cannot get comapany data");
                    return;
                };

                var trackDataObj = TrackData.Instance;

                common.Utils.Check(trackDataObj);

                if (trackDataObj.Exits(symbol))
                {
                    MsgBox.Instance.Show("Symbol: [" + symbol + "]  is exits!", TypeMsgBox.Warning);
                    return;
                }

                var trackModel = new TrackModel()
                {
                    Symbol = symbol,
                    Company = company.companyName,
                    CompanyFloor = company.floor,
                    ShortName = company.shortName,
                    Notify = true,
                    Ref = 0,
                    Ceil = 0,
                    Floor = 0,
                    LastPrice = 0,
                    LastVolume = 0,
                    OT = string.Empty,
                    Target1 = 0,
                    Target2 = 0,
                    JugdeColor = JugdeColor.Gray
                };

                trackDataObj.AddTrack(trackModel);

                txtSearchSymbol.Editor.Text = string.Empty;
            }
        }

        private void CommandRemoveTrack_Click(object sender, RoutedEventArgs e)
        {
            if (_trackVm == null) return;
            //Get the clicked MenuItem
            var menuItem = (MenuItem)sender;

            //Get the ContextMenu to which the menuItem belongs
            var contextMenu = (ContextMenu)menuItem.Parent;

            //Find the placementTarget
            var item = (DataGrid)contextMenu.PlacementTarget;

            //Get the underlying item, that you cast to your object that is bound
            //to the DataGrid (and has subject and state as property)
            var trackModel = (TrackModel)item.SelectedCells[0].Item;

            _trackVm.RemoveTrack(trackModel);

        }

        private void SettingsOpen_Click(object sender, RoutedEventArgs e)
        {
            DialogSettingsView settingsView = new DialogSettingsView();

            settingsView.ShowDialog();
        }
    }
}
