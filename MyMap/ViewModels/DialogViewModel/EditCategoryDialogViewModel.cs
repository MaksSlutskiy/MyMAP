using System;
using System.Threading.Tasks;
using MyMap.Interface;
using MyMap.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace MyMap.ViewModels.DialogViewModel
{
    public class EditCategoryDialogViewModel : ViewModelBase
    {
        private IService<Category> categoryService;
        private string _name;
        private DelegateCommand _okCommand;
        private DelegateCommand _backCommand;
        private DelegateCommand<object> _pickedColorCommand;
        private Category _category;
        private Color _selectedColor = Color.Gray;
        private bool _isCreated;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                    SetProperty(ref _selectedColor, value);
            }
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public DelegateCommand<object> PickedColorCommand =>
           _pickedColorCommand ?? (_pickedColorCommand = new DelegateCommand<object>((param) =>
           {
               SelectedColor = (Color)param;
           }));
        public DelegateCommand OkCommand =>
           _okCommand ?? (_okCommand = new DelegateCommand(async () =>
           {
               if(_isCreated)
               {
                   var category = await CreateCategory();
                   await NavigationService.GoBackAsync((nameof(Category), category));
               }
               else
               {
                   Save();
                   await NavigationService.GoBackAsync();
               }
           }));
        public DelegateCommand BackCommand =>
           _backCommand ?? (_backCommand = new DelegateCommand(async () =>
           {
                   await NavigationService.GoBackAsync();
           }));

        public EditCategoryDialogViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService pageDialogService)
           : base(navigationService, eventAggregator, pageDialogService)
        {
            categoryService = App.DBModulManager.CategoryService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(Category)))
            {
                _category = parameters[nameof(Category)] as Category;

                this.Name = _category.Name;
                this.SelectedColor = Color.FromHex(_category.Color);
            }
            else
            {
                _isCreated = true;
                this.Name = "New Category";
                this.SelectedColor = Color.FromHex("#000000");
            }
        }
        private void Save()
        {
            _category.Name = this.Name;
            _category.Color = this.SelectedColor.ToHex();
            var res = categoryService.Update(_category);
        }
        private async Task<Category> CreateCategory()
        {
            _category = new Category();
            _category.Name = this.Name;
            _category.Color = this.SelectedColor.ToHex();
             var caregory =  await categoryService.Create(_category);
            return caregory;
        }
    }
}
