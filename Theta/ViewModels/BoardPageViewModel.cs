using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        #endregion

        private async Task InitNodesCollection(FilterOptionModel filterOptionModel = null)
        {   
            try
            {
                BoardNodes.Clear();

                BoardNodes = new ObservableCollection<NodeModel>(await _nodeDatabase.GetNodesByFilterOptions(InitFilterOptionsList(filterOptionModel)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private List<string> InitFilterOptionsList(FilterOptionModel filterOptionModel)
        {
            try
            {
                List<string> filterOptions = new List<string>();

                if (filterOptionModel != null)
                {
                    foreach (PropertyInfo propertyInfo in filterOptionModel.GetType()?.GetProperties())
                    {
                        var value = propertyInfo.GetValue(filterOptionModel, null);
                        if (value != null)
                            filterOptions.Add($"{propertyInfo.Name} = {value}");
                    }
                }

                filterOptions.Add($"{nameof(NavigatedNodeModel.SelectedNodeModel.ParentId)} {(NavigatedNodeModel?.SelectedNodeModel?.LocalId == null ? "IS NULL" : $"= {NavigatedNodeModel?.SelectedNodeModel?.LocalId.ToString()}")}");
                
                return filterOptions;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}
