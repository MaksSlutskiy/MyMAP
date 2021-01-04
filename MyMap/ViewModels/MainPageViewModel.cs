using MyMap.Controls;
using MyMap.CustomViews;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Maps;

namespace MyMap.ViewModels
{
    public class MainPageViewModel : ViewModelToolbarBase
    {
        private CustomMap _mapView;
        private MapSpan _mapSpan;
        private Position _position;
        private List<CustomPin> _pins;
        private bool _isCenterIconVisible;
        private bool _isSideBarIconVisible;

        private DelegateCommand _sideBarOpenCommand;

        public DelegateCommand SideBarOpenCommand =>
            _sideBarOpenCommand ?? (_sideBarOpenCommand = new DelegateCommand(async() =>
            {
                await NavigationService.NavigateAsync("SideBarPopupPage");
            }));
        public bool IsCenterIconVisible
        {
            get => _isCenterIconVisible;
            set => SetProperty(ref _isCenterIconVisible, value);
        }
        public bool IsSideBarIconVisible
        {
            get => _isSideBarIconVisible;
            set => SetProperty(ref _isSideBarIconVisible, value);
        }

        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
            : base(navigationService, eventAggregator, pageDialogService)
        {
            Title = "Main Page";

            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title= "item0", ImageSource="outline_info.png" },
                new ViewItem {Title= "item1", ImageSource="outline_info.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
            IsSideBarIconVisible = true;


            _position = new Position(50.449218, 30.525824);
            _mapSpan = new MapSpan(_position, 0.8, 0.8);
            _mapView = new CustomMap();
            _mapView.MoveToRegion(_mapSpan);
            _mapView.MapType = MapType.Street;
            _pins = new List<CustomPin>()
            {
                new CustomPin
                {
                    Label = "Santa Cruz",
                    Address = "The city with a boardwalk",
                    Type = PinType.Place,
                     Name = "Xamarin",
                    Url = "http://xamarin.com/about/",

                    Icon = "outline_info.png",
                    Position = new Position(50.449218, 30.525824)
                },
                 new CustomPin
                {
                    Label = "Santa Cruz",
                    Address = "The city with a boardwalk",
                    Type = PinType.Place,
                     Name = "Xamarin",
                     Icon = "outline_info.png",
        Url = "http://xamarin.com/about/",
                    Position = new Position(50.449450, 30.525450)
                }
            };
            foreach (var item in _pins)
            {
                _mapView.Pins.Add(item);
                _mapView.CustomPins.Add(item);
            }
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

            if(IsCenterIconVisible)
            {
                switch (index)
                {
                    case 0: SaveNewPin(); break;
                    case 1: CloseAddingPin(); break;
                        // case 2: PaymentPattern(); break;
                        // case 3: ChangeColomnsListView(); break;
                }
            }
            else
            {
                //switch (index)
                //{
                //    case 0: IsSearchBarVisible = !_isSearchBarVisible; break;
                //    case 1: NewConsigment(); break;
                //}
            }

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (parameters.ContainsKey("ActionMap"))
            {
                if (parameters["ActionMap"].ToString() == ActionMap.AddPin.ToString())
                {
                    IsCenterIconVisible = true;
                    IsSideBarIconVisible = false;
                    this.ToolbarItems = new List<ViewItem>
                    {   
                        new ViewItem {Title= "Save", ImageSource="outline_check.png" },
                        new ViewItem {Title= "Back", ImageSource="outline_clear.png" },
                    };
                }

            }
        }

        private void CloseAddingPin()
        {

            IsCenterIconVisible = false;
            IsSideBarIconVisible = true;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title= "item0", ImageSource="outline_info.png" },
                new ViewItem {Title= "item1", ImageSource="outline_info.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
        }
        private void SaveNewPin()
        {

            IsCenterIconVisible = false;
            IsSideBarIconVisible = true;
            this.ToolbarItems = new List<ViewItem>
            {
                new ViewItem {Title= "item0", ImageSource="outline_info.png" },
                new ViewItem {Title= "item1", ImageSource="outline_info.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };
        }
    }
}
