using Prism.Ioc;
using Prism.Unity;
using Prism;
using Theta.Pages;
using Theta.ViewModels;
using Theta.Constants;
using Theta.Interfaces;
using Theta.Database;
using Theta.Pages.PopupPages;
using Theta.ViewModels.PopupViewModels;
using Rg.Plugins.Popup.Services;
using Acr.UserDialogs;

namespace Theta
{
    public partial class App : PrismApplication
    {
        #region Prism  
        
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(PageNames.BoardPage);

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(PopupNavigation.Instance);

            containerRegistry.RegisterSingleton<INodeDatabase, NodeDatabase>();

            containerRegistry.RegisterForNavigation<BoardPage, BoardPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPopupPage, MenuPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<FilterPopupPage, FilterPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>();
            containerRegistry.RegisterForNavigation<AllNodesPage, AllNodesPageViewModel>();
            containerRegistry.RegisterForNavigation<NodeDetailsPage, NodeDetailsPageViewModel>();
        }

        #endregion
    }
}
