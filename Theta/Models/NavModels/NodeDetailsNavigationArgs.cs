using System;
namespace Theta.Models.NavModels
{
    public class NodeDetailsNavigationArgs
    {
        public NodeModel SelectedNodeModel { get; set; }

        public NodeModel ParentNodeModel { get; set; }

        public Action UpdatePreviousSecltion { get; set; }
    }
}
