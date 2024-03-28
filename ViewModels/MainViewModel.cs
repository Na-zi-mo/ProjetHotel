using hotel24Eq5.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotel24Eq5.ViewModels;

namespace hotel24Eq5.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand CmdGotoAccueil { get; private set; }
        public RelayCommand CmdGotoChambre { get; private set; }

        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel()
        {

            CurrentViewModel = new AccueilViewModel();
            // le contentControl doit savoir afficher un AccueilViewModel

            CmdGotoAccueil = new RelayCommand(GotoAccueil, null);
            CmdGotoChambre = new RelayCommand(GotoChambre, null);
        }

        private void GotoAccueil(object obj)
        {
            CurrentViewModel = new AccueilViewModel();
        }
        private void GotoChambre(object obj)
        {
            CurrentViewModel = new ChambreViewModel();
        }
    }
}
