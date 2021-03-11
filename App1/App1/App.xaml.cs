namespace App1
{
    using AutoMapper;
    using Database;
    using Database.Entities;
    using Models;
    using Services;
    using Services.Interfaces;
    using System;
    using System.Threading.Tasks;
    using Utils;
    using Views;
    using Xamarin.Forms;


    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CustomInitialized();

            MainPage = new LidlPage();
        }

        protected override void OnStart()
        {
            PriceWatcher();
        }

        protected override void OnSleep()
        {
            PriceWatcher();
        }

        protected override void OnResume()
        {
            PriceWatcher();
        }

        private static void CustomInitialized()
        {
            DependencyService.Register<IAppDbContext, AppDbContext>();
            DependencyService.RegisterSingleton(CreateAutoMapper());

            DependencyService.Register<IGoodsService, GoodsService>();
        }

        private static IMapper CreateAutoMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Goods, GoodsModel>();
                cfg.CreateMap<GoodsModel, Goods>();
            }).CreateMapper();
        }

        private static Task PriceWatcher()
        {
            Device.StartTimer(TimeSpan.FromMinutes(30), () =>
            {
                var task = DataWatchDog.CheckLidlPrices();
                task.Wait();

                return true;
            });

            return Task.CompletedTask; ;
        }
    }
}
