using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hotel24Eq5.Commands;
using System.Threading.Tasks;
using hotel24Eq5.Models;
using hotel24Eq5.ViewModels;

namespace hotel24Eq5.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand CmdGotoAccueil { get; private set; }
        public RelayCommand CmdGotoReservation { get; private set; }
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

            CurrentViewModel = new ReservationViewModel();
           
            CmdGotoAccueil = new RelayCommand(GotoAccueil,null);
            CmdGotoReservation = new RelayCommand(GotoReservation, null);
        }

        private void GotoReservation(object obj)
        {
            CurrentViewModel = new ReservationViewModel();
        }

        private void GotoAccueil(object obj)
            {
                CurrentViewModel = new AccueilViewModel();
            }

        }
    }
