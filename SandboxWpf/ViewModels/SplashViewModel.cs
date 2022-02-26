using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxWpf.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private string _sandboxVersion;

        public SplashViewModel()
        {
        }

        public string SandboxVersion
        {
            get => _sandboxVersion;
            set
            {
                _sandboxVersion = value;
                OnPropertyChanged(nameof(SandboxVersion));
            }
        }
    }
}
