using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyMap.Extensions;
using MyMap.Interface;
using MyMap.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyMap.ViewModels.DialogViewModel
{
    public class EditPinDialogViewModel : ViewModelBase
    {
        private bool _isCenterIconVisible;
        private IService<MapObjectPin> mapObjectService;
        private IService<Category> categoryService;
        private ObservableCollection<Category> _sourceItemsCategory;
        private string _name;
        private string _description;
        private string _address;
        private string _saveText;
        private Category _category;
        private MapObjectPin mapObjectPin;
        private List<string> _categories;
        private bool _isEdit;
        private Color _tintColor;

        public EditPinDialogViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
           : base(navigationService, eventAggregator, pageDialogService)
        {
            mapObjectService = App.DBModulManager.MapObjectService;
            categoryService = App.DBModulManager.CategoryService;
            GetDate();
        }
        private DelegateCommand _okCommand;
        private DelegateCommand _editCommand;
        private DelegateCommand<Category> _selectedCategoryCommand;
        public ObservableCollection<Category> SourceItemsCategory
        {
            get
            {
                if (_sourceItemsCategory == null)
                {
                    _sourceItemsCategory = new ObservableCollection<Category>();
                }
                return _sourceItemsCategory;
            }
            set => SetProperty(ref _sourceItemsCategory, value);
        }
        public DelegateCommand OkCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand(async () =>
            {
                Save();
                var nav_param = new NavigationParameters();
                nav_param.Add(nameof(MapObjectPin), mapObjectPin);
                await NavigationService.GoBackAsync(nav_param);
            }));

        public DelegateCommand EditCommand =>
           _editCommand ?? (_editCommand = new DelegateCommand(() =>
           {
               if (IsEdit)
               {
                   SaveText = "Back";
                   IsEdit = false;
                   TintColor = Color.LightGray;
               }
               else
               {
                   SaveText = "Save";
                   IsEdit = true;
                   TintColor = Color.Black;
               }

           }));
        public DelegateCommand<Category> SelectedCategoryCommand =>
           _selectedCategoryCommand ?? (_selectedCategoryCommand = new DelegateCommand<Category>((param) =>
          {
              this.Category = param;
          }));

        public Color TintColor
        {
            get => _tintColor;
            set => SetProperty(ref _tintColor, value);
        }

        public bool IsCenterIconVisible
        {
            get => _isCenterIconVisible;
            set => SetProperty(ref _isCenterIconVisible, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public string SaveText
        {
            get => _saveText;
            set => SetProperty(ref _saveText, value);
        }
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        public Category Category
        {
            get => _category;
            set
            {
                SetProperty(ref _category, value);
            }
        }
        private async void GetDate()
        {
            SourceItemsCategory = (await categoryService.GetAll()).ToObservableCollection();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(MapObjectPin)))
            {
                mapObjectPin = parameters[nameof(MapObjectPin)] as MapObjectPin;

                //var _mapObjectsPin = (await mapObjectService.GetAll()).ToList();
                //mapObjectPin = _mapObjectsPin.Where(x => x.latitude == _mapObjectPin.latitude && x.longitude == _mapObjectPin.longitude).FirstOrDefault();
                //while (true)
                //{
                //    mapObjectPin = _mapObjectsPin.Where(x => x.latitude == _mapObjectPin.latitude && x.longitude == _mapObjectPin.longitude).FirstOrDefault();
                //    if (mapObjectPin != null)
                //        break;
                //    else
                //    {
                //        _mapObjectsPin = (await mapObjectService.GetAll()).ToList();
                //    }

                //}

                //int id = item.Id;
                //createPin = await mapObjectService.Get(id);

                this.Name = mapObjectPin.Name;
                this.Description = mapObjectPin.Description;
                this.Category = SourceItemsCategory.Where(x => x.Id == mapObjectPin.Category).FirstOrDefault(); 
                GetAddress();
            }
            if (parameters.ContainsKey("New"))
            {
                SaveText = "Save";
                TintColor = Color.Black;
                IsEdit = true;
            }
            else
            {
                SaveText = "Back";
                TintColor = Color.LightGray;
                IsEdit = false;
            }
        }

        private async void GetAddress()
        {
            try
            {
                Geocoder geoCoder = new Geocoder();
                Position position = new Position(mapObjectPin.latitude, mapObjectPin.longitude);
                var addrs = (await Geocoding.GetPlacemarksAsync(new Location(mapObjectPin.latitude, mapObjectPin.longitude))).FirstOrDefault();
                this.Address = $"{addrs.Thoroughfare} {addrs.SubThoroughfare} {addrs.PostalCode} {addrs.Locality} {addrs.CountryName}";

            }
            catch
            {

            }
        }

        private void Save()
        {
            mapObjectPin.Name = this.Name;
            mapObjectPin.Description = this.Description;
            mapObjectPin.Category = Category.Id;
            var res = mapObjectService.Update(mapObjectPin);
        }
        //private async List<MapObjectPin> GetList()
        //{
        //    return (await mapObjectService.GetAll()).ToList();
        //}

    }
}
