   M S B A M L   `   `   `  ����  Mashisho    Mashisho.TenantDetail   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � 
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
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��label1��5      $��Id��6   $��Center=�6+   $��3q�6F   $��Right=�6Q   �� 5   
   -   5        $��label2��5      $
��Name��6   $��1��6-   $��Center=�6:   $��3q�6U   $��Right=�6`   �� 5   
   -   5        $��label3��5   *   $��Other Occupiers��6   $��2��68   $��Center=�6E   $��3q�6`   $��Right=�6k   �� 5   
   -   5        $��label4��5   %   $��	Directory��6   $��3��63   $��Center=�6@   $��3q�6[   $��Right=�6f   
������ 6�   �� 5   
   -   5        $��textBox1��5   "   $��1��6    �� 
IsReadOnly . $��Center=�6]   $��3q�6x   $	��200��6�   ��6D   ��  �� Path$ Id�6�   �� 5   
   -   5        $��textBox2��5      $��1��6"   $��1��62   $��Center=�6Z   $��3q�6u   $	��200��6�   ��6?   �� $
 Name�6�   �� 5   
   -   5        $��textBox3��5      $��1��6"   $��2��62   $��Center=�6d   $��3q�6   $	��200��6�   ��6?   �� $ OtherOccupiers�6�   �� 5   
   -   5        $��textBox4��5      $��1��6"   $��3��62   $��Center=�6_   $��3q�6z   $	��200��6�   ��6?   �� $ 	Directory�6�   &   System.Windows.Controls.DataGrid  5   
   -	   5        $��	dataGrid1��5   ?      AutoGenerateColumns .  $��2��6/   $��4��6w   $��7q�6�   $��7q�6�   $��3q�6�   ��6P   �� $ 	Tenancies�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Rental Unit��6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� $ 
RentalUnit�6I     5      $ 
Start Date��6a    6%   �� $ 	StartDate� �� StringFormat$ 
yyyy-MM-dd��6a     5       $ End Date��6_    6%   �� $ EndDate�$ 
yyyy-MM-dd��6_     5!      $
 Rent��6V    6%   �� $ RentPCM�$ C��6V     5"      $ Deposit��6V    6%   �� $ Deposit�$ C��6V   5$      �� 5%   
   -
   5        $��btnClear��5%   :   $��4��6   $��Clear��6   $��23��6.   $��75��6J   $	��Top=�6U   $��4q�6m   6x   �� 5&   
   -   5        $��stackPanel1��5&   "   $��4��6   $��3��65   	 �� Orientation$	 
Horizontal=�6I   $��RightToLeft=�6b   .+�5'      �� -   5        $��	btnUpdate��5'      $��Update��6&   $��23��67   $��75��6C   $��4q�6f   $��Right=�6q   $��Bottom=�6�   �� 5(      -   5        $��	btnDelete��5(      $��Delete��6&   $��23��67   $��75��6C   $��4q�6N   $��Right=�6Y   $��Bottom=�6u   6�   �� 5)      -   5        $��btnAdd��5)      $	��Add��6#   $��23��61   $��75��6=   $��4q�6H   $��Right=�6S   $��Bottom=�6o   6�   5*      5+      5,      