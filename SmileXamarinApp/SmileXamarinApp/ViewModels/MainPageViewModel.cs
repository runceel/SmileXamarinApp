using Microsoft.ProjectOxford.Face;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmileXamarinApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetProperty(ref this.imageSource, value); }
        }

        private IPageDialogService PageDialogService { get; }

        public DelegateCommand TakePhotoCommand { get; }

        public MainPageViewModel(IPageDialogService pageDialogService)
        {
            this.PageDialogService = pageDialogService;
            this.TakePhotoCommand = new DelegateCommand(async() => await this.TakePhotoAsync());
        }

        private async Task TakePhotoAsync()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                AllowCropping = false,
            });
            if (file == null) { return; }

            this.ImageSource = ImageSource.FromStream(() => file.GetStream());

            var client = new FaceServiceClient("Your API Key");
            var result = await client.DetectAsync(file.GetStream(), returnFaceAttributes: new[]
            {
                FaceAttributeType.Smile,
            });

            if (!result.Any()) { return; }

            await this.PageDialogService.DisplayAlertAsync("Smile point", $"Your smile point is {result.First().FaceAttributes.Smile * 100}", "OK");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}
