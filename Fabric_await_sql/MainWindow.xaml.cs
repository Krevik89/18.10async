using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fabric_await_sql
{
    
    public partial class MainWindow : Window
    {
        DbConnection conn=null;
        DbProviderFactory fact = null;
        string ConnectionString = "";

        public MainWindow()
        {
            InitializeComponent();

        }

        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            conn.ConnectionString = ConnectionString;
            await conn.OpenAsync();

            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "waitfor delay '00:00:04';select * from dbo.Products";
            cmd.CommandText += textbox1.Text;

            DataTable table = new DataTable();
            using (DbDataReader reader = await cmd.ExecuteReaderAsync())
            {
                // do while...
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fact = DbProviderFactories.GetFactory("System.Data.SqlClient");
            conn = fact.CreateConnection();
            ConnectionString = GetConectionstring("System.Data.SqlClient");
        }
        private string GetConectionstring(string connName)
        {
            return ConfigurationManager.ConnectionStrings[ connName]; 
        }

    }
}
