   M S B A M L   `   `   `  ����  ForumController    ForumController.ShowPosts   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	ShowPosts��5   	   $	��500��6   $	��600��6(   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	dataGrid1��5   k      AutoGenerateColumns .  $
��Auto��6/   $
��Left=�6=   $��	30,30,0,0q�6X   $	��Top=�6|   $
��Auto��6�      CanUserSortColumns .    CanUserDeleteRows .     CanUserResizeColumns .     MinRowHeight$ 30Q�6�      CanUserAddRows .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Post��6>   	   
IsReadOnly	 .  1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6%   �� *Text+6L     5	      $
 Date��6C   	 . 
 6%   �� *	DateAdded+6Q   5      5      5      