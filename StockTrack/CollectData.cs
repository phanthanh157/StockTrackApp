using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockTrack.model;

namespace StockTrack
{
    public class CollectData
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        public static async Task<StockInfo> GetStockInfo()
        {
            StockInfo stockInfo = null;
            try
            {
                var api = AppSettings.Read("STOCK_CODE");

                if (!string.IsNullOrEmpty(api))
                {
                    using (var response = new HttpClient())
                    {
                        var content = await response.GetAsync(api,HttpCompletionOption.ResponseContentRead);
                        //content.EnsureSuccessStatusCode();

                        if (content.IsSuccessStatusCode)
                        {
                            var resStr = await content.Content.ReadAsStringAsync();

                            stockInfo = JsonConvert.DeserializeObject<StockInfo>(resStr);
                        }
                    }
                }
                else
                {
                    log.Warn("api stock code is empty");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
            }

            return stockInfo;
        }


        public static async Task<List<StockPrices>> GetStockData(string[] symbols)
        {
            List<StockPrices> stockPrices = null;
            try
            {
                var api = AppSettings.Read("STOCK_DATA");

                if (!string.IsNullOrEmpty(api))
                {

                    string symbolStr = String.Join(",", symbols);

                    string url = api + symbolStr;

                    using (var response = new HttpClient())
                    {
                        var content = await response.GetAsync(url);
                        //content.EnsureSuccessStatusCode();

                        if (content.IsSuccessStatusCode)
                        {
                            var resStr = await content.Content.ReadAsStringAsync();

                            stockPrices = JsonConvert.DeserializeObject<List<StockPrices>>(resStr);
                        }
                    }
                }
                else
                {
                    log.Warn("api stock data is empty");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
            }

            return stockPrices;
        }

    }
}
