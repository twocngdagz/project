using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
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
    /// Interaction logic for TenantDetail.xaml
    /// </summary>
    public partial class TenantDetail : UserControl
    {
        public TenantDetail()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            object dc = this.DataContext;
            IPartialObject po = (IPartialObject)dc;
            po.Update();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            object dc = this.DataContext;
            IPartialObject po = (IPartialObject)dc;
            po.Delete();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            object dc = this.DataContext;
            IPartialObject po = (IPartialObject)dc;
            po.Add();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            object dc = this.DataContext;
            IPartialObject po = (IPartialObject)dc;
            po.Clear();
        }

        private void label4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object dc = this.DataContext;
                PartialTenant pt = (PartialTenant)dc;
                string path = System.IO.Path.Combine(@"P:\OurTenancies", pt.Directory);
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening directory : " + ex.Message);
            }
        }
    }
}
