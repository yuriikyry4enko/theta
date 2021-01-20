using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Navigation;
using Theta.Constants;
using Xamarin.Forms;

namespace Theta.ViewModels.PopupViewModels
{
    class MenuPopupPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public MenuPopupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this._navigationService = navigationService;
        }

        public ICommand NavigateCommand => new Command<string>(async (navArg) =>
        {
            try
            {
                switch(navArg)
                {
                    case "All Nodes":
                        await _navigationService.NavigateAsync(PageNames.AllNodesPage, null, null, false);
                        break;
                    case "Tasks":
                        await _navigationService.NavigateAsync(PageNames.TasksPage, null, null, false);
                        break;
                    case "Tree":
                        await _navigationService.NavigateAsync(PageNames.BoardPage, null, null, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });
    }
}
