using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.DropdownTest
{
    public class DowndownComponent<TItem> : ComponentBase
    {
        [Parameter]
        public RenderFragment InitialTip { get; set; }
        //[Parameter]
        //public RenderFragment ChildContent { get; set; }
        [Parameter]
        public RenderFragment ItemTemplate { get; set; }
        [Parameter]
        public EventCallback<TItem> OnSelected { get; set; }

        protected bool show = false;

        protected RenderFragment Tip;

        protected override void OnInitialized() { this.Tip = InitialTip; }
        public async Task HandleSelect(TItem item, RenderFragment<TItem> contentFragment)
        {
            this.Tip = contentFragment.Invoke(item);
            show = false;
            StateHasChanged();
            await this.OnSelected.InvokeAsync(item);
        }
    }
}
