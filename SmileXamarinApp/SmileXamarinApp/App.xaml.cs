using Prism.Unity;
using SmileXamarinApp.Views;
using Xamarin.Forms;

namespace SmileXamarinApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterTypeForNavigation<NavigationPage>();
            this.Container.RegisterTypeForNavigation<MainPage>();

            Plugin.Media.CrossMedia.Current.Initialize();
        }
    }
}
