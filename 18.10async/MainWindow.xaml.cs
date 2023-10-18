using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _18._10async
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();        
        }


        string SlowMethod(CancellationToken token)
        {
            Thread.Sleep(5000);
            token.ThrowIfCancellationRequested();
            return "Жди";
        }

        async private Task GetDataAsync(string filePath)
        {         
            byte[] data = null;
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                data = new byte[fs.Length];
                await fs.ReadAsync(data, 0, data.Length);
            }
            Tbox.Text += Encoding.UTF8.GetString(data);
        }

        private Task<string> SlowMethofAsync(CancellationToken token)
        {
            return Task.Run<string>(() => { return SlowMethod(token); });
        }

        async private  void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(3));

                Tbox.Text = "before async code\n";
                //await GetDataAsync(@"C:\Users\РПО\Desktop\Работа с файлами.txt");
                Tbox.Text += await SlowMethofAsync(cts.Token);
                //Tbox.Text=SlowMethod();
                Tbox.Text = "\nafter async code\n";
            }catch(OperationCanceledException es)
            {
                MessageBox.Show(es.Message);
            }
            

            //Tbox.Text=SlowMethod();
            //await GetDataAsync(@"C:\Users\РПО\Desktop\Работа с файлами.txt");

            //CancellationTokenSource; отмена операции в др методе

        }
    }
}
// async await;
// int r = await MethodAsync();
// await MethodAsync(); 
// ecли void Task t = MethodAsync();
// await t;

/*private async void Foo()
        {
            InitializeComponent();
            //async await;
            int r = await MethodAsync(); //ecли void
        }

        async Task<int> MethodAsync()
        {
            int result=0;
            //code
            //result  object <T>;
            return result;
        }

        async Task MethodAsync2() //async void или async Task
        {
            //code
        }
*/