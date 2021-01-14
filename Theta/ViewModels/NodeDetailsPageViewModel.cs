using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Navigation;
using Theta.Constants;
using Theta.Interfaces;
using Theta.Models;
using Theta.Models.NavModels;
using Xamarin.Forms;

namespace Theta.ViewModels
{
    class NodeDetailsPageViewModel : BaseViewModel
    {
        private readonly INodeDatabase _nodeDatabase;
        private readonly IUserDialogs _userDialogs;

        private NodeDetailsNavigationArgs _navigationArgs;
        public NodeDetailsNavigationArgs NavigationArgs
        {
            get => _navigationArgs;
            set => SetProperty(ref _navigationArgs, value);
        }

        private bool _isEditable;
        public bool IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value);
        }

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

        private int? _nodeStatusId;
        public int? NodeStatusId
        {
            get => _nodeStatusId;
            set => SetProperty(ref _nodeStatusId, value);
        }

        private int? _nodeTypeId;
        public int? NodeTypeId
        {
            get => _nodeTypeId;
            set => SetProperty(ref _nodeTypeId, value);
        }

        private int? _priorityId;
        public int? PriorityId
        {
            get => _priorityId;
            set => SetProperty(ref _priorityId, value);
        }

        private int? _assignedEmployeeId;
        public int? AssignedEmployeeId
        {
            get => _assignedEmployeeId;
            set => SetProperty(ref _assignedEmployeeId, value);
        }

        private string _nodeDescription;
        public string NodeDescription
        {
            get => _nodeDescription;
            set => SetProperty(ref _nodeDescription, value);
        }

        private DateTime _beginDate;
        public DateTime BeginDate
        {
            get => _beginDate;
            set => SetProperty(ref _beginDate, value);
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private NodeModel _parentModel = new NodeModel();
        public NodeModel ParentModel
        {
            get => _parentModel;
            set => SetProperty(ref _parentModel, value);
        }

        public DateTime MinDate { get; } = DateTime.Now;
        public DateTime MaxDate { get; } = DateTime.Now.AddMonths(12);

        public List<string> NodeTypes { get; } = DictionariesConstants.NodeTypes.Values.ToList();
        public List<string> Statuses { get; } = DictionariesConstants.Statuses.Values.ToList();
        public List<string> Priorities { get; } = DictionariesConstants.Priorities.Values.ToList();
        public List<string> Employees { get; } = DictionariesConstants.Employees.Values.ToList();

        private ObservableCollection<NodeModel> _posibleChildNodes = new ObservableCollection<NodeModel>();
        public ObservableCollection<NodeModel> PosibleChildNodes
        {
            get => _posibleChildNodes;
            set => SetProperty(ref _posibleChildNodes, value);
        }

        private ObservableCollection<NodeModel> _childNodes = new ObservableCollection<NodeModel>();
        public ObservableCollection<NodeModel> ChildNodes
        {
            get => _childNodes;
            set => SetProperty(ref _childNodes, value);
        }

        private ObservableCollection<NodeModel> _posibleParentNodes;
        public ObservableCollection<NodeModel> PosibleParentNodes
        {
            get => _posibleParentNodes;
            set => SetProperty(ref _posibleParentNodes, value);
        }

        public NodeDetailsPageViewModel(
            IUserDialogs userDialogs,
            INavigationService navigationService,
            INodeDatabase nodeDatabase) : base(navigationService)
        {
            this._nodeDatabase = nodeDatabase;
            this._userDialogs = userDialogs;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                NavigationArgs = GetParameters<NodeDetailsNavigationArgs>(parameters);

                await InitNodeDetail();
            }
        }

        public ICommand DeleteNodeModelCommand => new Command(async () =>
        {
            try
            {
                await _nodeDatabase.DeleteNode(NavigationArgs.SelectedNodeModel);

                NavigationArgs.UpdatePreviousSecltion();

                await navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand SaveEditCommand => new Command(async () =>
        {
            try
            {
                if (IsEditable)
                {
                    IsEditable = false;

                    if (NodeTypeId == null || string.IsNullOrEmpty(NodeName))
                    {
                        await _userDialogs.AlertAsync("You need to enter Name and Type for saving", "Empty data", "Ok");
                        return;
                    }

                    if (NavigationArgs?.SelectedNodeModel == null)
                    {
                        await _nodeDatabase.SaveNode(new NodeModel
                        {
                            Name = NodeName,
                            Project = ProjectName,
                            Description = NodeDescription,
                            Status = NodeStatusId,
                            Priority = PriorityId,
                            NodeType = NodeTypeId,
                            AssignedEmployeeId = AssignedEmployeeId,
                            ParentId = ParentModel?.LocalId,
                            BeginDate = BeginDate,
                            EndDate = EndDate,
                        });
                    }
                    else
                    {
                        NavigationArgs.SelectedNodeModel.Name = NodeName;
                        NavigationArgs.SelectedNodeModel.Project = ProjectName;
                        NavigationArgs.SelectedNodeModel.Description = NodeDescription;
                        NavigationArgs.SelectedNodeModel.Status = NodeStatusId;
                        NavigationArgs.SelectedNodeModel.Priority = PriorityId;
                        NavigationArgs.SelectedNodeModel.NodeType = NodeTypeId;
                        NavigationArgs.SelectedNodeModel.AssignedEmployeeId = AssignedEmployeeId;
                        NavigationArgs.SelectedNodeModel.ParentId = ParentModel?.LocalId;
                        NavigationArgs.SelectedNodeModel.BeginDate = BeginDate;
                        NavigationArgs.SelectedNodeModel.EndDate = EndDate;

                        await _nodeDatabase.SaveNode(NavigationArgs.SelectedNodeModel);
                    }

                    NavigationArgs.UpdatePreviousSecltion();

                    await navigationService.GoBackAsync();
                }
                else
                {
                    IsEditable = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand AddChildCommand => new Command(() =>
        {
            ChildNodes.Add(new NodeModel());
        });

        public ICommand DeleteChildNodeCommand => new Command<NodeModel>((item) =>
        {
            ChildNodes.Remove(item);
        });

        public ICommand SelectedIndexNodeTypeChangedCommad => new Command(async () =>
        {
            await GetPosibleChildNodes();

            await GetPosibleParentsNodes();
        });

        private async Task InitNodeDetail()
        {
            try
            {
                if (NavigationArgs?.SelectedNodeModel != null)
                {
                    NodeName = NavigationArgs.SelectedNodeModel.Name;
                    ProjectName = NavigationArgs.SelectedNodeModel.Project;
                    NodeDescription = NavigationArgs.SelectedNodeModel.Description;
                    NodeStatusId = NavigationArgs.SelectedNodeModel.Status;
                    NodeTypeId = NavigationArgs.SelectedNodeModel.NodeType;
                    PriorityId = NavigationArgs.SelectedNodeModel.Priority;
                    AssignedEmployeeId = NavigationArgs.SelectedNodeModel.AssignedEmployeeId;
                    BeginDate = NavigationArgs.SelectedNodeModel.BeginDate;
                    EndDate = NavigationArgs.SelectedNodeModel.EndDate;
                }
                else
                {
                    IsEditable = true;
                }

                await GetPosibleParentsNodes();

                await GetPosibleChildNodes(true);

                if (NavigationArgs.SelectedNodeModel?.ParentId != null)
                {
                    ParentModel = PosibleParentNodes.FirstOrDefault(x => x.LocalId == NavigationArgs.SelectedNodeModel.ParentId);
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task GetPosibleChildNodes(bool initEmptyChildList = false)
        {
            try
            {
                PosibleChildNodes = new ObservableCollection<NodeModel>(await _nodeDatabase.GetNodesByExpression(
                   NavigationArgs?.SelectedNodeModel?.LocalId == null ?
                       x => x.NodeType > NodeTypeId :
                       x => x.LocalId != NavigationArgs.SelectedNodeModel.LocalId && x.NodeType > NodeTypeId
                ));

                if (initEmptyChildList)
                {
                    foreach (var childNodes in PosibleChildNodes)
                    {
                        if (childNodes.ParentId == NavigationArgs?.SelectedNodeModel?.LocalId)
                        {
                            ChildNodes.Add(childNodes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task GetPosibleParentsNodes()
        {
            try
            {
                PosibleParentNodes = new ObservableCollection<NodeModel>(await _nodeDatabase.GetNodesByExpression(
                    NavigationArgs?.SelectedNodeModel?.LocalId == null ?
                        x => x.NodeType < NodeTypeId :
                        x => x.NodeType < NodeTypeId && x.LocalId != NavigationArgs.SelectedNodeModel.LocalId
                ));

                if (PosibleParentNodes.Count() == 0)
                    ParentModel = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
