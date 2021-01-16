using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    class BoardPageViewModel : BaseViewModel
    {
        private readonly INodeDatabase _nodeDatabase;

        private ObservableCollection<NodeModel> _boardNodes = new ObservableCollection<NodeModel>();
        public ObservableCollection<NodeModel> BoardNodes
        {
            get => _boardNodes;
            set => SetProperty(ref _boardNodes, value);
        }

        private BoardNavigationArgs _navigatedNodeModel = new BoardNavigationArgs();
        public BoardNavigationArgs NavigatedNodeModel
        {
            get => _navigatedNodeModel;
            set => SetProperty(ref _navigatedNodeModel, value);
        }

        public BoardPageViewModel(
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
                NavigatedNodeModel = GetParameters<BoardNavigationArgs>(parameters);

                await InitNodesCollection();
            }
        }

        #region Commands

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

        #endregion

        private async Task InitNodesCollection(FilterOptionModel filterOptionModel = null)
        {   
            try
            {
                BoardNodes.Clear();

                List<NodeModel> allNodes = new List<NodeModel>();

                if (NavigatedNodeModel == null)
                {
                    if (filterOptionModel?.Status == null)
                    {
                        allNodes = (await _nodeDatabase.GetNodes()).Where(x => x.ParentId == null).ToList();
                    }
                    else
                    {
                        allNodes = (await _nodeDatabase.GetNodes()).Where(x => x.ParentId == null).Where(x => x.Status == filterOptionModel.Status).ToList();
                    }
                }
                else
                {
                    if (filterOptionModel?.Status == null)
                    {
                        allNodes = await _nodeDatabase.GetNodesByExpression(x => x.ParentId == NavigatedNodeModel.SelectedNodeModel.LocalId.Value);
                    }
                    else
                    {
                        allNodes = (await _nodeDatabase.GetNodesByExpression(x => x.ParentId == NavigatedNodeModel.SelectedNodeModel.LocalId.Value)).
                                       Where(x => x.Status == filterOptionModel.Status).ToList();
                    }
                }

                BoardNodes = new ObservableCollection<NodeModel>(allNodes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
