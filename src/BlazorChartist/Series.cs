using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorChartist
{
    // This will be a "data-only" component. It won't render any markup,
    // so there is no need to use a .razor file (though you could).
    public class Series : ComponentBase, IDisposable
    {
        // Accept data via Razor sytnax
        [Parameter] public string Name { get; set; }
        [Parameter] public IEnumerable<double> Values { get; set; }

        // Each time params change, update a 'SeriesData' instance.
        private readonly SeriesData _data = new SeriesData();
        protected override void OnParametersSet()
        {
            _data.Name = Name;
            _data.Data = Values;
        }

        // When we're first added to the UI, attach our data to parent
        // When we're removed from the UI, remove our data from parent
        [CascadingParameter] public Chart OwnerChart { get; set; }

        protected override void OnInitialized() =>
            OwnerChart.Data.Series.Add(_data);

        void IDisposable.Dispose() =>
            OwnerChart.Data.Series.Remove(_data);
    }
}