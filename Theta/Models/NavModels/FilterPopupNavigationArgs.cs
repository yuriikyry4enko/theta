﻿using System;
namespace Theta.Models.NavModels
{
    public class FilterPopupNavigationArgs
    {
        public Action<FilterOptionModel> UpdatePreviousSecltion { get; set; }

        public FilterOptionModel CurrentFilterOptions { get; set; }
    }
}
