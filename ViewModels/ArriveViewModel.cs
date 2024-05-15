using hotel24Eq5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotel24Eq5.Commands;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows;
using System.Runtime.Remoting.Contexts;
using System.Collections.ObjectModel;

namespace hotel24Eq5.ViewModels
{
    public class ArriveViewModel : BaseViewModel
    {

        #region Visibility
        private Visibility visibilityListArrive = Visibility.Visible;
        public Visibility VisibilityListArrive
        {
            get => visibilityListArrive;
            set
            {
                if (visibilityListArrive != value)
                {
                    visibilityListArrive = value;
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
        private Visibility visibilitySelectReserve = Visibility.Collapsed;
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
        #endregion
        #region RelayCommands

        public RelayCommand OkClientCommand { get; private set; }
        public RelayCommand CancelClientCommand { get; private set; }
        public RelayCommand SelectClientCommand { get; private set; }

        public RelayCommand OkReserveCommand { get; private set; }
        public RelayCommand CancelReserveCommand { get; private set; }
        public RelayCommand SelectReserveCommand { get; private set; }

        public RelayCommand OkChambreCommand { get; private set; }
        public RelayCommand CancelChambreCommand { get; private set; }
        public RelayCommand SelectChambreCommand { get; private set; }

        public RelayCommand AddArriveCommand { get; private set; }

        public RelayCommand DeleteArriveCommand { get; private set; }
        public RelayCommand EditArriveCommand { get; private set; }
        public RelayCommand SaveArriveCommand { get; private set; }
        public RelayCommand CancelArriveCommand { get; private set; }
        #endregion

        private DataServiceHotel ds = new DataServiceHotel();


        #region ViewSources
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
        
        
        ICollectionView reservesViewSource;
        public ICollectionView ReservesViewSource
        {
            get => reservesViewSource;
            set
            {
                reservesViewSource = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public ArriveViewModel() 
        {
            ArrivesViewSource = CollectionViewSource.GetDefaultView(ds.Arrives);

            ClientsViewSource = CollectionViewSource.GetDefaultView(ds.Clients);
            ReservesViewSource = CollectionViewSource.GetDefaultView(ds.Reserves);
            ChambresViewSource = CollectionViewSource.GetDefaultView(ds.Chambres);

            SelectClientCommand = new RelayCommand(SelectClient, null);
            OkClientCommand = new RelayCommand(OkClient, null);
            CancelClientCommand = new RelayCommand(CancelClient, null);

            SelectReserveCommand = new RelayCommand(SelectReserve, null);
            OkReserveCommand = new RelayCommand(OkReserve, null);
            CancelReserveCommand = new RelayCommand(CancelReserve, null);

            SelectChambreCommand = new RelayCommand(SelectChambre, null);
            OkChambreCommand = new RelayCommand(OkChambre, null);
            CancelChambreCommand = new RelayCommand(CancelChambre, null);

            AddArriveCommand = new RelayCommand(AddArrive, CanBeginEdit);
            EditArriveCommand = new RelayCommand(EditArrive, CanBeginEdit);
            DeleteArriveCommand = new RelayCommand(DeleteArrive, CanBeginEdit);
            SaveArriveCommand = new RelayCommand(SaveArrive, CanEndEdit);
            CancelArriveCommand = new RelayCommand(CancelArrive, CanEndEdit);
        }


        #region Chambre
        private void CancelChambre(object obj)
        {
            VisibilitySelectChambre = Visibility.Collapsed;
        }
        private void OkChambre(object obj)
        {
            CHAMBRE chambre = (CHAMBRE)ChambresViewSource.CurrentItem;

            ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;

            if (chambre != null)
            {
                if (arrive.CHAMBRE != null && arrive.Id_Chambre != null)
                {
                    arrive.RESERVE.DE.FirstOrDefault(de => de.Id_Chambre == arrive.Id_Chambre).Attribuee = false;
                }

                arrive.Id_Chambre = chambre.Id_Chambre;
                arrive.CHAMBRE = chambre;


                arrive.RESERVE.DE.FirstOrDefault(de => de.Id_Chambre == arrive.Id_Chambre).Attribuee = true;

                ArrivesViewSource.MoveCurrentTo(null);
                ArrivesViewSource.MoveCurrentTo(arrive);
                OnPropertyChanged("ArrivesViewSource");
                ArrivesViewSource.Refresh();

                VisibilitySelectChambre = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Aucune Chambre séléctionnée.");
            }

        }
        private void SelectChambre(object obj)
        {
            ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;
            if (VisibilitySelectClient == Visibility.Collapsed && VisibilitySelectReserve == Visibility.Collapsed && arrive.RESERVE != null)
            {
                VisibilitySelectChambre = Visibility.Visible;

                ChambresViewSource.Filter = chambre =>
                {
                    CHAMBRE c = chambre as CHAMBRE;
                    foreach (var de in arrive.RESERVE.DE)
                    {
                        if (c != null && c.Id_Chambre == de.Id_Chambre && de.Attribuee == false)
                        {
                            return true;
                        }
                    }
                    return false;
                    
                };
            }
            else if (arrive.RESERVE == null)
            {
                MessageBox.Show("Vous devez d'abord séléctionner une reserve avant de séléctionner une chambre.");
            }
            else
            {
                MessageBox.Show("Vous devez finir finir l'action actuelle ou annuler avant de séléctionner une chambre.");
            }
        }

        #endregion
        #region Reserve
        private void CancelReserve(object obj)
        {
            VisibilitySelectReserve = Visibility.Collapsed;
        }
        private void OkReserve(object obj)
        {
            RESERVE reserve = (RESERVE)ReservesViewSource.CurrentItem;
            ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;

            if (reserve != null)
            {
                arrive.Id_Reserve = reserve.Id_Reserve;
                arrive.RESERVE = reserve;

                arrive.RESERVE.CLIENT = reserve.CLIENT;

                ArrivesViewSource.MoveCurrentTo(arrive);
                OnPropertyChanged("ArrivesViewSource");
                ArrivesViewSource.Refresh();

                ChambresViewSource.MoveCurrentTo(null);
                OnPropertyChanged("ChambresViewSource");
                ChambresViewSource.Refresh();

                VisibilitySelectReserve = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Aucune Reserve séléctionnée.");
            }
        }
        private void SelectReserve(object obj)
        {
            if (ActionModeActuel == ACTIONMODE.EDIT)
            {
                MessageBox.Show("Vousne pouvez pas modifier la réservation d'une arrivée.");
            }
            else if (VisibilitySelectClient == Visibility.Collapsed && VisibilitySelectChambre == Visibility.Collapsed)
            {
                VisibilitySelectReserve = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Vous devez finir finir l'action actuelle ou annuler avant de séléctionner une réservation.");
            }
        }
        #endregion
        #region Client
        private void CancelClient(object obj)
        {
            VisibilitySelectClient = Visibility.Collapsed;
        }
        private void OkClient(object obj)
        {
            CLIENT client = (CLIENT)ClientsViewSource.CurrentItem;
            ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;

            if (client != null)
            { 
                arrive.Id_Client = client.Id_Client;
                arrive.CLIENT = client;

                ArrivesViewSource.MoveCurrentTo(arrive);
                OnPropertyChanged("ArrivesViewSource");
                ArrivesViewSource.Refresh();

                VisibilitySelectClient = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Aucun Client séléctionné.");
            }
        }
        private void SelectClient(object obj)
        {
            if (ActionModeActuel == ACTIONMODE.EDIT)
            {
                MessageBox.Show("Vousne pouvez pas modifier le client d'une arrivée.");
            }
            else if (VisibilitySelectReserve == Visibility.Collapsed && VisibilitySelectChambre == Visibility.Collapsed)
            {
                VisibilitySelectClient = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Vous devez finir l'action actuelle ou annuler avant de séléctionner un client.");
            }
        }
        #endregion

        #region ActionMode
        private bool CanEndEdit(object obj)
        {
            return ActionModeActuel != ACTIONMODE.DISPLAY;
        }
        private bool CanBeginEdit(object obj)
        {
            return ActionModeActuel == ACTIONMODE.DISPLAY;
        }
        #endregion


        #region Actions
        private void AddArrive(object obj)
        {
            ActionModeActuel = ACTIONMODE.ADD;
            VisibilityListArrive = Visibility.Collapsed;

            ARRIVE arrive = new ARRIVE();

            ds.Arrives.Add(arrive);

            ArrivesViewSource.MoveCurrentTo(arrive);
            OnPropertyChanged("ArrivesViewSource");
            ArrivesViewSource.Refresh();
        }
        private void CancelArrive(object obj)
        {
            VisibilitySelectClient = Visibility.Collapsed;
            VisibilitySelectReserve = Visibility.Collapsed;
            VisibilitySelectChambre = Visibility.Collapsed;
            VisibilityListArrive = Visibility.Visible;

            ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;


            if (ActionModeActuel == ACTIONMODE.ADD)
            {
                if (arrive.Id_Chambre != null && arrive.RESERVE.DE != null)
                {
                    arrive.RESERVE.DE.FirstOrDefault(de => de.Id_Chambre == arrive.Id_Chambre).Attribuee = false;
                }
                ds.Arrives.Remove(arrive);
                ArrivesViewSource.Refresh();
            }
            else
            {
                ds.LoadAll();

                ArrivesViewSource = CollectionViewSource.GetDefaultView(ds.Arrives);
                ArrivesViewSource.Refresh();
                ArrivesViewSource.MoveCurrentTo(null);
                ArrivesViewSource.MoveCurrentToLast();
                OnPropertyChanged("ArrivesViewSource");
                ArrivesViewSource.Refresh();
            }
            ActionModeActuel = ACTIONMODE.DISPLAY;
            OnPropertyChanged("ArrivesViewSource");
            ArrivesViewSource.Refresh();
        }

        private void SaveArrive(object obj)
        {
            ARRIVE arrive = (ARRIVE)arrivesViewSource.CurrentItem;

            if (arrive != null)
            {
                if (arrive.CLIENT == null || arrive.Id_Client == null)
                {
                    MessageBox.Show("Séléctionnez un client");
                    return;

                }
                if (arrive.CHAMBRE == null || arrive.Id_Chambre == null)
                {
                    MessageBox.Show("Séléctionnez une chambre");
                    return;

                }
                if (arrive.RESERVE == null ||arrive.Id_Reserve == null)
                {
                    MessageBox.Show("Séléctionnez une réservation");
                    return;

                }
                if (DateTime.Today.CompareTo(arrive.Date_Arrive) != 0)
                {
                    MessageBox.Show("La date d'arrivée doit être celle d'aujourd'hui");
                    return;

                }
                if (arrive.Date_Arrive < arrive.RESERVE.Date_Debut)
                {
                    MessageBox.Show("La date d'arrivée ne peut pas être antérieure à celle du début de la réservation");
                    return;

                }
                if (arrive.Date_Arrive > arrive.RESERVE.Date_Fin)
                {
                    MessageBox.Show("La date d'arrivée ne peut pas être postérieure à celle de la fin de la réservation");
                    return;

                }
                if (arrive.Recu_Par == String.Empty || arrive.Recu_Par == null)
                {
                    MessageBox.Show("Vous devez remplir la case Reçu Par");
                    return;

                }

                ds.Save(arrive);
            }

            VisibilityListArrive = Visibility.Visible;
            ActionModeActuel = ACTIONMODE.DISPLAY;
            
            ArrivesViewSource.MoveCurrentTo(null);
            ArrivesViewSource.MoveCurrentTo(arrive);
            OnPropertyChanged("ArrivesViewSource");
            ArrivesViewSource.Refresh();
        }

        private void DeleteArrive(object obj)
        {
            if (ArrivesViewSource.CurrentItem != null)
            {
                string messageBoxText = "Etes-vous certain de vouloir supprimer cette arrivé?";
                string caption = "Vous etes sur le point de détruire une arrivé";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.OK)
                {                                   
                    ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;

                    ds.Arrives.Remove(arrive);
                    ds.Delete(arrive);


                    ds.LoadAll();
                    ArrivesViewSource = CollectionViewSource.GetDefaultView(ds.Arrives);
                    ClientsViewSource = CollectionViewSource.GetDefaultView(ds.Clients);
                    ReservesViewSource = CollectionViewSource.GetDefaultView(ds.Reserves);
                    ChambresViewSource = CollectionViewSource.GetDefaultView(ds.Chambres);

                    ArrivesViewSource.MoveCurrentToLast();
                    OnPropertyChanged("ArrivesViewSource");
                    ArrivesViewSource.Refresh();

                    MessageBox.Show("L'arrivée a bel et bien été supprimé");
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une arrivé");
            }
            ActionModeActuel = ACTIONMODE.DISPLAY;
        }

        private void EditArrive(object obj)
        {
            ActionModeActuel = ACTIONMODE.EDIT;
            VisibilityListArrive = Visibility.Collapsed;

            if (ArrivesViewSource.CurrentItem != null)
            {
                ARRIVE arrive = (ARRIVE)ArrivesViewSource.CurrentItem;

                ActionModeActuel = ACTIONMODE.EDIT;

            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une arrivée");
            }
           
        }
        #endregion
    }
}
