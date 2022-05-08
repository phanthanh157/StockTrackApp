using StockTrack.model;
using System.Windows;
using System.Windows.Controls;

namespace StockTrack.symbols
{
    public class SearchTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SearchTempalte { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is StockCompany)
                return SearchTempalte;
            return base.SelectTemplate(item, container);
        }
    }
}
