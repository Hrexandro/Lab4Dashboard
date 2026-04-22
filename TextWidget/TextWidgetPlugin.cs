using System;
using System.ComponentModel.Composition;
using Contracts;

namespace TextWidget
{
    [Export(typeof(IWidget))]
    [ExportMetadata("Name", "Analizator tekstu")]
    public class TextWidgetPlugin : IWidget
    {
        private readonly TextWidgetView _view;

        public string Name => "Analizator tekstu";
        public object View => _view;

        [ImportingConstructor]
        public TextWidgetPlugin(IEventAggregator eventAggregator, TextWidgetView view)
        {
            _view = view;
            eventAggregator.Subscribe<DataSubmittedEvent>(OnDataReceived);
        }

        private void OnDataReceived(DataSubmittedEvent payload)
        {
            var text = payload?.Data ?? string.Empty;

            int characterCount = text.Length;
            int wordCount = text
                .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;

            _view.UpdateStats(characterCount, wordCount);
        }
    }
}