using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using hotel24Eq5.Commands;
using hotel24Eq5.Models;

namespace hotel24Eq5.ViewModels
{
    public class ChambreViewModel : BaseViewModel
    {
		#region RelayCommand

		public RelayCommand AddChambreCommand { get; private set; }

		public RelayCommand EditChambreCommand { get; private set; }

		public RelayCommand DeleteChambreCommand { get; private set; }

		public RelayCommand SaveChambreCommand { get; private set; }

		public RelayCommand CancelChambreCommand { get; private set; }

		public RelayCommand AddEnfantCommand { get; private set; }

		public RelayCommand RemoveEnfantCommand { get; private set; }

		public RelayCommand SelectCommoditeCommand { get; private set; }

		public RelayCommand CancelSelectCommoditeCommand { get; private set; }

		#endregion

		private DataServiceHotel ds = new DataServiceHotel();

		private List<COMMODITE> commoditesNonInclusChambre;
		public List<COMMODITE> CommoditesNonInclusChambre
		{
			get => commoditesNonInclusChambre;
			set
			{
				commoditesNonInclusChambre = value;
				OnPropertyChanged();
			}
		}



		private List<AYANT> chambreAyantsToDelete = new List<AYANT>();

		private bool isEnabledTextBox = false;
		public bool IsEnabledTextBox
		{
			get => isEnabledTextBox;
			set
			{
				isEnabledTextBox = value;
				OnPropertyChanged();
			}
		}

		private bool isEnabledEtat = false;
		public bool IsEnabledEtat
		{
			get => isEnabledEtat;
			set
			{
				isEnabledEtat = value;
				OnPropertyChanged();
			}
		}

		private bool isEnabledIdChambre = false;
		public bool IsEnabledIdChambre
		{
			get => isEnabledIdChambre;
			set
			{
				isEnabledIdChambre = value;
				OnPropertyChanged();
			}
		}

		ICollectionView commoditesNonInclusChambreViewSource;

		public ICollectionView CommoditesNonInclusChambreViewSource
		{
			get => commoditesNonInclusChambreViewSource;
			set
			{
				commoditesNonInclusChambreViewSource = value;
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

		ICollectionView typesChambresViewSource;

		public ICollectionView TypesChambresViewSource
		{
			get => typesChambresViewSource;
			set
			{
				typesChambresViewSource = value;
				OnPropertyChanged();

			}
		}

		ICollectionView localisationsViewSource;

		public ICollectionView LocalisationsViewSource
		{
			get => localisationsViewSource;
			set
			{
				localisationsViewSource = value;
				OnPropertyChanged();
			}
		}

		ICollectionView ayantsViewSource;
		public ICollectionView AyantsViewSource
		{
			get => ayantsViewSource;
			set
			{
				ayantsViewSource = value;
				OnPropertyChanged();
			}
		}

		private Visibility visibilitySelectChambre = Visibility.Visible;
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

		private Visibility visibilitySelectCommodite = Visibility.Collapsed;
		public Visibility VisibilitySelectCommodite
		{
			get => visibilitySelectCommodite;
			set
			{
				if (visibilitySelectCommodite != value)
				{
					visibilitySelectCommodite = value;
					OnPropertyChanged();
				}
			}
		}

		public ChambreViewModel()
		{
			CancelSelectCommoditeCommand = new RelayCommand(CancelSelectCommodite, null);

			SelectCommoditeCommand = new RelayCommand(SelectCommodite, null);

			AyantsViewSource = CollectionViewSource.GetDefaultView(ds.Ayants);

			LocalisationsViewSource = CollectionViewSource.GetDefaultView(ds.Localisations);

			TypesChambresViewSource = CollectionViewSource.GetDefaultView(ds.TypesChambres);

			ChambresViewSource = CollectionViewSource.GetDefaultView(ds.Chambres);

			AddChambreCommand = new RelayCommand(AddChambre, CanBeginEdit);

			EditChambreCommand = new RelayCommand(EditChambre, CanBeginEdit);

			DeleteChambreCommand = new RelayCommand(DeleteChambre, CanBeginEdit);

			SaveChambreCommand = new RelayCommand(SaveChambre, CanEndEdit);

			CancelChambreCommand = new RelayCommand(CancelChambre, CanEndEdit);

			AddEnfantCommand = new RelayCommand(AddEnfant, CanEndEdit);

			RemoveEnfantCommand = new RelayCommand(RemoveEnfant, CanEndEdit);
		}		

		private void SelectCommodite(object obj)
		{
			if (CommoditesNonInclusChambreViewSource.CurrentItem != null)
			{
				AYANT a = null;

				CHAMBRE ch = (CHAMBRE)ChambresViewSource.CurrentItem;

				COMMODITE co = (COMMODITE)CommoditesNonInclusChambreViewSource.CurrentItem;

				a = chambreAyantsToDelete.FirstOrDefault(ay => ay.COMMODITE.Code_Commodite == co.Code_Commodite && ay.Id_Chambre == ch.Id_Chambre);

				if(a != null)
				{
					chambreAyantsToDelete.Remove(a);
				}

				a = new AYANT();				
				a.Id_Chambre = ch.Id_Chambre;
				a.Code_Commodite = co.Code_Commodite;

				a.COMMODITE = co;

				ch.AYANT.Add(a);
				
				CommoditesNonInclusChambre.Remove(co);
				CommoditesNonInclusChambreViewSource.Refresh();
			}
			else
			{
				MessageBox.Show("Vous devez saisir une Commodite");
			}
		}

		private void CancelSelectCommodite(object obj)
		{
			VisibilitySelectCommodite = Visibility.Collapsed;
			VisibilitySelectChambre = Visibility.Visible;
		}		

		public void PrepareListCommoditesPeutAjouterChambre(CHAMBRE c)
		{
			//var CommoditesInclus =
			//from co in ds.Commodites
			//from a in c.AYANT
			//where co.Code_Commodite == a.Code_Commodite
			//select co;

			//commoditesNonInclusChambre = ds.Commodites.Except(CommoditesInclus).ToList();			

			CommoditesNonInclusChambre = ds.GetCommoditesNonInclusChambre(c);
			CommoditesNonInclusChambreViewSource = CollectionViewSource.GetDefaultView(CommoditesNonInclusChambre);			
		}

		private void AddChambre(object obj)
		{
			IsEnabledEtat = true;
			IsEnabledIdChambre = true;
			ActionModeActuel = ACTIONMODE.ADD;
			chambreAyantsToDelete.Clear();

			CHAMBRE chambre = new CHAMBRE();

			ds.Chambres.Add(chambre);

			ChambresViewSource.MoveCurrentTo(chambre);
			OnPropertyChanged("ChambresViewSource");
			ChambresViewSource.Refresh();
		}

		private void EditChambre(object obj)
		{
			CHAMBRE ch = (CHAMBRE)ChambresViewSource.CurrentItem;
			if (ChambresViewSource != null)
			{
				ActionModeActuel = ACTIONMODE.EDIT;
				chambreAyantsToDelete.Clear();
				PrepareListCommoditesPeutAjouterChambre(ch);
	
			}
			else
			{
				MessageBox.Show("Vous Devez Selectionner une chambre");
			}
		}

		private void DeleteChambre(object obj)
		{
			CHAMBRE chambre = (CHAMBRE)ChambresViewSource.CurrentItem;
			chambreAyantsToDelete.Clear();
			if (ChambresViewSource.CurrentItem != null)
			{				
				string messageBoxText = "Etes-vous certain de vouloir supprimer cette chambre?";
				string caption = "Vous etes sur le point de détruire une chambre";
				MessageBoxButton button = MessageBoxButton.OKCancel;
				MessageBoxImage icon = MessageBoxImage.Warning;

				MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

				if (result == MessageBoxResult.OK)
				{
					ds.Chambres.Remove(chambre);
					ds.DeleteChambre(chambre);
				}							
			}
			else
			{
				MessageBox.Show("Vous devez selectionner une chambre");
			}
			ActionModeActuel = ACTIONMODE.DISPLAY;
		}

		private void SaveChambre(object obj)
		{
			CHAMBRE chambre = (CHAMBRE)ChambresViewSource.CurrentItem;

			if (chambre.Code_Localisation == null)
			{
				MessageBox.Show("Selectionner une localisation");
				return;
			}
			if (chambre.Code_TypeChambre == null)
			{
				MessageBox.Show("Selectionner une type chambre");
				return;
			}
			if (chambre.Etage == null || chambre.Etage <= 0 || chambre.Etage >= 100)
			{
				MessageBox.Show("L'etage ne peut pas etre 0 ni negatif ni plus grand que 100");
				return;
			}
			if(chambre.Etat == null)
			{
				MessageBox.Show("Selectionnez un etat");
				return;
			}
			if (chambre.Memo == null)
			{
				MessageBox.Show("Entrez un memo");
				return;
			}
			if (chambre.Prix == null || chambre.Prix <= 0 || chambre.Prix >= 1000)
			{
				MessageBox.Show("Le prix ne peux pas etre 0 ni negatif ni plus grand que 1000");
				return;
			}
			if (chambre.AYANT.Count == 0)
			{
				MessageBox.Show("Selectionez une commodite");
				return;
			}

			if (chambre != null)
			{				
				foreach(var a in chambreAyantsToDelete)
				{
					ds.DeleteAyantChambre(a);
				}
				chambreAyantsToDelete.Clear();

				ds.Save(chambre);

				ActionModeActuel = ACTIONMODE.DISPLAY;
				VisibilitySelectCommodite = Visibility.Collapsed;
				VisibilitySelectChambre = Visibility.Visible;

				ChambresViewSource.Refresh();
				OnPropertyChanged("ChambresViewSource");
				ChambresViewSource.MoveCurrentTo(null);
				ChambresViewSource.MoveCurrentTo(chambre);
			}
			else
			{

			}
			
		}

		private void CancelChambre(object obj)
		{
			CHAMBRE chambre = (CHAMBRE)ChambresViewSource.CurrentItem;

			chambreAyantsToDelete.Clear();

			chambre.AYANT.Clear();

			if (chambre.Id_Chambre == -1)
			{
				bool r = ds.Chambres.Remove(chambre);

				ChambresViewSource.Refresh();
			}
			else
			{
				ds.Reload(chambre);

				chambresViewSource.MoveCurrentTo(chambre);
			}

			ActionModeActuel = ACTIONMODE.DISPLAY;
			VisibilitySelectCommodite = Visibility.Collapsed;
			VisibilitySelectChambre = Visibility.Visible;
			OnPropertyChanged("ChmbresViewSource");
			ChambresViewSource.Refresh();
		}

		private void AddEnfant(object obj)
		{			
			CHAMBRE chambre = (CHAMBRE)ChambresViewSource.CurrentItem;
			PrepareListCommoditesPeutAjouterChambre(chambre);

			OnPropertyChanged("CommoditesNonInclusChambreViewSource");

			VisibilitySelectCommodite = Visibility.Visible;
			VisibilitySelectChambre = Visibility.Collapsed;
		}

		private void RemoveEnfant(object obj)
		{
			CHAMBRE ch = (CHAMBRE)ChambresViewSource.CurrentItem;								
			
			if (obj is AYANT)
			{
				if (ch.AYANT.Count == 1)
				{
					MessageBox.Show("Vous devez garder au moins une commodite");
					return;
				}
				AYANT a = (AYANT)obj;

				string messageBoxText = "Etes-vous certain de vouloir supprimer cette commodite de la chambre?";
				string caption = "Vous etes sur le point de detruire un produit de cette chambre";

				MessageBoxButton button = MessageBoxButton.OKCancel;
				MessageBoxImage icon = MessageBoxImage.Warning;
				MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
				if (result == MessageBoxResult.OK)
				{
					if(ActionModeActuel == ACTIONMODE.EDIT)
					{
						chambreAyantsToDelete.Add(a);
					}
					ch.AYANT.Remove(a);
					CommoditesNonInclusChambre.Add(a.COMMODITE);
				}
			}			
			else
			{
				MessageBox.Show("Vous devez selectionner une commodite");
			}
		}		
	}
}
