using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Contracts;

namespace DashboardApp
{
    public partial class MainWindow : Window
    {
        [Import]
        private IEventAggregator _eventAggregator;

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IWidget, IWidgetMetadata>> Widgets { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadWidgets()
        {
            WidgetsTabControl.Items.Clear();

            if (Widgets == null)
                return;

            foreach (var lazyWidget in Widgets)
            {
                var tab = new TabItem
                {
                    Header = lazyWidget.Metadata.Name,
                    Content = lazyWidget.Value.View
                };

                WidgetsTabControl.Items.Add(tab);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_eventAggregator == null)
                return;

            _eventAggregator.Publish(new DataSubmittedEvent(InputTextBox.Text));
        }
    }
}