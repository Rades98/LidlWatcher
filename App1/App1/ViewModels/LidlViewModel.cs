namespace App1.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Models;
    using Services.Interfaces;
    using Utils;
    using Xamarin.Forms;

    public class LidlViewModel : BaseViewModel
    {
        private readonly IGoodsService _goodsService;

        public string Url { get; set; }

        public ObservableCollection<GoodsModel> GoodsModels
        {
            get
            {
                var task = Task.Run(async () => await GetDataAsync());
                task.Wait();
                return new ObservableCollection<GoodsModel>(task.Result);
            }
        }

        public ICommand AddUrlCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GoodsModel SelectedItem { get; set; }

        public LidlViewModel()
        {
            _goodsService = DependencyService.Resolve<IGoodsService>();

            var task = DataWatchDog.CheckLidlPrices();
            task.Wait();

            AddUrlCommand = new Command(AddWatchDogAsync);
            DeleteCommand = new Command(Delete);
        }

        private async void AddWatchDogAsync()
        {
            try
            {
                var ldg = new LidlDataGet(Url);
                ldg.GetData();

                var newModel = new GoodsModel()
                {
                    ActualPrice = ldg.Price,
                    LastUpdate = DateTimeOffset.Now,
                    Name = ldg.Name,
                    Url = Url,
                    UsualPrice = ldg.Price,
                    ImageUrl = ldg.ImageUrl
                };

                try
                {
                    await _goodsService.AddRecordAsync(newModel);
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Chyba", e.Message, "OK");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Chyba", e.Message, "OK");
            }
            
            OnPropertyChanged(nameof(GoodsModels));

            Url = "";
            OnPropertyChanged(nameof(Url));
        }

        private async void Delete()
        {
            try
            {
                await _goodsService.DeleteRecordAsync(SelectedItem.GoodsId);

                OnPropertyChanged(nameof(GoodsModels));
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Chyba", e.Message, "OK");
            }
        }

        private async Task<ObservableCollection<GoodsModel>> GetDataAsync()
        {
            return new ObservableCollection<GoodsModel>(await _goodsService.GetAsync());
        }
    }
}
