   M S B A M L   `   `   `  ����  Mashisho    Mashisho.LandlordDetail   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � 
�������  � ColumnDefinitions  5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��228* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Id��6   $��Center=�6   $��3q�68   $��Right=�6C   �� 5   
   $
��Name��6   $��1��6   $��Center=�6,   $��3q�6G   $��Right=�6R   �� 5   
   $��	Address 1��6   $��2��6%   $��Center=�62   $��3q�6M   $��Right=�6X   �� 5   
   $��	Address 2��6   $��3��6%   $��Center=�62   $��3q�6M   $��Right=�6X   �� 5   
   -   5        $��	Directory��5      $��4��6%   $��Center=�62   $��3q�6M   $��Right=�6X   
������ 6�   �� 5   
   $��1��6    �� 
IsReadOnly . $��Center=�6M   $��3q�6h   $	��200��6s   ��64   ��  �� Path$ Id�6s   �� 5   
   $��1��6   $��1��6"   $��Center=�6J   $��3q�6e   $	��200��6p   ��6/   �� $
 Name�6p   �� 5   
   $��1��6   $��2��6"   $��Center=�6N   $��3q�6i   $	��200��6t   ��6/   �� $ Address1�6t   �� 5   
   $��1��6   $��3��6"   $��Center=�6N   $��3q�6i   $	��200��6t   ��6/   �� $ Address2�6t   �� 5   
   $��1��6   $��4��6"   $��Center=�6O   $��3q�6j   $	��200��6u   ��6/   �� $ 	Directory�6u   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dataGrid1��5   ?      AutoGenerateColumns .  $��2��6/   $��5��6w   $��7q�6�   $��7q�6�   $��3q�6�   ��6P   �� $ 	Tenancies�   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Rental Unit��6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� $ 
RentalUnit�6I     5"      $ 
Start Date��6a    6%   �� $ 	StartDate� �� StringFormat$ 
yyyy-MM-dd��6a     5#      $ End Date��6_    6%   �� $ EndDate�$ 
yyyy-MM-dd��6_     5$      $
 Rent��6V    6%   �� $ RentPCM�$ C��6V     5%      $ Deposit��6V    6%   �� $ Deposit�$ C��6V   5'      �� 5(   
   -   5        $��btnClear��5(   :   $��5��6   $��Clear��6   $��23��6.   $��75��6J   $	��Top=�6U   $��4q�6m   6x   �� 5)   
   -   5        $��stackPanel1��5)   "   $��5��6   $��3��65   	 �� Orientation$	 
Horizontal=�6I   $��RightToLeft=�6b   .+�5*      �� -   5        $��	btnUpdate��5*      $��Update��6&   $��23��67   $��75��6C   $��4q�6f   $��Right=�6q   $��Bottom=�6�   �� 5+      -   5        $��	btnDelete��5+      $��Delete��6&   $��23��67   $��75��6C   $��4q�6N   $��Right=�6Y   $��Bottom=�6u   6�   �� 5,      -   5        $��btnAdd��5,      $	��Add��6#   $��23��61   $��75��6=   $��4q�6H   $��Right=�6S   $��Bottom=�6o   6�   5-      5.      5/      