﻿using System;
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

        public FilterOptionModel CurrentFilterOptions { get; set; }

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
                CurrentFilterOptions = CurrentFilterOptions,
                UpdatePreviousSecltion = async (filterOptions) =>
                {
                    await InitNodesCollection(filterOptions);

                    CurrentFilterOptions = filterOptions;
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

                var paramParentId = $"{nameof(NavigatedNodeModel.SelectedNodeModel.ParentId)} {(NavigatedNodeModel?.SelectedNodeModel?.LocalId == null ? "IS NULL" : $"= {NavigatedNodeModel?.SelectedNodeModel?.LocalId.ToString()}")}";

                BoardNodes = new ObservableCollection<NodeModel>(await _nodeDatabase.GetNodesByFilterOptions(InitFilterOptionsList(filterOptionModel, paramParentId)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
