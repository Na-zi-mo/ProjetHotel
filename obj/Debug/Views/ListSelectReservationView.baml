   M S B A M L   `   `   `  ����E  Ahotel24Eq5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:hotel24Eq5.Viewshotel24Eq5.Views  5      0    *hotel24Eq5.Views.ListSelectReservationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:hotel24Eq5.Views     5      
�������.��5	      � 
���� ��  � ColumnDefinitions  5
   
   �� 5      $
��auto �6   6.   �� 5      $
��auto �6   6.    � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   .+�5   
   �� -   5         ��Name$ rESERVEListView��5   4   $��0��6   $��2��6    $ �� IsSynchronizedWithCurrentItem$
 TrueY�6M    �� SelectionMode$ Single=�6�   ��6r   �� *ReservesViewSource+ �� View 5      �� 5      .P�5      �� -   5        confirmeColumn��$��Confirme��5   =    �� Width$ 80��6O    �� CellTemplate 5      �� 5      .R�5   "   �� $��
6,-1,-6,-1q�6J    d� 	IsChecked 6+   �� *
Confirme+6J   5      5      �� 5      -   5        date_DebutColumn��$��
Date Debut��5   ?   $ 80��6S    5      �� 5      .R�5   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Date_Debut+	 �� StringFormat$	 
dd-MM-yyyy��5       5"      �� 5#      -   5        date_FinColumn��$��Date Fin��5#   =   $ 80��6O    5$      �� 5%      .R�5&   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Date_Fin+$	 
dd-MM-yyyy��5'      5)      �� 5*      -   5        date_ReserveColumn��$��Date Reserve��5*   A   $ 80��6W    5+      �� 5,      .R�5-   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *Date_Reserve+$	 
dd-MM-yyyy��5.      50      �� 51      -   5        id_ClientColumn2��$��	Id Client��51   ?   $ 80��6R    52      �� 53      .R�54   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *	Id_Client+55      57      �� 58      -   5        id_ReserveColumn2��$��
Id Reserve��58   @   $ 80��6T    59      �� 5:      .R�5;   "   �� $��-6,-1,-6,-1q�6,   ��6A   �� *
Id_Reserve+5<      5>      5?      5A      �� 5B   
   $��1��6   $��0��6   $��Ok��6.   $
��auto��6;   ��6H   �� *OkReserveCommand+6o   �� 5C   
   $��1��6   $��1��6   $��Cancel��6.   $
��auto��6?   ��6L   �� *CancelReserveCommand+6w   5D      5E      