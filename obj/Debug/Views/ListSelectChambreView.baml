   M S B A M L   `   `   `  ����E  Ahotel24Eq5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:hotel24Eq5.Viewshotel24Eq5.Views  5      ,    &hotel24Eq5.Views.ListSelectChambreView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:hotel24Eq5.Views     5      
�������.��5	      � 
������  � ColumnDefinitions  5
   
   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   .+�5   
   �� -   5         ��Name$ cHAMBREListView��5      $��0��6,   $��2��69   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6M    �� SelectionMode$ Single=�6�   $��0,0,0,0q�6�   ��6r   �� *ChambresViewSource+ �� View 5      �� 5      .P�5      �� -   5        code_LocalisationColumn��$��Code Localisation��5   F    �� Width$ 80��6a    �� CellTemplate 5      �� 5      .R�5   "   �� $��
6,-1,-6,-1q�6,   ��6@   �� *Code_Localisation+5      5      �� 5      -   5        code_TypeChambreColumn��$��Code Type Chambre��5   E   $ 80��6`    5      �� 5      .R�5   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Code_TypeChambre+5       5"      �� 5#      -   5        etageColumn��$��Etage��5#   :   $ 80��6I    5$      �� 5%      .R�5&   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Etage+5'      5)      �� 5*      -   5        
etatColumn��$
��Etat��5*   9   $ 80��6G    5+      �� 5,      .R�5-   "   �� $��-6,-1,-6,-1q�6B   ��6,   �� *Etat+6B   5.      50      �� 51      -   5        id_ChambreColumn2��$��
Id Chambre��51   @   $ 80��6T    52      �� 53      .R�54   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Id_Chambre+55      57      �� 58      -   5        
memoColumn��$
��Memo��58   9   $ 80��6G    59      �� 5:      .R�5;   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Memo+5<      5>      �� 5?      -   5        
prixColumn��$
��Prix��5?   9   $ 80��6G    5@      �� 5A      .R�5B   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Prix+5C      5E      5F      5H      �� 5I   
   $��1��6   $��0��6   $��Ok��6.   $
��auto��6;   ��6H   �� *OkChambreCommand+6o   �� 5J   
   $��1��6   $��1��6   $��Cancel��6.   $
��auto��6?   ��6L   �� *CancelChambreCommand+6w   5K      5L      