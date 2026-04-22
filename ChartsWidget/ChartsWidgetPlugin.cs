using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using Contracts;

namespace ChartsWidget
{
    [Export(typeof(IWidget))]
    [ExportMetadata("Name", "Wykres liczb")]
    public class ChartsWidgetPlugin : IWidget
    {
        private readonly ChartsWidgetView _view;

        public string Name => "Wykres liczb";
        public object View => _view;

        [ImportingConstructor]
        public ChartsWidgetPlugin(IEventAggregator eventAggregator, ChartsWidgetView view)
        {
            _view = view;
            eventAggregator.Subscribe<DataSubmittedEvent>(OnDataReceived);
        }

        private void OnDataReceived(DataSubmittedEvent payload)
        {
            var text = payload?.Data ?? string.Empty;
            var parts = text.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var values = new List<double>();

            foreach (var part in parts)
            {
                if (double.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    if (number < 0) number = 0;
                    if (number > 150) number = 150;
                    values.Add(number);
                }
            }

            _view.UpdateChart(values);
        }
    }
}