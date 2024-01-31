using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Extensions.Configuration;

namespace PersonManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string? connStr;
        IConfiguration? config;
        public MainWindow()
        {
            InitializeComponent();
            SetupBuilder();
            frame.Navigate(new ListPeoplePage(new ViewModels.PersonViewModel())
            {
                Frame = frame
            });
        }

        private void SetupBuilder()
        {

            /* PersonManager -> Manage User Secrets
               "ConnectionStrings": {
                  "pppkConnString": "Server=..."
                }
             */
            config = new ConfigurationBuilder()
                .AddUserSecrets<MainWindow>()
                .Build();
            connStr = config.GetConnectionString("pppkConnString");

        }
    }
}
