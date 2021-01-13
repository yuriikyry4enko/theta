using Prism.Ioc;
using Prism.Unity;
using Prism;
using Theta.Pages;
using Theta.ViewModels;
using Theta.Constants;
using Theta.Interfaces;
using Theta.Database;

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

            containerRegistry.RegisterSingleton<INodeDatabase, NodeDatabase>();

            containerRegistry.RegisterForNavigation<BoardPage, BoardPageViewModel>();
            containerRegistry.RegisterForNavigation<NodeDetailsPage, NodeDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<NodeCreationPage, NodeCreationPageViewModel>();
        }

        #endregion
    }
}
