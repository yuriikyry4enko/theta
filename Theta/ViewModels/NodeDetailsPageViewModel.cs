using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Theta.Models;

namespace Theta.ViewModels
{
    class NodeDetailsPageViewModel : BaseViewModel
    {
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

        private NodeModel _navigatedNodeModel = new NodeModel();
        public NodeModel NavigatedNodeModel
        {
            get => _navigatedNodeModel;
            set => SetProperty(ref _navigatedNodeModel, value);
        }


        public NodeDetailsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                NavigatedNodeModel = GetParameters<NodeModel>(parameters);

                InitNodeInfo();
            }
        }

        private void InitNodeInfo()
        {
            ProjectName= NavigatedNodeModel.Project;
            NodeName = NavigatedNodeModel.Name;
            NodeDescription = NavigatedNodeModel.Description;
            NodeStatus = NavigatedNodeModel.Status;
            NodeType = NavigatedNodeModel.NodeType;
        }
    }
}
