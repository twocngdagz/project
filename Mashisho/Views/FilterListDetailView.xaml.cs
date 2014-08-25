using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mashisho
{
    /// <summary>
    /// Interaction logic for FilterListDetailView.xaml
    /// </summary>
    public partial class FilterListDetailView : UserControl
    {
        public FilterListDetailView()
        {
            InitializeComponent();

            this.AddHandler(TextBox.KeyDownEvent, new RoutedEventHandler(TextBox_KeyDown));
        }

        // This bit of code to get the binding to update when [enter] is pressed ... >
        private void TextBox_KeyDown(object sender, RoutedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;
            KeyEventArgs args = e as KeyEventArgs;
            if (args != null && args.Key == Key.Enter && textBox != null)
            {
                BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
                if (binding != null)
                {
                    binding.UpdateSource();
                }
            }
        }
    }
}
