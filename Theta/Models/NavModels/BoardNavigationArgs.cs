using System;
namespace Theta.Models.NavModels
{
    public class BoardNavigationArgs
    {
        public NodeModel SelectedNodeModel { get; set; }

        public Action UpdatePreviousSecltion { get; set; }
    }
}
