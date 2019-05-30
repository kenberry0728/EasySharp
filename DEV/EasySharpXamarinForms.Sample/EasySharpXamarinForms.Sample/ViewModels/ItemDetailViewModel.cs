﻿using System;

using EasySharpXamarinForms.Sample.Models;

namespace EasySharpXamarinForms.Sample.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}