using System;
using System.IO;
using MyMap.CustomViews;
using MyMap.Interface;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyMap.ViewModels.DialogViewModel
{
    public class ShareMapDialogViewModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private string _name;
        private byte[] imageByte;
        private DelegateCommand _btnShareClick;
        private DelegateCommand _okCommand;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public ShareMapDialogViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
          : base(navigationService, eventAggregator, pageDialogService)
        {
        }
        public DelegateCommand OkCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand(async () =>
            {
                await NavigationService.GoBackAsync();
            }));
        public DelegateCommand BtnShareClick =>
         _btnShareClick ?? (_btnShareClick = new DelegateCommand(async () =>
         {
             if(Name == null)
             {
                 Name = "Screen from MyMap";
             }
             var path = Xamarin.Forms.DependencyService.Get<IFileService>().SavePicture(Name + ".jpg", new MemoryStream(imageByte), "imagesFolder");

             if (path != null)
             {
                     await Share.RequestAsync(new ShareFileRequest
                     {
                         File = new ShareFile(path),
                         Title = Name
                     });
                 
             }
         }));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            //if (parameters.ContainsKey("ImageByte"))
            //{
            //    imageByte = parameters["ImageByte"] as byte[];
            //    ImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageByte));
            //}
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("ImageByte"))
            {
                imageByte = parameters["ImageByte"] as byte[];
                ImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageByte));
            }

        }
    }
}
