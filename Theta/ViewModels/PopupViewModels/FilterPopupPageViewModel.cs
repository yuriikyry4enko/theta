using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Theta.Constants;
using Theta.Models.NavModels;
using Xamarin.Forms;

namespace Theta.ViewModels.PopupViewModels
{
    class FilterPopupPageViewModel : BaseViewModel
    {
        private int? _nodeStatusFilterId;
        public int? NodeStatusFilterId
        {
            get => _nodeStatusFilterId;
            set => SetProperty(ref _nodeStatusFilterId, value);
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

        private DateTime? _beginDate;
        public DateTime? BeginDate
        {
            get => _beginDate;
            set => SetProperty(ref _beginDate, value);
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public DateTime MinDate { get; } = DateTime.Now;
        public DateTime MaxDate { get; } = DateTime.Now.AddMonths(12);

        private FilterPopupNavigationArgs NavigationArgs;

        public List<string> Statuses { get; } = DictionariesConstants.Statuses.Values.ToList();
        public List<string> NodeTypes { get; } = DictionariesConstants.NodeTypes.Values.ToList();
        public List<string> Priorities { get; } = DictionariesConstants.Priorities.Values.ToList();
        public List<string> Employees { get; } = DictionariesConstants.Employees.Values.ToList();

        public FilterPopupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() != NavigationMode.Back)
            {
                NavigationArgs = GetParameters<FilterPopupNavigationArgs>(parameters);

                InitCurrentFilterOptions();
            }
        }

        public ICommand ApplyFilterCommand => new Command(async () =>
        {
            try
            {
                NavigationArgs.UpdatePreviousSecltion(new Models.FilterOptionModel
                {
                    Status = NodeStatusFilterId,
                    Priority = PriorityId,
                    AssignedEmployeeId = AssignedEmployeeId,
                    NodeType = NodeTypeId,
                    //BeginDate = BeginDate,
                    //EndDate = EndDate,
                });

                await navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        });

        public ICommand GoBackToRootCommand => new Command(async () =>
        {
            await navigationService.GoBackToRootAsync();
        });

        private void InitCurrentFilterOptions()
        {
            NodeTypeId = NavigationArgs?.CurrentFilterOptions?.NodeType;
            PriorityId = NavigationArgs?.CurrentFilterOptions?.Priority;
            NodeStatusFilterId = NavigationArgs?.CurrentFilterOptions?.Status;
            AssignedEmployeeId = NavigationArgs?.CurrentFilterOptions?.AssignedEmployeeId;
        }
    }
}
