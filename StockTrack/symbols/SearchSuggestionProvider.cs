using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StockTrack.model;
using WpfControls.Editors;

namespace StockTrack.symbols
{
    public class SearchSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            var trackVm = TrackVm.Instance;

            List<StockCompany> companies = trackVm.StockInfos?.data;
            IEnumerable<StockCompany> result = companies;
            if (companies != null)
            {
                result = companies.Where(x => x.code.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1);
            }
            return result;
        }
    }
}
