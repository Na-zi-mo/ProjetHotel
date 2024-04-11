using hotel24Eq5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotel24Eq5.Commands;
using System.ComponentModel;

namespace hotel24Eq5.ViewModels
{
    public class ArriveViewModel : BaseViewModel
    {
        #region RelayCommands

        #endregion

        private DataServiceHotel ds = new DataServiceHotel();


        ICollectionView arrivesViewSource;
        
        public ICollectionView ArrivesViewSource
        {
            get => arrivesViewSource;
            set
            {
                arrivesViewSource = value;
                OnPropertyChanged();
            }
        }
        ICollectionView clientsViewSource;

        public ICollectionView ClientsViewSource
        {
            get => clientsViewSource;
            set
            {
                clientsViewSource = value;
                OnPropertyChanged();
            }
        }

        ICollectionView chambresViewSource;

        public ICollectionView ChambresViewSource
        {
            get => chambresViewSource;
            set
            {
                chambresViewSource = value;
                OnPropertyChanged();
            }
        }

        public ArriveViewModel() 
        {
        
        }


    }
}
