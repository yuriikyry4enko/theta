using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Theta.Constants;
using Theta.Interfaces;
using Theta.Models;
using Theta.Models.NavModels;
using Xamarin.Forms;

namespace Theta.ViewModels
{
    class AllNodesPageViewModel : BaseViewModel
    {
        private readonly INodeDatabase _nodeDatabase;

        private ObservableCollection<NodeModel> _allNodes = new ObservableCollection<NodeModel>();
        public ObservableCollection<NodeModel> AllNodes
        {
            get => _allNodes;
            set => SetProperty(ref _allNodes, value);
        }

        private BoardNavigationArgs _navigatedNodeModel = new BoardNavigationArgs();
        public BoardNavigationArgs NavigatedNodeModel
        {
            get => _navigatedNodeModel;
            set => SetProperty(ref _navigatedNodeModel, value);
        }

        public AllNodesPageViewModel(
            INavigationService navigationService,
            INodeDatabase nodeDatabase) : base(navigationService)
        {
            this._nodeDatabase = nodeDatabase;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                await InitNodesCollection();
            }
        }

        public override ICommand FilterCommand => new Command(async () =>
        {
            await navigationService.NavigateAsync(PageNames.FilterPopupPage, CreateParameters(new FilterPopupNavigationArgs
            {
                UpdatePreviousSecltion = async (filterOptions) =>
                {
                    await InitNodesCollection(filterOptions);
                },
            }), null, false);
        });

        public ICommand NodeCreationCommad => new Command(async () =>
        {
            await navigationService.NavigateAsync(PageNames.NodeDetailsPage, CreateParameters(new NodeDetailsNavigationArgs
            {
                ParentNodeModel = NavigatedNodeModel?.SelectedNodeModel,
                UpdatePreviousSecltion = async () =>
                {
                    await InitNodesCollection();
                },
            }), null, false);
        });

        public ICommand NodeDetailsCommad => new Command<NodeModel>(async (node) =>
        {
            await navigationService.NavigateAsync(PageNames.NodeDetailsPage, CreateParameters(new NodeDetailsNavigationArgs
            {
                ParentNodeModel = NavigatedNodeModel?.SelectedNodeModel,
                SelectedNodeModel = node,
                UpdatePreviousSecltion = async () =>
                {
                    await InitNodesCollection();
                },
            }), null, false);
        });

        public ICommand NodeTappedCommad => new Command<NodeModel>(async (node) =>
        {
            await navigationService.NavigateAsync(PageNames.BoardPage, CreateParameters(new BoardNavigationArgs
            {
                SelectedNodeModel = node,
            }), null, false);
        });

        private async Task InitNodesCollection(FilterOptionModel filterOptionModel = null)
        {
            try
            {
                AllNodes.Clear();

                AllNodes = new ObservableCollection<NodeModel>(await _nodeDatabase.GetNodesByFilterOptions(InitFilterOptionsList(filterOptionModel)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
