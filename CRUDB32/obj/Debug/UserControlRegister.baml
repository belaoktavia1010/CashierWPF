   M S B A M L   `   `   `  ����B  >CRUDB32, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDB32CRUDB32  5      !    CRUDB32.UserControlRegister   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:CRUDB32     5      .��5	      � ���	#FFA6ECE2.+�5
   
   �� $
��Name��6   $
��Left=�6   $��	98,76,0,0q�6:   $	��Top=�6M   �� 5   
   $��REGISTER��6   $
��Left=�6#   $��
266,20,0,0q�6>   $	��Top=�6R   �� 5   
   $��Email��6   $
��Left=�6    $��
98,117,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��
TxtNameReg��5      $
��Left=�6$   $��23��6?   $��
311,80,0,0q�6K     �� TextWrapping$
  Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��437��6�   6�   �� 5   
   -   5        $��TxtEmailReg��5   B   $
��Left=�6U   $��23��6p   $��311,120,0,0q�6|   $
  Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��437��6�   6�   �� 5   
   $
��Role��6   $
��Left=�6   $��
98,163,0,0q�6:   $	��Top=�6N   �� 5   
   -   5        $��btnSubmitReg��5      $��Submit��6%   $
��Left=�66   $��311,212,0,0q�6Q   $	��Top=�6f   $��86��6~   6�   �� 5   
   -   5        $��
CBRegister��5   `    �� DisplayMemberPath$
 Name��60    �� SelectedValuePath$ Id��6I   $
��Left=�6r   $��311,167,0,0q�6�   $	��Top=�6�   $	��177��6�   ��6   �� *Role+6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DataGridUser��5   /      AutoGenerateColumns .  $
��Left=�6t   $	��144��6�   $��
98,288,0,0q�6�   $	��Top=�6�   $	��816��6�   ���	#FFF5FAFF   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        	Name_User��,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 5   8      Header$
 Name��6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Name+6]     5      -   5        
Email_User��$	 150 5   9   $ Email��6_    6E   �� *Email+6_   5      5      5      