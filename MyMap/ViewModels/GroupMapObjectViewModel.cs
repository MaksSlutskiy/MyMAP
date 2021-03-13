using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using MyMap.Controls;
using MyMap.CustomViews;
using MyMap.Extensions;
using MyMap.Helps;
using MyMap.Interface;
using MyMap.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace MyMap.ViewModels
{
    public class GroupMapObjectViewModel : ViewModelToolbarBase, IInitialize
    {
        private IService<MapObjectPin> mapObjectService;
        private IService<Category> categoryService;
        private ObservableCollection<Category> _sourceItemsCategory;
        private ObservableCollection<ObjectsClassGroup<MapObjectPin>> _sourceItems;
        private ObservableCollection<ObjectsClassGroup<MapObjectPin>> _allsourceItems;
        private List<MapObjectPin> _mapObjectPin;
        private List<MapObjectPin> _mapObjectPinOld;
        private DelegateCommand<MapObjectPin> _itemTappedCommand;
        private DelegateCommand<MapObjectPin> _removeItemCommand;
        private DelegateCommand _searchItemsCommand;
        private DelegateCommand<ObjectsClassGroup<MapObjectPin>> _detailObjectCommand;
        private DelegateCommand<MapObjectPin> _settingsObjectCommand;
        private DelegateCommand _backCommand;
        private string _searchBarText;
        public ObservableCollection<ObjectsClassGroup<MapObjectPin>> SourceItems
        {
            get
            {
                if (_sourceItems == null)
                {
                    _sourceItems = new ObservableCollection<ObjectsClassGroup<MapObjectPin>>();
                }
                return _sourceItems;
            }
            set => SetProperty(ref _sourceItems, value);
        }
        public string SearchBarText
        {
            get => _searchBarText;
            set
            {
                if (_searchBarText != value)
                {
                    SetProperty(ref _searchBarText, value);

                    if (SearchItemsCommand.CanExecute())
                    {
                        SearchItemsCommand.Execute();
                    }
                }
            }
        }
        public DelegateCommand BackCommand =>
         _backCommand ?? (_backCommand = new DelegateCommand(async () =>
         {
            await NavigationService.GoBackAsync(("UpdatePins", true));
         }));
        //public List<MapObjectPin> MapObjectPin
        //{
        //    get => _mapObjectPin;
        //    set
        //    {
        //            SetProperty(ref _mapObjectPin, value);
        //    }
        //}

        public GroupMapObjectViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
            : base(navigationService, eventAggregator, pageDialogService)
        {
            try
            {
                mapObjectService = App.DBModulManager.MapObjectService;
                categoryService = App.DBModulManager.CategoryService;
                GetDate();
                this.ToolbarItems = new ObservableCollection<ViewItem>
            {
                new ViewItem {Title= Translator.TranslatorInstance["Archive_Map"], ImageSource="outline_map.png" },
                 new ViewItem {Title= Translator.TranslatorInstance["Main_Save"], ImageSource="outline_save.png" },
                new ViewItem {Title= Translator.TranslatorInstance["Archive_Categories"], ImageSource="outline_list_add.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_Templates"], ImageSource="outline_pattern_payment.png" },
               // new ViewItem {Title=Translator.TranslatorInstance["New_Archive_ChangeColumn"], ImageSource="outline_view_col.png" }
            };

            }
            catch
            {
            }

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Categorys"))
            {
                _sourceItemsCategory = parameters["Categorys"] as ObservableCollection<Category>;
            }
            Refresh();
        }

        //public override void Initialize(INavigationParameters parameters)
        //{
        //    base.Initialize(parameters);
        //    GetDate();
        //}
        public DelegateCommand SearchItemsCommand =>
            _searchItemsCommand ?? (_searchItemsCommand = new DelegateCommand(() =>
            {
                SearchObject();
            }));


        public ObjectsClassGroup<MapObjectPin> SearchMapObject(string search, ObjectsClassGroup<MapObjectPin> mapObject)
        {


            //var res = Filter(mapObject._objects, search.ToLower());
            var res = mapObject._objects.Where(x => x.Name.ToLower().StartsWith(search.ToLower()));
            return new ObjectsClassGroup<MapObjectPin>(res.ToList(), mapObject.ObjectGroup, res.ToList().Count, mapObject.Expanded, mapObject.Rotation);
        }
        private void SearchObject()
        {
            //foreach (var item in _allsourceItems)
            //{
            //    foreach (var obj in SourceItems)
            //    {
            //        if (item.ObjectGroup == obj.ObjectGroup)
            //        {
            //            item.Expanded = obj.Expanded;
            //            item.IsEnabledObjectGroup = obj.IsEnabledObjectGroup;
            //            break;
            //        }
            //    }
            //}
            this.SourceItems = _allsourceItems;
            if (SourceItems.Count > 0)
            {
                ObservableCollection<ObjectsClassGroup<MapObjectPin>> searchSourceItems = new ObservableCollection<ObjectsClassGroup<MapObjectPin>>();
                foreach (var item in SourceItems)
                {
                    var group = SearchMapObject(SearchBarText, item);
                    if (group.Count != 0)
                        group.IsEnabledObjectGroup = item.IsEnabledObjectGroup;
                    else
                        group.IsEnabledObjectGroup = false;
                    searchSourceItems.Add(group);
                }
                if (searchSourceItems.Count != 0 && SearchBarText != "")
                {
                    this.SourceItems = searchSourceItems;
                }
            }


        }
        private IEnumerable<T> Filter<T>(IEnumerable<T> source, string searchStr)
        {
            var propsToCheck = typeof(T).GetProperties().Where(a => a.PropertyType == typeof(string) && a.CanRead);

            return source.Where(obj =>
            {
                foreach (PropertyInfo prop in propsToCheck)
                {
                    string value = (string)prop.GetValue(obj);
                    if (value != null && value.ToLower().Contains(searchStr)) return true;
                }
                return false;
            });
        }


        //    // _allsourceItems = new ObservableCollection<object>(SourceItems);
        //    IsEnabledClassifier(SourceItems);
        //}
        //public void UpdateGroup(bool value)
        //{
        //    foreach (var nd in SourceItems)
        //    {
        //        if (nd is ObjectsClassGroup<MapObjectPin>)
        //        {
        //            var dispatchFilterGroup = nd as ObjectsClassGroup<MapObjectPin>;
        //            var count = dispatchFilterGroup._objects.Where(x=>x.IsVisible == true).Count();
        //            foreach (var item in dispatchFilterGroup._objects)
        //            {
        //                if (count == 0 || !value)
        //                {
        //                    item.IsVisible = value;
        //                }
        //            }

        //        }
        //    }
        //}


        private async void GetDate()
        {
            _mapObjectPin = (await mapObjectService.GetAll()).ToList();
            _sourceItemsCategory = (await categoryService.GetAll()).ToObservableCollection();
        }
        private  void Refresh()
        {
            _mapObjectPinOld = new List<MapObjectPin>();
            foreach (var item in _mapObjectPin)
            {
                _mapObjectPinOld.Add(new MapObjectPin { Id = item.Id, Name = item.Name, Description = item.Description, IsVisible = item.IsVisible });
            }


            //_sourceItemsCategory = (await categoryService.GetAll()).ToObservableCollection();
            SourceItems = new ObservableCollection<ObjectsClassGroup<MapObjectPin>>();
            if (_mapObjectPin != null)
            {
                foreach (var item in _mapObjectPin)
                {
                    var obj = _sourceItemsCategory.Where(x => x.Id == item.Category).FirstOrDefault();
                    if(obj == null)
                    {
                        item.Category = 1;
                    }
                }
                    foreach (Category category in _sourceItemsCategory)
                {
                    var mapObjectPinSorted = _mapObjectPin.Where(x => x.Category == category.Id).ToList();
                    SourceItems.Add(new ObjectsClassGroup<MapObjectPin>(mapObjectPinSorted, category.Name.ToString(), mapObjectPinSorted.Count));
                }

            }
            _allsourceItems = new ObservableCollection<ObjectsClassGroup<MapObjectPin>>(SourceItems);
            RefreshGroupEnabled(true);
        }
        //private void IsEnabledClassifier(ObservableCollection<ObjectsClassGroup<MapObjectPin>> objects)
        //{
        //    foreach (var res in objects)
        //    {
        //        var itemObject = (res as ObjectsClassGroup<MapObjectPin>);
        //        if (_mapObjectPin != null)
        //        {
        //            itemObject.SetEnabledClassifier();
        //        }

        //    }
        //}

        protected override void ToolbarItemClicked(object parameter)
        {
            var index = ToolbarItems.IndexOf(parameter as ViewItem);


            switch (index)
            {
                case 0:
                    //SaveNewPin();
                    //var nav_param = new NavigationParameters();
                    //nav_param.Add("GroupMapObjectPin",_mapObjectPin);
                    //SaveChanges();
                    NavigationService.GoBackAsync(("UpdatePins", true));
                    break;
                case 1:

                    SaveChanges();
                    break;
                case 2:

                    //SaveChanges();
                    NavigationService.NavigateAsync("EditCategoryPage");
                    break;
                    // case 2: PaymentPattern(); break;
                    // case 3: ChangeColomnsListView(); break;
            }

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            //SaveChanges();
        }
        public DelegateCommand<MapObjectPin> ItemTappedCommand =>
           _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<MapObjectPin>((item) =>
           {
               //NavigationService.NavigateAsync("DetailRSPopupPage", (nameof(MapObjectPin), item));
               NavigationService.GoBackAsync(("MoveToPin", item));
           }));

        public DelegateCommand<MapObjectPin> RemoveItemCommand =>
          _removeItemCommand ?? (_removeItemCommand = new DelegateCommand<MapObjectPin>((item) =>
          {
              //NavigationService.NavigateAsync("DetailRSPopupPage", (nameof(MapObjectPin), item));
              //NavigationService.GoBackAsync(("MoveToPin", item));

              Remove(item);

          }));
        public DelegateCommand<ObjectsClassGroup<MapObjectPin>> DetailObjectCommand =>
          _detailObjectCommand ?? (_detailObjectCommand = new DelegateCommand<ObjectsClassGroup<MapObjectPin>>((item) =>
          {
              if (item != null)
              {
                  item.Expanded = !item.Expanded;
                  item.Rotation = item.Expanded ? 180 : 0;
                  //isStationsExpand = ObjGroup.Expanded;
              }

          }));
        public DelegateCommand<MapObjectPin> SettingsObjectCommand =>
          _settingsObjectCommand ?? (_settingsObjectCommand = new DelegateCommand<MapObjectPin>(async (item) =>
          {
              var nav_param = new NavigationParameters();
              nav_param.Add(nameof(MapObjectPin), item);
              await NavigationService.NavigateAsync("EditPinDialogPage", nav_param);

          }));

        private async void SaveChanges()
        {
            // var res = await mapObjectService.UpdateRange(_mapObjectPin);


            foreach (var item in _mapObjectPin)
            {
                foreach (var objectItem in _mapObjectPinOld)
                {
                    if (objectItem.Id == item.Id)
                    {
                        if (objectItem.Name != item.Name || objectItem.Description != item.Description || objectItem.IsVisible != item.IsVisible)
                        {
                            var res = await mapObjectService.Update(item);
                            break;
                        }
                    }
                }
            }
            Xamarin.Forms.DependencyService.Get<IToast>().ShowCustomToast(Translator.TranslatorInstance["Archive_Message"],
                   Color.FromHex("#8E97FD"), Color.FromHex("#FFFFFF"));

        }
        private async void Remove(MapObjectPin mapObjectPin)
        {
            await mapObjectService.Delete(mapObjectPin);
            foreach (var item in SourceItems)
            {
                if (item.Contains(mapObjectPin))
                {
                    item.Remove(mapObjectPin);
                    item.Count = item.Count();
                }
            }
            _mapObjectPin.Remove(mapObjectPin);
        }
        public void RefreshGroupEnabled(bool isCheckedElement)
        {
            foreach (var nd in SourceItems)
            {
                if (isCheckedElement)
                {
                    CheckGroup(nd);
                }
                else
                {
                    UncheckGroup(nd);
                }
            }

        }
        private void CheckGroup(ObjectsClassGroup<MapObjectPin> nd)
        {
            foreach (var item in nd.Objects)
            {
                if (item.IsVisible && !nd.IsEnabledObjectGroup)
                {
                    nd.IsEnabledObjectGroup = true;
                }
            }
        }
        private void UncheckGroup(ObjectsClassGroup<MapObjectPin> nd)
        {
            var count = 0;
            foreach (var item in nd.Objects)
            {
                if (item.IsVisible)
                {
                    count++;
                }
            }
            if (count == 0)
                nd.IsEnabledObjectGroup = false;
        }
    }


    public class ObjectsClassGroup<T> : ObservableCollection<T>, INotifyPropertyChanged
    {
        public List<T> _objects;
        public string ObjectGroup { get; set; }
        private int count;
        private bool _expanded;
        private int _rotation = 0;
        private bool _isEnabledObjectGroup;
        public List<T> Objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;

                    OnPropertyChanged("Expanded");
                    if (_expanded)
                    {
                        foreach (var item in _objects)
                            this.Add(item);
                    }
                    else
                    {
                        this.Clear();
                    }
                }
            }
        }
        private void UpdateItems(bool value)
        {

            if (_objects is List<MapObjectPin>)
            {
                var count = (this as ObservableCollection<MapObjectPin>).Where(x => x.IsVisible == true).Count();
                if (count == 0 || !value)
                {
                    foreach (var item in _objects as List<MapObjectPin>)
                    {
                        item.IsVisible = value;
                    }
                }

            }
        }

        public ObjectsClassGroup(List<T> objects, string objectGroup, int count, bool expanded = true, int rotation = 180)
        {
            _objects = objects;
            ObjectGroup = objectGroup;
            Count = count;
            _expanded = expanded;
            _rotation = rotation;
            if (expanded)
                foreach (var item in objects)
                    this.Add(item);

        }
        public int Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                OnPropertyChanged("Rotation");
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public bool IsEnabledObjectGroup
        {
            get => _isEnabledObjectGroup;
            set
            {
                if(_isEnabledObjectGroup != value)
                {
                    _isEnabledObjectGroup = value;
                    UpdateItems(value);
                    OnPropertyChanged("IsEnabledObjectGroup");
                }
            }
        }

        //public void SetEnabledGroup()
        //{

        //    if (_objects is List<MapObjectPin>)
        //    {
        //        var count = (this as ObservableCollection<MapObjectPin>).Where(x => x.IsVisible == true).Count();
        //        if (count == 0)
        //        {
        //            this.IsEnabledObjectGroup = false;
        //        }
        //        else
        //        {
        //            this.IsEnabledObjectGroup = true;
        //        }

        //    }
        //}

    }
}
