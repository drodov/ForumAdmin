   M S B A M L   `   `   `  ����   ForumController    ForumController.ShowPosts   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  	ShowPosts��$	��500��$	��600��.��� .+�&   System.Windows.Controls.DataGrid  -   $��	dataGrid1��   AutoGenerateColumns .  $
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$
��Auto��   CanUserSortColumns .    CanUserDeleteRows .     CanUserResizeColumns .     MinRowHeight$ 30Q�   CanUserAddRows .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Post��	   
IsReadOnly	 .  1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *Text+  $
 Date��	 . 
 �� *	DateAdded+