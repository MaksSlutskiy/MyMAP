using System;
using System.Collections.ObjectModel;
using System.Linq;
using MyMap.Controls;
using MyMap.Extensions;
using MyMap.Interface;
using MyMap.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace MyMap.ViewModels
{
    public class EditCategoryViewModel : ViewModelToolbarBase, IInitialize
    {
        private IService<Category> categoryService;
        private ObservableCollection<Category> _sourceItems;
        private DelegateCommand<Category> _itemTappedCommand;
        private DelegateCommand<Category> _removeItemCommand;

        public ObservableCollection<Category> SourceItems
        {
            get
            {
                if (_sourceItems == null)
                {
                    _sourceItems = new ObservableCollection<Category>();
                }
                return _sourceItems;
            }
            set => SetProperty(ref _sourceItems, value);
        }
        public DelegateCommand<Category> ItemTappedCommand =>
           _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<Category>((item) =>
           {
               NavigationService.NavigateAsync("EditCategoryDialogPage", (nameof(Category), item));
               //NavigationService.GoBackAsync(("MoveToPin", item));
           }));

        public DelegateCommand<Category> RemoveItemCommand =>
          _removeItemCommand ?? (_removeItemCommand = new DelegateCommand<Category>((item) =>
          {
              Remove(item);
          }));
        public EditCategoryViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
            : base(navigationService, eventAggregator, pageDialogService)
            {
            try
            {
                categoryService = App.DBModulManager.CategoryService;
                GetDate();
                this.ToolbarItems = new ObservableCollection<ViewItem>
            {
                new ViewItem {Title= "item0", ImageSource="outline_book.png" },
                new ViewItem {Title= "item1", ImageSource="outline_add.png" },
            };

            }
            catch
            {
            }
        }
        //public override void Initialize(INavigationParameters parameters)
        //{
        //    base.Initialize(parameters);
        //    GetDate();
        //}
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey(nameof(Category)))
            {
                var category = parameters[nameof(Category)] as Category;
                if (category != null)
                {
                    SourceItems.Add(category);
                }

            }
        }
        protected override void ToolbarItemClicked(object parameter)
        {
            var index = ToolbarItems.IndexOf(parameter as ViewItem);


            switch (index)
            {
                case 0:
                    SaveChanges();
                    NavigationService.GoBackAsync(("Categorys", _sourceItems));
                    break;
                case 1:
                    NavigationService.NavigateAsync("EditCategoryDialogPage");
                    break;
                    // case 2: PaymentPattern(); break;
                    // case 3: ChangeColomnsListView(); break;
            }
        }

        private async void GetDate()
        {
            try
            {
                SourceItems = (await categoryService.GetAll()).ToObservableCollection();
            }
            catch
            {
                SourceItems = (await categoryService.GetAll()).ToObservableCollection();
            }
            SourceItems = (await categoryService.GetAll()).ToObservableCollection();
        }
        private async void SaveChanges()
        {
            foreach (var item in SourceItems)
            {
                await categoryService.Update(item);
            }
        }
        //private async void CreateCategory()
        //{
        //    Category category = new Category();
        //    category.Name = "Test";
        //    category.Color = "#000000";
        //    await categoryService.Create(category);
        //    SourceItems.Add(category);
        //}
        private async void Remove(Category category)
        {
            if(category.Id != 1)
            {
                await categoryService.Delete(category);
                SourceItems.Remove(category);
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Message",
                       "This category is static", "Ok");

            }
        }
    }
}
