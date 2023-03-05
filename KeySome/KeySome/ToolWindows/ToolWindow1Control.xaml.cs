using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KeySome
{
    public partial class ToolWindow1Control : UserControl
    {
        public ToolWindow1Control()
        {
            InitializeComponent();
        }

        public void SetItems(IEnumerable<KeyItem> items)
        {
            CommandsTreeView.ItemsSource = items;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("ToolWindow1Control", "Button clicked");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var l1 = new TreeViewItem() { Header = "1" };
            
            var itemContainerTemplate = new ItemContainerTemplate(){ }; //todo

            var l2 = new TreeViewItem() { HeaderTemplate = itemContainerTemplate  };

            CommandsTreeView.Items.Add(l1);
        }
    }
}
