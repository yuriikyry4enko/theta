using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Theta.Constants;
using Theta.Models.NavModels;
using Xamarin.Forms;

namespace Theta.ViewModels.PopupViewModels
{
    class FilterPopupPageViewModel : BaseViewModel
    {
        private int? _nodeStatusFilterId;
        public int? NodeStatusFilterId
        {
            get => _nodeStatusFilterId;
            set => SetProperty(ref _nodeStatusFilterId, value);
        }

        private FilterPopupNavigationArgs NavigationArgs;

        public List<string> Statuses { get; } = DictionariesConstants.Statuses.Values.ToList();

        public FilterPopupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                NavigationArgs = GetParameters<FilterPopupNavigationArgs>(parameters);
            }
        }

        public ICommand ApplyFilterCommand => new Command(async () =>
        {
            try
            {
                NavigationArgs.UpdatePreviousSecltion(new Models.FilterOptionModel
                {
                    Status = NodeStatusFilterId,
                });

                await navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand GoBackToRootCommand => new Command(async () =>
        {
            await navigationService.GoBackToRootAsync();
        });

    }
}
