   M S B A M L   `   `   `  ����B  >CRUDB32, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:CRUDB32CRUDB32  5   	       CRUDB32.Dashboard   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:CRUDB32  5   	   $	��600��5      $
��1080��6'   
�������  � 
ResizeMode$  NoResize=�6G    � WindowStartupLocation$ CenterScreen=�6]    � WindowStyle$
 None=�6�    � 	Resources 5
         OpenMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	CloseMenu��%�  &	        &	 �     �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 70Q�63     5      $ 0:0:0.5��6'   $	 200Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 200Q�63     5      $ 0:0:0.5��6'   $ 70Q�69   5      5      5       � Triggers 5      :� 5   
   	 :� RoutedEvent$	 ButtonBase.Click��6   
 :� 
SourceName$
 ButtonOpenMenu��66   .[�5      �� #��[  6   5      :� 5   
   $	 ButtonBase.Click��6   $
 ButtonCloseMenu��66   .[�5      �� #��[ 6   5      .��5       � ���	#FFDEDEDE.+�5!   
   � -   5         �Name$ GridMain��5!      $��Right=�6!   $��Bottom=�6=   $	��540��6X   $
��1010��6e   � 5"   
   $��60��6   $	��Top=�6   ���	#FF649CD4 J� Effect 5#      3  -System.Windows.Media.Effects.DropShadowEffect  5$         
BlurRadius$ 1Q�6#      ShadowDepth$ 1Q�62      Color$ 	#FFACACAC��6B   .+�5&      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5'      �� $��
Abel Dutra��6   $��Center=�6.   $��16(�6I   '  !MaterialDesignThemes.Wpf.PopupBox  5(         PlacementMode$ BottomAndAlignRightEdges=�6*      	StaysOpen .  $��10q�6e      PopupContent. 5)      �� $	��150��6!   .+�5*      �� $��Settings��6!   �� 5+      $��Account��6!   �� 5,      $
��Help��6!   �� 5-      �� 5.      -   5        $��	BtnLogout��5.   !   $��Logout��62   6C   5/      50      51      �� 52      $��Center=�6   $��Center=�65   $��22(�6P   	Dashboard6i   53      � 54   
   -   5        $ GridMenu��54      $��70��6!   $
��Left=�6,   ���	#FF31577E 55        56      $ 2Q�6#   $ 1Q�62   $ 	#FFAEAEAE��6B   .+�58      �� .+�59      � $	��150��6   
������� J� RenderTransformOrigin$ 0.5,0.5&�67   z�5:      V� 5;      .~�5<      �� �� 5=      �� 5>      T� 5?      5@      .+�5B      �� -   5         ��Name$ ButtonOpenMenu��5B      $��60��65   $��60��6A   $��5q�6L   $��Right=�6W   $	��Top=�6s    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 6�   ���	#FF31577E��6�   Z� ��6�   Z� .��5C      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$
 Menu=�62   $��25��6>   $��25��6I   5D      �� 5E      -   5        $ ButtonCloseMenu��5E      $y�	Collapsed=�66   $��60��6M   $��60��6Y   $��5q�6d   $��Right=�6o   $	��Top=�6�   #��[ 6�   ���	#FF31577E��6�   Z� ��6�   Z� .��5F        $ 	ArrowLeft=�62   $��25��6C   $��25��6N   5G      �� 5I      $��80��6   $��Bottom=�6(   5J      �� 5K      -   5         ��Name$ ListViewMenu��5K      ���	#FF5C99D6$��Disabled=�6H   .@�5L      �� -   5         ��Name$ ItemHome��5L   #   $��60��65   .��5M      �� $ 
Horizontal=�6%   .+�5N        $ ViewDashboard=�66   $��25��6K   $��25��6W   $��10q�6b   �� 5O      $
��Home��6(   $��Center=�64   $��20 10q�6O   5P      5Q      �� 5R      -   5        $ ItemTransaction��5R   #   $��60��6<   .��5S      �� $ 
Horizontal=�6%   .+�5T        $ Pencil=�66   $��25��6D   $��25��6P   $��10q�6[   �� 5U      $��Create��6(   $��Center=�66   $��20 10q�6Q   5V      5W      �� 5X      -	   5        $ ItemRegister��5X   #   $��60��69   .��5Y      �� $ 
Horizontal=�6%   .+�5Z        $ Ticket=�66   $��25��6D   $��25��6P   $��10q�6[   �� 5[      $��Tickets��6(   $��Center=�67   $��20 10q�6S   5\      5]      �� 5^      -
   5        $ ItemSupplier��5^   #   $��60��69   .��5_      �� $ 
Horizontal=�6%   .+�5`        $ Message=�66   $��25��6E   $��25��6Q   $��10q�6\   �� 5a      $��Transaction��6(   $��Center=�6;   $��20 10q�6W   5b      5c      �� 5d      -   5        $ ItemItem��5d   #   $��60��65   .��5e      �� $ 
Horizontal=�6%   .+�5f        $ Message=�66   $��25��6E   $��25��6Q   $��10q�6\   �� 5g      $
��Item��6(   $��Center=�64   $��20 10q�6P   5h      5i      5j      5k      5l      5m      5n      