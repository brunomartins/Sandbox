using SandboxWpf.ViewModels;
using System.Windows;

namespace SandboxWpf.View
{
    /// <summary>
    /// Interaction logic for SplashAbout.xaml
    /// </summary>
    public partial class SplashAbout : Window
    {
        private readonly SplashViewModel _viewModel;

        public SplashAbout(string sandboxVersion)
        {
            InitializeComponent();
            _viewModel = new SplashViewModel
            {
                SandboxVersion = sandboxVersion
            };
            this.DataContext = _viewModel;
        }

        void CloseBanner_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
