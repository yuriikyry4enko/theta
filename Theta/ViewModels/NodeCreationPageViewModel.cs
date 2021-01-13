using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Navigation;
using Theta.Interfaces;
using Theta.Models;
using Theta.Models.NavModels;
using Xamarin.Forms;

namespace Theta.ViewModels
{
    class NodeCreationPageViewModel : BaseViewModel
    {
        private readonly INodeDatabase _nodeDatabase;
        private BoardNavigationArgs _NavigationArgs;

        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        private string _nodeName;
        public string NodeName
        {
            get => _nodeName;
            set => SetProperty(ref _nodeName, value);
        }

        private string _nodeStatus;
        public string NodeStatus
        {
            get => _nodeStatus;
            set => SetProperty(ref _nodeStatus, value);
        }

        private string _nodeDescription;
        public string NodeDescription
        {
            get => _nodeDescription;
            set => SetProperty(ref _nodeDescription, value);
        }

        private string _nodeType;
        public string NodeType
        {
            get => _nodeType;
            set => SetProperty(ref _nodeType, value);
        }

        private int? _parentId;
        public int? ParentId
        {
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }

        public NodeCreationPageViewModel(
            INavigationService navigationService,
            INodeDatabase nodeDatabase) : base(navigationService)
        {
            this._nodeDatabase = nodeDatabase;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                _NavigationArgs = GetParameters<BoardNavigationArgs>(parameters);
            }
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            try
            {
                await _nodeDatabase.SaveNode(new NodeModel
                {
                    Name = NodeName,
                    Project = ProjectName,
                    Description = NodeDescription,
                    Status = NodeStatus,
                    NodeType = NodeType,
                    ParentId = ParentId,
                });

                _NavigationArgs.UpdatePreviousSecltion();

                await navigationService.GoBackAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });
    }
}
