﻿using System;
namespace Theta.Models.NavModels
{
    public class BoardNavigationArgs
    {
        public NodeModel SelectedNodeModel { get; set; } = new NodeModel();

        public Action UpdatePreviousSecltion { get; set; }
    }
}
