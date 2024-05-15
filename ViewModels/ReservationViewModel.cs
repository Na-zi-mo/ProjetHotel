using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using hotel24Eq5.Models;
using hotel24Eq5.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using hotel24;
using System.Runtime.Remoting.Contexts;
using System.Windows.Input;
using System.Data.SqlClient;

namespace hotel24Eq5.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {


        #region Relaycommand
        public RelayCommand AddReserveCommand { get; private set; }
        public RelayCommand DeleteReserveCommand { get; private set; }
        public RelayCommand EditReserveCommand { get; private set; }
        public RelayCommand SaveReserveCommand { get; private set; }
        public RelayCommand CancelReserveCommand { get; private set; }
        public RelayCommand AddECommand { get; private set; }

        public RelayCommand AddCCommand { get; private set; }
        public RelayCommand RemoveECommand { get; private set; }
        public RelayCommand SelectClientCommand { get; private set; }
        public RelayCommand SelectChambreCommand { get; private set; }
        public RelayCommand CancelSelectClientCommand { get; private set; }
        public RelayCommand CancelSelectChambreCommand { get; private set; }


        public ObservableCollection<CLIENT> ClientsSansReservations { get; }

        #endregion
        private DataService ds = new DataService();
        ICollectionView reservesViewSource;
        ICollectionView clientsViewSource;
        ICollectionView arrivesViewSource;
        ICollectionView desViewSource;
        ICollectionView chambresViewSource;
        ICollectionView clientsansreserveViewSource;
        ICollectionView chambresnoninclusViewSource;



        public ICollectionView ReservesViewSource
        {
            get => reservesViewSource;
            set
            {
                reservesViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView ClientsViewSource
        {
            get => clientsViewSource;
            set
            {
                clientsViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView ArrivesViewSource
        {
            get => arrivesViewSource;
            set
            {
                arrivesViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView DesViewSource
        {
            get => desViewSource;
            set
            {
                desViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView ChambresViewSource
        {
            get => chambresViewSource;
            set
            {
                chambresViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView ClientsansreserveViewSource
        {
            get => clientsansreserveViewSource;
            set
            {
                clientsansreserveViewSource = value;
                OnPropertyChanged();

            }
        }
        public ICollectionView ChambresNonInclusViewSource
        {
            get => chambresnoninclusViewSource;
            set
            {
                chambresnoninclusViewSource = value;
                OnPropertyChanged();

            }
        }

        public ReservationViewModel()
        {
            ReservesViewSource = CollectionViewSource.GetDefaultView(ds.Reserves);
            ClientsViewSource = CollectionViewSource.GetDefaultView(ds.Clients);
            ArrivesViewSource = CollectionViewSource.GetDefaultView(ds.Arrives);
            DesViewSource = CollectionViewSource.GetDefaultView(ds.Des);
            ChambresViewSource = CollectionViewSource.GetDefaultView(ds.Chambres);

            AddReserveCommand = new RelayCommand(AddReserve, CanBeginEdit);
            DeleteReserveCommand = new RelayCommand(DeleteReserve, CanBeginEdit);
            EditReserveCommand = new RelayCommand(EditReserve, CanBeginEdit);
            SaveReserveCommand = new RelayCommand(SaveReserve, CanEndEdit);
            CancelReserveCommand = new RelayCommand(CancelReserve, CanEndEdit);


            SelectChambreCommand = new RelayCommand(SelectChambre,null);
            CancelSelectChambreCommand= new RelayCommand(CancelChambre,null);

            SelectClientCommand = new RelayCommand(SelectClient, null);
            CancelSelectClientCommand = new RelayCommand(CancelClient, null);

            RemoveECommand = new RelayCommand(RemoveDe, CanEndEdit);
            AddECommand = new RelayCommand(AddEReserve, CanEndEdit);

            AddCCommand = new RelayCommand(AddCClient, CanEndEdit);

         //   ClientsSansReservations = ds.GetClientsWithoutReservations();


        }

        private bool isEditing = true;

        public bool IsEditing
        {
            get => isEditing;
            set
            {
                if (isEditing != value)
                {
                    isEditing = value;
                    OnPropertyChanged();

                }
            }
        }
    
        
        private bool _isEditingDates = false;
        public bool IsEditingDates
        {
            get { return _isEditingDates; }
            set
            {
                _isEditingDates = value;
                OnPropertyChanged(nameof(IsEditingDates));
            }
        }

        public bool IsNotEditing => !IsEditing;
        private void AddCClient(object obj)
        {
            VisibilitySelectReserve = Visibility.Collapsed;
            VisibilitySelectClient = Visibility.Visible;
        }
     //   private bool isNotInSelectProduit = true ;
        private Visibility visibilitySelectChambre = Visibility.Collapsed;

      
        public Visibility VisibilitySelectChambre
        {
            get => visibilitySelectChambre;
            set
            {
                if (visibilitySelectChambre != value)
                {
                    visibilitySelectChambre = value;
                    OnPropertyChanged();

                }
            }
        }

        private Visibility visibilitySelectReserve = Visibility.Visible;
        public Visibility VisibilitySelectReserve
        {
            get => visibilitySelectReserve;
            set
            {
                if (visibilitySelectReserve != value)
                {
                    visibilitySelectReserve = value;
                    OnPropertyChanged();

                }
            }
        }
        private Visibility visibilitySelectClient = Visibility.Collapsed;
        public Visibility VisibilitySelectClient
        {
            get => visibilitySelectClient;
            set
            {
                if (visibilitySelectClient != value)
                {
                    visibilitySelectClient = value;
                    OnPropertyChanged();

                }
            }
        }

        private void CancelChambre(object obj)
        {
           VisibilitySelectChambre = Visibility.Collapsed;
            VisibilitySelectReserve= Visibility.Visible;


        }
        private void CancelClient(object obj)
        {
            VisibilitySelectClient = Visibility.Collapsed;
            VisibilitySelectReserve = Visibility.Visible;


        }
        private List<CHAMBRE> reserveDesToDelete = new List<CHAMBRE>();
        private List<CHAMBRE> ChambresNonInclusDansReserve = new List<CHAMBRE>();

        private void SelectChambre(object obj)
        {
            if (ChambresNonInclusViewSource.CurrentItem != null)
            {
                DE de = null;
                RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
                CHAMBRE chambre = (CHAMBRE)ChambresNonInclusViewSource.CurrentItem;

                // Vérification de l'existence d'ARRIVE pour cet Id_Reserve et Id_Chambre
                //var arriveExists = ds.Arrives.Any(a => a.Id_Reserve == reserve.Id_Reserve && a.Id_Chambre == chambre.Id_Chambre);
                //if (arriveExists)
                //{
                //    MessageBox.Show("Vous ne pouvez pas ajouter cette chambre à la réservation car une arrivée a été enregistrée.");
                //    return;
                //}

                int dernierIdDe = ds.GetNbDe(); // Récupérer la dernière valeur de Id_De

                de = new DE();
                de.Id_De = dernierIdDe++;
                de.Id_Reserve = reserve.Id_Reserve;
                de.Id_Chambre = chambre.Id_Chambre;
                de.Attribuee = false;
                de.CHAMBRE = chambre;

                reserve.DE.Add(de);
                ChambresNonInclusDansReserve.Remove(chambre);

                ChambresNonInclusViewSource.MoveCurrentTo(null);
                ReservesViewSource.Refresh();
                OnPropertyChanged("ChambresNonInclusViewSource");
                ChambresNonInclusViewSource.Refresh();
                VisibilitySelectChambre = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Vous devez choisir une chambre.");
            }
        }


        private void SelectClient(object obj)
        {
            if (ClientsViewSource.CurrentItem != null)
            {
                RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem; ;
                CLIENT client = (CLIENT)ClientsViewSource.CurrentItem;
                
                reserve.Id_Client = client.Id_Client;
                reserve.CLIENT = client;
                ReservesViewSource.MoveCurrentTo(reserve);
                OnPropertyChanged("ReservesViewSource");
                ReservesViewSource.Refresh();
                VisibilitySelectClient = Visibility.Collapsed;
                
            }

            else
            {
                MessageBox.Show("vous devez choisir un client");
            }
        }
        private void RemoveDe(object obj)
        {
            RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
            if (obj is DE de)
            {
                // Vérification de l'existence d'ARRIVE pour cet Id_Reserve et Id_Chambre
                //var arriveExists = ds.Arrives.Any(a => a.Id_Reserve == de.Id_Reserve && a.Id_Chambre == de.Id_Chambre);
                //if (arriveExists)
                //{
                //    MessageBox.Show("Vous ne pouvez pas supprimer cette chambre de la réservation car une arrivée a été enregistrée.");
                //    return;
                //}

                string messageBoxText = "Etes-vous certain de vouloir supprimer cette chambre de la réservation ?";
                string caption = "Vous êtes sur le point de détruire cette chambre de cette réservation";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                if (result == MessageBoxResult.OK)
                {
                    reserve.DE.Remove(de);
                    ChambresNonInclusDansReserve.Add(de.CHAMBRE);
                    OnPropertyChanged("ChambresNonInclusViewSource");
                    ChambresNonInclusViewSource.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une chambre.");
            }
        }


        public void PrepareListChambresPeutAjouterReserve(RESERVE reserve)
        {
            var chambresInclus =
                from p in ds.Chambres
                from c in reserve.DE
                where p.Id_Chambre == c.Id_Chambre
                select p;

            ChambresNonInclusDansReserve = ds.Chambres.Except(chambresInclus).ToList();
            ChambresNonInclusViewSource = CollectionViewSource.GetDefaultView(ChambresNonInclusDansReserve);
        }

        private void AddEReserve(object obj)
        {
            VisibilitySelectReserve = Visibility.Collapsed;
            VisibilitySelectChambre = Visibility.Visible;
        }

        private void RemoveEReserve(object obj)
        {
            
                RESERVE bon = (RESERVE)ReservesViewSource.CurrentItem;
                if (obj is DE)
                {
                    DE de = (DE)obj;
                    string messageBoxText = "Etes-vous certain de vouloir supprimer cet chambre de la reservation?";
                    string caption = "Vous etes sur le point de détruire un une chambre de cette reservation";
                    MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    if (result == MessageBoxResult.OK)
                    {
                        bon.DE.Remove(de);
                        ChambresNonInclusDansReserve.Add(de.CHAMBRE);
                    
                    }
                }
                else
                {
                    MessageBox.Show("Vous devez sélectionner une chambre");
                }

            

        }

        private void CancelReserve(object obj)
        {
           
            //reserveDesToDelete.Clear();
            RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
            if (ActionModeActuel == ACTIONMODE.ADD)
            {
                bool r= ds.Reserves.Remove(reserve);
                
            }
            else
            {
                ds.Reload(reserve);
               
            }
            IsEditingDates = false;
            ReservesViewSource.MoveCurrentTo(null);
            ReservesViewSource.MoveCurrentTo(reserve);
            ReservesViewSource.Refresh();
            VisibilitySelectReserve = Visibility.Visible;
            ActionModeActuel = ACTIONMODE.DISPLAY;

        }

        private void SaveReserve(object obj)
        {
            RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
            if (reserve != null)
            {
                if (reserve.Date_Debut == null || reserve.Date_Fin ==null)
                {
                    MessageBox.Show("Entrez toutes les dates");
                    return;
                }
                if (reserve.Date_Debut >= reserve.Date_Fin)
                {
                    MessageBox.Show("La date de début doit être antérieure à la date de fin");
                    return;
                }
                if (reserve.Id_Client == null)
                {
                    MessageBox.Show("Entrez les informations du clients");
                    return;
                }
                if (reserve.DE == null)
                {
                    MessageBox.Show("Veuillez choisir au moins une chambre");
                    return;
                }
                if (Conflit(reserve))
                {
                    MessageBox.Show("Cette réservation entre en conflit avec une réservation existante pour la chambre.");
                    return;
                }



                ds.Save(reserve);

                // ReservesViewSource.MoveCurrentTo(null);
                IsEditingDates = false;
                ReservesViewSource.MoveCurrentTo(reserve);
                ReservesViewSource.Refresh();
                VisibilitySelectReserve = Visibility.Visible;
            }

            ActionModeActuel = ACTIONMODE.DISPLAY;

        }
        private bool Conflit(RESERVE newReservation)
        {
            foreach (var existingReservation in ds.Reserves)
            {
                if (existingReservation.Id_Reserve == newReservation.Id_Reserve)
                    continue; 

                foreach (var existingDe in existingReservation.DE)
                {
                    foreach (var newDe in newReservation.DE)
                    {
                        if (existingDe.Id_Chambre == newDe.Id_Chambre)
                        {                           
                            if (!(newReservation.Date_Fin <= existingReservation.Date_Debut || newReservation.Date_Debut >= existingReservation.Date_Fin))
                            {
                                return true; 
                            }
                        }
                    }
                }
            }

            return false; 
        }
        private void EditReserve(object obj)
        {
            IsEditingDates = true;
            ActionModeActuel = ACTIONMODE.EDIT;
           

            if (ReservesViewSource.CurrentItem != null)
            {
                RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
                PrepareListChambresPeutAjouterReserve(reserve);
                ActionModeActuel = ACTIONMODE.EDIT;
              //  reserveDesToDelete.Clear();

            }
        }

            private void DeleteReserve(object obj)
        {
            if (ReservesViewSource.CurrentItem != null)
            {
                RESERVE reserves = (RESERVE)ReservesViewSource.CurrentItem;

                if (reserves.ARRIVE.Count > 0)
                {
                    MessageBox.Show("Vous ne pouvez pas supprimer cette réservation car ayant deja des arrivées.");
                    return;
                }


                if (reserves.ARRIVE.Count > 0 && reserves.ARRIVE.Count != reserves.DE.Count)
                {
                    MessageBox.Show("Vous ne pouvez pas supprimer cette réservation car le nombre d'arrivées est différent du nombre de départs.");
                    return;
                }

                //if (reserves.ARRIVE.Count > 0 || reserves.Date_Fin.HasValue && reserves.Date_Fin.Value.AddDays(2) >= DateTime.Now)
                //{
                //    MessageBox.Show("Vous ne pouvez plus supprimer cette réservation car la date de fin plus deux jours est supérieure ou égale à la date du jour.");
                //    return;
                //}

                string messageBoxText = "Etes-vous certain de vouloir supprimer cette reservation?";
                string caption = "Vous etes sur le point de détruire cette reservation";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.OK)
                {                                   
                    RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
                    ds.Reserves.Remove(reserve);
                    ds.DeleteReserve(reserve); 
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une reservation");
            }
        }


        private void AddReserve(object obj)
        {
           
            IsEditingDates = true;

            ActionModeActuel = ACTIONMODE.ADD;
            RESERVE reserve = new RESERVE();
           
            PrepareListChambresPeutAjouterReserve(reserve);
            ds.Reserves.Add(reserve);
            reservesViewSource.MoveCurrentTo(reserve);
            reservesViewSource.Refresh();
           

        }

    }
}
