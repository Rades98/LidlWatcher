namespace App1.Utils
{
    using System;
    using System.Threading.Tasks;
    using Plugin.LocalNotifications;
    using Services.Interfaces;
    using Xamarin.Forms;

    public static class DataWatchDog
    {
        private static IGoodsService _goodsService;

        private static bool _isInitialized = false;

        private static void Init()
        {
            _goodsService = DependencyService.Get<IGoodsService>();
            _isInitialized = true;
        }

        public static async Task<bool> CheckLidlPrices()
        {
            if (!_isInitialized)
            {
                Init();
            }

            var changed = false;
            var data = await _goodsService.GetAsync();

            foreach (var goodsModel in data)
            {
                var ldg = new LidlDataGet(goodsModel.Url);
                ldg.GetData();

                if (ldg.Price < goodsModel.ActualPrice)
                {
                    CrossLocalNotifications.Current.Show("Zlevnilo zboží", $"{ldg.Name} z {goodsModel.ActualPrice} na {ldg.Price}");
                }

                goodsModel.UsualPrice = goodsModel.ActualPrice;
                goodsModel.ActualPrice = ldg.Price;
                goodsModel.LastUpdate = DateTimeOffset.Now;
                changed = true;

                await _goodsService.UpdateAsync(goodsModel);
            }

            return changed;
        }
    }
}
