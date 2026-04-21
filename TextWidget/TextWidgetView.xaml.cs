using System.Windows.Controls;

namespace TextWidget
{
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