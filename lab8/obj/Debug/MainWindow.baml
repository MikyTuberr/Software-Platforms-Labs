   M S B A M L   `   `   `  ����?  ;PT_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PT_8PT_8  5   	       PT_8.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:PT_8     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� $��0��6   .@�5      m� $��_File��6   .@�5      m� -   5        $��_Open��5      6*   m� 5      -   5        $��_Exit��5      6*   5      5      �� 5   
   $��1��6   ��5      �� 5      .R�5      �  � ColumnDefinitions 5       �� 5!      $
��Auto �6/   �� 5"      $��* �6/   5$      5%      .@�5'      �� -   5         ��Name$ txtFileAttributes��5'      $��0��63   $��5q�6C   5(      � 5+   
   $��2��6    5,      �� 5-      $
��Auto �6#   �� 5/      $��* �6#   .+�54      R� -   5         R�Name$ treeView��54      $��0��6)   $��10q�69   $
��Left=�6E   $	��200��6`   �� 57      $��1��6   $
��Auto=�6+   $
��Auto=�6P   .��58      �� -   5         ��Name$ txtFileContent��58      $��10q�62    �� TextWrapping$
 Wrap=�6>    �� AcceptsReturn .  �� 
IsReadOnly . 6g   59      5:      5;      5<      