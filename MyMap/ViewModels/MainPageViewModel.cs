using MyMap.Controls;
using MyMap.CustomViews;
using MyMap.Extensions;
using MyMap.Interface;
using MyMap.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyMap.ViewModels
{
    public class MainPageViewModel : ViewModelToolbarBase
    {
        private IService<MapObjectPin> mapObjectService;
        private IService<Category> categoryService;
        private CustomMap _mapView;
        private MapSpan _mapSpan;
        private Position _position;
        private List<CustomPin> _pins;
        private List<MapObjectPin> _mapObjectPin;
        private List<Category> _categorys;
        private bool _isCenterIconVisible;
        private bool _isSideBarIconVisible;
        private double _anchorX;
        private double _anchorY;
        private MapObjectPin createPin;
        private ImageSource _imageSource;
        private DataContainer dataContainer;

        //private DelegateCommand _sideBarOpenCommand;

        //public DelegateCommand SideBarOpenCommand =>
        //    _sideBarOpenCommand ?? (_sideBarOpenCommand = new DelegateCommand(async () =>
        //    {
        //        await NavigationService.NavigateAsync("SideBarPopupPage");
        //    }));

        public void AddPin()
        {
            IsCenterIconVisible = true;
            this.ToolbarItems = new ObservableCollection<ViewItem>
                    {
                        new ViewItem {Title= "Save", ImageSource="outline_check.png" },
                        new ViewItem {Title= "Back", ImageSource="outline_clear.png" },
                    };
        }


        public bool IsCenterIconVisible
        {
            get => _isCenterIconVisible;
            set => SetProperty(ref _isCenterIconVisible, value);
        }
        public double AnchorX
        {
            get => _anchorX;
            set => SetProperty(ref _anchorX, value);
        }
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public double AnchorY
        {
            get => _anchorY;
            set => SetProperty(ref _anchorY, value);
        }
        public bool IsSideBarIconVisible
        {
            get => _isSideBarIconVisible;
            set => SetProperty(ref _isSideBarIconVisible, value);
        }

        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
            : base(navigationService, eventAggregator, pageDialogService)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), (Func<bool>)(() =>
            {
                try
                {
                    Xamarin.Forms.DependencyService.Get<ILoadingPage>().ShowLoadingPage();
                    mapObjectService = App.DBModulManager.MapObjectService;
                    categoryService = App.DBModulManager.CategoryService;
                    GetDate();
                  
                    return false;
                }
                catch
                {
                    return true;
                }

            }));
            Title = "Main Page";
            this.ToolbarItems = new ObservableCollection<ViewItem>
            {
                new ViewItem {Title= "item0", ImageSource="outline_book.png" },
                new ViewItem {Title= "item0", ImageSource="outline_share.png" },
                new ViewItem {Title= "item0", ImageSource="outline_add.png" },
                new ViewItem {Title= "item1", ImageSource="outline_book.png" },

                
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
          
            IsSideBarIconVisible = true;
            dataContainer = new DataContainer();
            ConfigMap();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);


        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }


        public CustomMap MapView
        {
            get
            {
                return _mapView;
            }

        }

        protected override void ToolbarItemClicked(object parameter)
        {
            var index = ToolbarItems.IndexOf(parameter as ViewItem);

            if (IsCenterIconVisible)
            {
                switch (index)
                {
                    case 0:
                        SaveNewPin();


                        break;
                    case 1: CloseAddingPin(); break;
                        // case 2: PaymentPattern(); break;
                        // case 3: ChangeColomnsListView(); break;
                }
            }
            else
            {
                switch (index)
                {
                    case 0: SettingsPage(); break;
                    case 1: ShareMapView(); break;
                    case 2:
                        AddPin(); break;
                    case 3: GroupMapObject(); break;
                }
            }

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            //if (parameters.ContainsKey("ActionMap"))
            //{
            //    if (parameters["ActionMap"].ToString() == ActionMap.AddPin.ToString())
            //    {

            //        IsCenterIconVisible = true;
            //        IsSideBarIconVisible = false;
            //        this.ToolbarItems = new ObservableCollection<ViewItem>
            //        {
            //            new ViewItem {Title= "Save", ImageSource="outline_check.png" },
            //            new ViewItem {Title= "Back", ImageSource="outline_clear.png" },
            //        };
            //    }

            //}

            if (parameters.ContainsKey("CheckTheme"))
            {
                MapView.CheckTheme = !MapView.CheckTheme;
            }
            if (parameters.ContainsKey(nameof(MapObjectPin)))
            {
                var mapObjectPin = parameters[nameof(MapObjectPin)] as MapObjectPin;
                Update(mapObjectPin);
            }
            if (parameters.ContainsKey("MoveToPin"))
            {
                var mapObjectPin = parameters["MoveToPin"] as MapObjectPin;
                if (mapObjectPin != null)
                {
                    MapView.Pins.Clear();
                    //GetDate();
                    var _position = new Position(mapObjectPin.latitude, mapObjectPin.longitude);
                    var _mapSpan = new MapSpan(_position, 0.005, 0.005);
                    MapView.MoveToRegion(_mapSpan);
                }

            }
            if (parameters.ContainsKey("UpdatePins"))
            {
                MapView.Pins.Clear();
                GetDate();
            }
        }

        private void CloseAddingPin()
        {

            IsCenterIconVisible = false;
            IsSideBarIconVisible = true;
            this.ToolbarItems = new ObservableCollection<ViewItem>
            {
                  new ViewItem {Title= "item0", ImageSource="outline_book.png" },
                new ViewItem {Title= "item0", ImageSource="outline_share.png" },
                  new ViewItem {Title= "item0", ImageSource="outline_add.png" },
                new ViewItem {Title= "item1", ImageSource="outline_book.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
        }
        private async void SaveNewPin()
        {

            //Position = MapView.VisibleRegion.Center
            var _createPin = new MapObjectPin
            {
                Name = "Xamarin",
                Icon = "outline_pinMap.png",
                Description = "test",
                IsVisible = true,
                Category = 1,
                latitude = MapView.VisibleRegion.Center.Latitude,
                longitude = MapView.VisibleRegion.Center.Longitude
            };




            var pin2 = new CustomPin
            {
                Label = "Xamarin",
                Icon = "outline_pinMap.png",
                Description = "test",
                Color = _categorys.Where(x => x.Id == 1).FirstOrDefault().Color,
                Position = MapView.VisibleRegion.Center
            };
            MapView.CustomPins.Add(pin2);
            MapView.IsUpdate = !MapView.IsUpdate;

            MapView.Pins.Add(pin2);
            var res = mapObjectService.Create(_createPin);
            var getpin = await res;
            while (true)
            {
                if (getpin != null)
                {
                    break;
                }
            }
            IsCenterIconVisible = false;
            IsSideBarIconVisible = true;
            this.ToolbarItems = new ObservableCollection<ViewItem>
            {
                  new ViewItem {Title= "item0", ImageSource="outline_book.png" },
                new ViewItem {Title= "item0", ImageSource="outline_share.png" },
                  new ViewItem {Title= "item0", ImageSource="outline_add.png" },
                new ViewItem {Title= "item1", ImageSource="outline_book.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
            var nav_param = new NavigationParameters();
            nav_param.Add(nameof(MapObjectPin), getpin);
            nav_param.Add("New", true);
            await NavigationService.NavigateAsync("EditPinDialogPage", nav_param);
        }
        private async void GroupMapObject()
        {
          
            await NavigationService.NavigateAsync("GroupMapObjectPage");
        }
        private void ShareMapView()
        {
            this.MapView.IsMakeSnapshot = true;
            Xamarin.Forms.DependencyService.Get<ILoadingPage>().ShowLoadingPage();
            Device.StartTimer(TimeSpan.FromSeconds(0.5), (Func<bool>)(() =>
            {
                try
                {

                    var imageByte = Xamarin.Forms.DependencyService.Get<IViewToImageService>().GetImage(this.MapView);
                    Xamarin.Forms.DependencyService.Get<ILoadingPage>().HideLoadingPage();
                    this.MapView.IsMakeSnapshot = false;
                    var nav_param = new NavigationParameters();
                    nav_param.Add("ImageByte", imageByte);
                    //nav_param.Add(nameof(ImageSource), ImageSource);

                    NavigationService.NavigateAsync("ShareMapDialogPage", nav_param);
                    return false;
                }
                catch
                {
                    return true;
                }

            }));

        }


        private async void GetDate()
        {
            _categorys = (await categoryService.GetAll()).ToList();
            if (_categorys.Count == 0)
            {
                Category category = new Category { Name = "Default category", Color = "#000000" };
                var cat = await categoryService.Create(category);
                _categorys.Add(cat);
            }
            _mapObjectPin = (await mapObjectService.GetAll()).ToList();
            _pins = new List<CustomPin>();
            foreach (var item in _mapObjectPin)
            {
                var obj = _categorys.Where(x => x.Id == item.Category).FirstOrDefault();
                if (obj == null)
                {
                    item.Category = 1;
                }
            }
            foreach (var res in _mapObjectPin)
            {

                if (res.IsVisible)
                {
                    var color = _categorys.Where(x => x.Id == res.Category).FirstOrDefault().Color;
                    _pins.Add(new CustomPin { Icon = res.Icon, Description = res.Description, Position = new Position(res.latitude, res.longitude), Label = res.Name, Color = _categorys.Where(x => x.Id == res.Category).FirstOrDefault().Color });

                }
            }

            MapView.Pins.Clear();
            MapView.CustomPins.Clear();
            foreach (var item in _pins)
            {
                _mapView.CustomPins.Add(item);
            }
            MapView.IsUpdate = !MapView.IsUpdate;
            foreach (var item in _pins)
            {
                MapView.Pins.Add(item);
            }
        }

        //private void UpdateList(List<MapObjectPin> mapObjectPins)
        //{
        //    MapView.Pins.Clear();
        //    _mapObjectPin = mapObjectPins;
        //    _pins = new List<CustomPin>();

        //    foreach (var res in _mapObjectPin)
        //    {
        //        if (res.IsVisible)
        //            _pins.Add(new CustomPin { Icon = res.Icon, Description = res.Description, Position = new Position(res.latitude, res.longitude), Label = res.Name });
        //    }

        //    foreach (var item in _pins)
        //    {
        //        _mapView.Pins.Add(item);
        //        _mapView.CustomPins.Add(item);
        //    }
        //}

        private void Update(MapObjectPin mapObjectPin)
        {
            var res = mapObjectService.Update(mapObjectPin);
            var item = MapView.CustomPins.Where(x => x.Position.Latitude == mapObjectPin.latitude && x.Position.Longitude == mapObjectPin.longitude).FirstOrDefault();
            item.Name = mapObjectPin.Name;
            item.Description = mapObjectPin.Description;
            item.Label = mapObjectPin.Name;
            item.Color = _categorys.Where(x => x.Id == mapObjectPin.Category).FirstOrDefault().Color;

            MapView.Pins.Remove(MapView.Pins.LastOrDefault());
            MapView.Pins.Add(new CustomPin { Icon = mapObjectPin.Icon, Description = mapObjectPin.Description, Position = new Position(mapObjectPin.latitude, mapObjectPin.longitude), Label = mapObjectPin.Name, Color = _categorys.Where(x => x.Id == mapObjectPin.Category).FirstOrDefault().Color });
            //GetDate();
        }

        private void ConfigMap()
        {
            double latitude = 50.449218;
            double longitude = 30.525824;
            double degreesLat = 0.8;
            double degreesLon = 0.8;
            try
            {
                 latitude = Convert.ToDouble(dataContainer.GetValue(App.Info.LatitudeKey).ToString());
                 longitude = Convert.ToDouble(dataContainer.GetValue(App.Info.LongitudeKey).ToString());
                 degreesLat = Convert.ToDouble(dataContainer.GetValue(App.Info.DegreesLatKey).ToString());
                 degreesLon = Convert.ToDouble(dataContainer.GetValue(App.Info.DegreesLonKey).ToString());
            }
            catch
            {

            }

            _position = new Position(latitude, longitude);
            _mapSpan = new MapSpan(_position, degreesLat, degreesLon);
            _mapView = new CustomMap();
            _mapView.MoveToRegion(_mapSpan);
            _mapView.MapType = MapType.Street;

            if (App.Info.Device == DevicePlatform.iOS)
                AnchorY = 18;
            else
                AnchorY = -30;
            _mapView.PropertyChanged += Map_PropertyChanged;
        }
        private async void SettingsPage()
        {
            await NavigationService.NavigateAsync("SettingPage");
        }
        void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "VisibleRegion")
            {
                dataContainer.RemoveValue(App.Info.LatitudeKey);
                dataContainer.AddValue(App.Info.LatitudeKey, _mapView.VisibleRegion.Center.Latitude.ToString());

                dataContainer.RemoveValue(App.Info.LongitudeKey);
                dataContainer.AddValue(App.Info.LongitudeKey, _mapView.VisibleRegion.Center.Longitude.ToString());

                dataContainer.RemoveValue(App.Info.DegreesLatKey);
                dataContainer.AddValue(App.Info.DegreesLatKey, _mapView.VisibleRegion.LatitudeDegrees.ToString());

                dataContainer.RemoveValue(App.Info.DegreesLonKey);
                dataContainer.AddValue(App.Info.DegreesLonKey, _mapView.VisibleRegion.LongitudeDegrees.ToString());
            }
            if(e.PropertyName =="IsMakeSnapshot")
            Xamarin.Forms.DependencyService.Get<ILoadingPage>().HideLoadingPage();
        }
      
    }
}
