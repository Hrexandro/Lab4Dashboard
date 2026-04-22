using System.ComponentModel.Composition;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace TextWidget
{
    [Export(typeof(TextWidgetView))]
    [Export(typeof(UserControl))]
    [ExportMetadata("Name", "Analizator tekstu")]
    public partial class TextWidgetView : UserControl
    {
        public TextWidgetView()
        {
            InitializeComponent();
        }

        public void UpdateStats(int characterCount, int wordCount)
        {
            CharactersTextBlock.Text = characterCount.ToString();
            WordsTextBlock.Text = wordCount.ToString();
        }
    }
}