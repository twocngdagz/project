   M S B A M L   `   `   `  ����C  ?Mashisho, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MashishoMashisho  5      #clr-namespace:MashishoMashisho  5      #    Mashisho.FilterListDetailView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -
ViewModelsclr-namespace:Mashisho     5      %myclr-namespace:Mashisho     5        � 	Resources  5
       �� Source�� 5   
   $ MyDict.xaml?�6   %    64   .��5      � ���	#FFE0B86C � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��208* �6   �� 5      $
��173* �6   .+�5   
   �� $��0��6   ��6&   �� *MyObjectFilter+&   System.Windows.Controls.DataGrid  5   
   -   5        $��	dataGrid1��5       $��1��6    �� DisplayMemberPath$
 Name��6Y   $��7q�6r      CanUserReorderColumns .    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     SelectionMode$ Single=�62  	   
IsReadOnly	 . $
 �� IsSynchronizedWithCurrentItem$

 TrueY�6[  ��61   ��  �� Path$ 
AllObjects� �� SelectedItem 6}   �� $ MyPartialObject.SetObject� �� Mode$ OneWayToSource=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+  5      $
 Name��6%    63   �� *Name+  5      $ Other Occupiers��6%    6>   �� *OtherOccupiers+  5      $ 	Directory��6%    68   �� *	Directory+5      �� 5   
   $��2��6   ��6&   �� $ MyPartialObject�5       5!      