   M S B A M L   `   `   `  ����B  >CRUDB32, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDB32CRUDB32  5      !    CRUDB32.UserControlSupplier   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:CRUDB32     5      .��5	      � ���	#FFA6ECE2$��Stretch=�6#   $��Stretch=�6A   .+�5
   
   �� -   5          ��Name$  TxtName��5
      $
��Left=�6#   $��23��6>   $��
22,185,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��278��6�   �� 5   
   -   5         ��Name$ 	BtnSubmit��5      $��Insert��6$   $��Right=�65   $��0,328,871,0q�6Q   $	��Top=�6f   $	��108��6~   6�   �� 5   
   -   5        $  TxtId��5      $
��Left=�6!   $��23��6<   $��
22,126,0,0q�6H   $
 Wrap=�6\   $	��Top=�6p   $	��145��6�    �� 
IsReadOnly . 6�   �� 5   
   $��ID��6   $
��Left=�6   $	��Top=�68   $��	22,96,0,0q�6P    J� RenderTransformOrigin$ 0.874,2.296&�6c   �� 5   
   $
��Name��6   $
��Left=�6   $	��Top=�6:   $��
18,154,0,0q�6R   �� 5   
   -   5        $  TxtEmail��5      $
��Left=�6$   $��29��6?   $��
22,244,0,0q�6K   $
 Wrap=�6_   $	��Top=�6s   $	��278��6�   6�   �� 5   
   $��Email��6   $
��Left=�6    $	��Top=�6;   $��
22,213,0,0q�6S   �� 5   
   -   5        $ 	BtnUpdate��5      $��Update��6$   $
��Left=�65   $��192,328,0,0q�6P   $	��Top=�6e   $	��108��6}   6�   �� 5   
   -   5        $ 	BtnDelete��5      $��Delete��6$   $
��Left=�65   $��344,328,0,0q�6P   $	��Top=�6e   $	��108��6}   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DataGridSupplier��5         AutoGenerateColumns .  $
��Left=�6G   $��
366,85,0,0q�6b   $	��Top=�6v   $	��188��6�   $	��498��6�   ���	#FFF5FAFF   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        ID��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 5   1      Header$ ID��6S   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6<   �� *Id+6S     5      -	   5        Name��$	 150 5   3   $
 Name��6X   	 6?   �� *Name+6X     5      -
   5        	Email��$	 150 5   4   $ Email��6Z   	 6@   �� *Email+6Z   5      5      5      