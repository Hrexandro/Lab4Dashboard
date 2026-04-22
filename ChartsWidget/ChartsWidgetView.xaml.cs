using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChartsWidget
{
    [Export(typeof(ChartsWidgetView))]
    [Export(typeof(UserControl))]
    [ExportMetadata("Name", "Wykres liczb")]
    public partial class ChartsWidgetView : UserControl
    {
        public ChartsWidgetView()
        {
            InitializeComponent();
        }

        public void UpdateChart(List<double> values)
        {
            BarsPanel.Children.Clear();

            foreach (var value in values)
            {
                var bar = new Rectangle
                {
                    Width = 30,
                    Height = value,
                    Margin = new System.Windows.Thickness(5, 0, 5, 0),
                    Fill = Brushes.SteelBlue,
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom
                };

                BarsPanel.Children.Add(bar);
            }
        }
    }
}