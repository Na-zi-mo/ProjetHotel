   M S B A M L   `   `   `  ����E  Ahotel24Eq5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:hotel24Eq5.Viewshotel24Eq5.Views  5      +    %hotel24Eq5.Views.ListSelectClientView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:hotel24Eq5.Views     5      
�������.��5	      � 
����  �  � ColumnDefinitions  5
   
   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   .+�5   
   �� -   5         ��Name$ cLIENTListView��5   4   $��0��6   $��2��6    $ �� IsSynchronizedWithCurrentItem$
 TrueY�6L    �� SelectionMode$ Single=�6�   ��6q   �� *ClientsViewSource+ �� View 5      �� 5      .P�5      �� -   5        adresseColumn��$��Adresse��5   <    �� Width$ 80��6M    �� CellTemplate 5      �� 5      .R�5      �� $��
6,-1,-6,-1q�6$   ��68   �� *	Adresse+5      5      �� 5      -   5        date_ExpColumn��$��Date Exp��5   5   $ 80��6G    5      �� 5      .R�5      �� $��-6,-1,-6,-1q�6!   ��66   �� *
Date_Exp+ �� StringFormat$ 
dd-MM-yyyy��5       5"      �� 5#      -   5        id_ClientColumn1��$��	Id Client��5#   7   $ 80��6J    5$      �� 5%      .R�5&      �� $��-6,-1,-6,-1q�6$   ��69   �� *	Id_Client+5'      5)      �� 5*      -   5        	nomColumn��$	��Nom��5*   8   $ 80��6E    5+      �� 5,      .R�5-   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Nom+5.      50      �� 51      -   5        num_CarteColumn��$��	Num Carte��51   >   $ 80��6Q    52      �� 53      .R�54   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *	Num_Carte+55      57      �� 58      -   5        solde_DuColumn��$��Solde Du��58   =   $ 80��6O    59      �� 5:      .R�5;   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Solde_Du+5<      5>      �� 5?      -   5        telephoneColumn��$��	Telephone��5?   >   $ 80��6Q    5@      �� 5A      .R�5B   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *	Telephone+5C      5E      �� 5F      -	   5        type_CarteColumn��$��
Type Carte��5F   ?   $ 80��6S    5G      �� 5H      .R�5I   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Type_Carte+5J      5L      5M      5O      �� 5P   
   $��1��6   $��0��6   $��Ok��6.   $
��auto��6;   ��6H   �� *OkClientCommand+6n   �� 5Q   
   $��1��6   $��1��6   $��Cancel��6.   $
��auto��6?   ��6L   �� *CancelClientCommand+6v   5R      5S      