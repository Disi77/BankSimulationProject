   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   ? ;Bank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BankBank 5   	      Bank.OfficialWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   i eMahApps.Metro.IconPacks.PicolIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Zondicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag	 cMahApps.Metro.IconPacks.Typicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak
 gMahApps.Metro.IconPacks.FeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a^ ZMahApps.Metro.IconPacks, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.RPGAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.WeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Unicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.SimpleIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks    	 
                  5   	   tControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro    	 
                  5   	   Hlocalclr-namespace:Bank    	 
                  5   	     � Title$$  Bank System - Official version��5
   	   $	��450��60   $	��800��6=    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6      AccentColorBrush#���   5      $��2q�5      .��5      � -   5         �Name$ GridOfficialWindow��5      .+�5   
   o� -   5         o�Name$ MainMenu��5      $��31��6!   $	��Top=�5      $y�Visible=�6'   $��1��5        	MetroMenu#��[ 6   .@�5      m� $��
My Account��6   $��10,0,0,0q�6+   .@�5      m� -   5        $��Password change��5       m� Icon 5      1  +MahApps.Metro.IconPacks.PackIconFontAwesome  5         Kind$ KeySolid=�68   $��Right=�5   8   $��Center=�5   8   5      m� 5      -   5        $��Update my account��5       5        5      $ UserEditSolid=�68   $��Right=�5   8   $��Center=�5   8   5!      m� 5"      -   5         m�Name$ MenuItem_SecretPage��5"      $��Secret Page��68   $y�	Collapsed=�5#       5$        5%      $ 	BombSolid=�68   $��Right=�5&   8   $��Center=�5'   8   5)      �� 5*      m� 5+      -   5        $��Logout��5+       5,      /  )MahApps.Metro.IconPacks.PackIconZondicons  5-         Kind$ 
CloseSolid=�66   $��Right=�5.   6   $��Center=�5/   6   51      52      m� 53      $��	Customers��6   $��10,0,0,0q�6*   .@�54      m� -   5        $��Create new Customer��54       55      2 
 ,MahApps.Metro.IconPacks.PackIconFeatherIcons  56      	   Kind$	 UserPlus=�69   $��Right=�57   =   $��Center=�58   =   5:      m� 5;      -   5        $��Search Customer��5;       5<      .  (MahApps.Metro.IconPacks.PackIconMaterial  5=      
   Kind$
 AccountSearch=�65   $��Right=�5>   9   $��Center=�5?   9   5A      5B      5C      �� 5D   
   
���   �$��1q�6%   $
��Left=�69   $	��6.5��5E      $��11,23.2,0,0q�6   $��1��63   $	��Top=�6@   $	��226��5F      ���	#CC119EDA6   �� 5I   
   -	   5         ��Name$ MainPageLabel��5I      $��Main page label: ��6'   $
��Left=�5J      $��13,42.2,0,0q�6+   $	��Top=�6@   $��14(�5K      $
��Bold"�6   $y�Visible=�60   $��1��6E   �� 5N   
   -
   5         ��Name$ OldPassword��5N      $
��Left=�6'   $��24��5O      $��	14,80,0,0q�6    �� TextWrapping$
 Wrap=�61   $��Old password ...��5P      $	��Top=�6*   $	��195��5Q      ���	#CC119EDA$��Italic$�65   {�. $y�Hidden=�5T   :   $��1��6N   �� 5U   
   -   5         ��Name$ OldPassword_NoIcon��5U      $
��Left=�6,   $��15��5V      $��
188,85,0,0q�6   $��1��60   $	��Top=�5W      $��15��6(   $y�Hidden=�5Y      ��5X      3  -MahApps.Metro.IconPacks.MicronsImageExtension     Kind$ CrossInline=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 �  ��5Y      �� 5Z   
   -   5        $ OldPassword_YesIcon��5Z      $
��Left=�6-   $��15��5[      $��
188,85,0,0q�6   $��1��60   $	��Top=�5\      $��15��6(   $y�Hidden=�5^      ��5]        $ 
TickInline=�
 � � �5^      �� 5_   
   -   5        $ NewPassword1��5_      $
��Left=�6(   $��24��5`      $��14,122.2,0,0q�6   $
 Wrap=�6/   $��New password ...��5a      $	��Top=�6%   $	��195��5b      ���	#CC119EDA$��Italic$�60   {�. $y�Hidden=�5e   5   $��1��6I   �� 5f   
   -   5        $ NewPassword1_NoIcon��5f      $
��Left=�6-   $��15��5g      $��188,127,0,0q�6   $��1��61   $	��Top=�5h      $��15��6(   $y�Hidden=�5j      ��5i        $ CrossInline=�
 �  ��5j      �� 5k   
   -   5        $ NewPassword1_YesIcon��5k      $
��Left=�6.   $��15��5l      $��188,127,0,0q�6   $��1��61   $	��Top=�5m      $��15��6(   $y�Hidden=�5o      ��5n        $ 
TickInline=�
 � � �5o      �� 5p   
   -   5        $ NewPassword2��5p      $
��Left=�6(   $��24��5q      $��
14,152,0,0q�6   $
 Wrap=�6-   $��New password ...��5r      $	��Top=�6%   $	��195��5s      ���	#CC119EDA$��Italic$�60   {�. $y�Hidden=�5v   5   $��1��6I   �� 5w   
   -   5        $ NewPassword2_NoIcon��5w      $
��Left=�6-   $��15��5x      $��188,157,0,0q�6   $��1��61   $	��Top=�5y      $��15��6(   $y�Hidden=�5{      ��5z        $ CrossInline=�
 �  ��5{      �� 5|   
   -   5        $ NewPassword2_YesIcon��5|      $
��Left=�6.   $��15��5}      $��188,157,0,0q�6   $��1��61   $	��Top=�5~      $��15��6(   $y�Hidden=�5�      ��5        $ 
TickInline=�
 � � �5�      �� 5�   
   -   5         ��Name$ ConfirmButton��5�      $��Confirm��6(   $
��Left=�5�      $��
25,370,0,0q�5�      $	��Top=�6%   $��75��5�      $y�Hidden=�5�   -   $��1��6A   �� 5�   
   -   5        $ StornoButton��5�      $��Storno��6'   $
��Left=�5�      $��115,370,0,0q�5�      $	��Top=�6"   $��75��5�      $y�Visible=�5�   (   $��1��6=   �� 5�   
   -   5        $ 	NameLabel��5�      $��Name:��6#   $
��Left=�63   $��
4,91.2,0,0q�5�      $	��Top=�6$   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -   5        $ NameTextBox��5�      $
��Left=�6'   $��23��6B   $��104,91.2,0,0q�5�      $
 Wrap=�6(   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62    �� MaxLines$ 1��6?    �� 	MaxLength$ 50��6L   5�      �� 5�   
   -   5        $ NameTextBox_NoIcon��5�      $
��Left=�6,   $��10��5�      $��240,100,0,0q�6   $��1��61   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -   5        $ SurnameLabel��5�      $��Surname:��6&   $
��Left=�69   $��4,116.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -   5        $ SurnameTextBox��5�      $
��Left=�6*   $��23��6E   $��104,116.2,0,0q�5�      $
 Wrap=�6)   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   5�      �� 5�   
   -   5        $ SurnameTextBox_NoIcon��5�      $
��Left=�6/   $��10��5�      $��240,125,0,0q�6   $��1��61   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -   5        $ 
EmailLabel��5�      $��Email:��6$   $
��Left=�65   $��4,141.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -   5        $ EmailTextBox��5�      $
��Left=�6(   $��23��6C   $��104,141.2,0,0q�5�      $
 Wrap=�6)   $��@��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   5�      �� 5�   
   -   5        $ EmailTextBox_NoIcon��5�      $
��Left=�6-   $��10��5�      $��240,150,0,0q�6   $��1��61   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -   5        $ 
PhoneLabel��5�      $��Phone:��6$   $
��Left=�65   $��4,166.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -   5        $ PhoneTextBox��5�      $
��Left=�6(   $��23��6C   $��104,166.2,0,0q�5�      $
 Wrap=�6)   $
��+420��5�      $	��Top=�5�      $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   5�      �� 5�   
   -    5        $ PhoneTextBox_NoIcon��5�      $
��Left=�6-   $��10��5�      $��240,175,0,0q�6   $��1��61   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -!   5        $ AddressLabel��5�      $��Address:��6&   $
��Left=�69   $��4,191.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -"   5        $ StreetLabel��5�      $��Street:��6%   $
��Left=�67   $��4,216.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -#   5        $ StreetTextBox��5�      $
��Left=�6)   $��23��6D   $��104,216.2,0,0q�5�      $
 Wrap=�6)   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   �� 5�   
   -$   5        $ StreetTextBox_NoIcon��5�      $
��Left=�6.   $��10��5�      $��240,224.2,0,0q�6   $��1��63   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -%   5        $ StreetNumberLabel��5�      $��Street Number:��6+   $
��Left=�6D   $��4,241.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -&   5        $ StreetNumberTextBox��5�      $
��Left=�6/   $��23��6J   $��104,241.2,0,0q�5�      $
 Wrap=�6)   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Visible=�6   $��1��63   $ 1��6@   $ 50��6M   �� 5�   
   -'   5        $  StreetNumberTextBox_NoIcon��5�      $
��Left=�64   $��10��5�      $��240,250.2,0,0q�6   $��1��63   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -(   5        $ 	CityLabel��5�      $��City:��6#   $
��Left=�63   $��4,266.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -)   5        $ CityTextBox��5�      $
��Left=�6'   $��23��6B   $��104,266.2,0,0q�5�      $
 Wrap=�6)   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5�      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   �� 5�   
   -*   5        $ CityTextBox_NoIcon��5�      $
��Left=�6,   $��10��5�      $��240,275.2,0,0q�6   $��1��63   $	��Top=�5�      $��10��6(   $y�Hidden=�5�      ��5�        $ CrossInline=�
 �  ��5�      �� 5�   
   -+   5        $ PostalCodeLabel��5�      $��Postal Code:��6)   $
��Left=�6@   $��4,291.2,0,0q�5�      $	��Top=�6%   $��Right=�5�      $	��100��5�      $y�Hidden=�6   $��1��60   �� 5�   
   -,   5        $ PostalCodeTextBox��5�      $
��Left=�6-   $��23��6H   $��104,291.2,0,0q�5�      $
 Wrap=�6)   $�� ��5�      $	��Top=�6   $��Bottom=�5�      $	��150��5      $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   �� 5  
   --   5        $ PostalCodeTextBox_NoIcon��5     $
��Left=�62   $��10��5     $��240,299,0,0q�6   $��1��61   $	��Top=�5     $��10��6(   $y�Hidden=�5     ��5       $ CrossInline=�
 �  ��5     �� 5  
   -.   5        $ CountryLabel��5     $��Country:��6&   $
��Left=�69   $��4,316.2,0,0q�5     $	��Top=�6%   $��Right=�5     $	��100��5	     $y�Hidden=�6   $��1��60   �� 5
  
   -/   5        $ CountryTextBox��5
     $
��Left=�6*   $��23��6E   $��104,316.2,0,0q�5     $
 Wrap=�6)   $�� ��5     $	��Top=�6   $��Bottom=�5     $	��150��5     $y�Hidden=�6   $��1��62   $ 1��6?   $ 50��6L   �� 5  
   -0   5        $ 
ValidLabel��5     $��Valid:��6$   $
��Left=�65   $��234,91.2,0,0q�5     $	��Top=�6&   $��Right=�5     $	��100��5     $y�Hidden=�6   $��1��60   �� 5  
   -1   5        $ ValidTextBox��5     $
��Left=�6(   $��23��6C   $��334,91.2,0,0q�5     $
 Wrap=�6(   $�� ��5     $	��Top=�6   $��Bottom=�5     $	��150��5     $y�Hidden=�6   $��1��62   �� 5  
   -2   5        $ UserTypeLabel��5     $��
User Type:��6'   $
��Left=�6<   $��234,116.2,0,0q�5     $	��Top=�6'   $��Right=�5     $	��100��5     $y�Hidden=�6   $��1��60   �� 5  
   -3   5         ��Name$ UserTypeComboBox��5     $��23��6-   $
��Left=�5     $��Bottom=�5     $��334,116.2,0,0q�5     $	��Top=�6*   $	��150��5      $y�Hidden=�6   $��1��63   .@�5!     �� -4   5         ��Name$ UserTypeComboBox_Customer��5!     $��Customer��6>    �� 
IsSelected . 6Q   �� 5"     -5   5        $ UserTypeComboBox_Official��5"     $��Official��6>    . 6Q   5#     �� 5$  
   -6   5        $ UserSubTypeLabel��5$     $��User Subype:��6*   $
��Left=�6A   $��234,141.2,0,0q�5%     $	��Top=�6'   $��Right=�5&     $	��100��5'     $y�Hidden=�6   $��1��60   �� 5(  
   -7   5        $ OfficialSubTypeComboBox��5(     $��23��64   $
��Left=�5)     $��Bottom=�5*     $��334,141.2,0,0q�5+     $	��Top=�6*   $	��150��5,     $y�Hidden=�6   $��1��63   .@�5-     �� -8   5        $$ OfficialSubTypeComboBox_Junior��5-     $��Junior��6C   �� 5.     -9   5        $$ OfficialSubTypeComboBox_Normal��5.     $��Normal��6C   �� 5/     -:   5        $$ OfficialSubTypeComboBox_Senior��5/     $��Senior��6C   50     �� 51  
   -;   5        $ CustomerSubTypeComboBox��51     $��23��64   $
��Left=�52     $��Bottom=�53     $��334,141.2,0,0q�54     $	��Top=�6*   $	��150��55     $y�Hidden=�6   $��1��63   .@�56     �� -<   5        $$ CustomerSubTypeComboBox_Person��56     $��Person��6C   �� 57     -=   5        $% CustomerSubTypeComboBox_Company��57     $��Company��6D   58     �� 59  
   ->   5        $ 
LoginLabel��59     $��Login:��6$   $
��Left=�65   $��234,166.2,0,0q�5:     $	��Top=�6'   $��Right=�5;     $	��100��5<     $y�Hidden=�6   $��1��60   �� 5=  
   -?   5        $ LoginTextBox��5=     $
��Left=�6(   $��23��6C   $��334,166.2,0,0q�5>     $
 Wrap=�6)   $�� ��5?     $	��Top=�6   $��Bottom=�5@     $	��150��5A     $y�Visible=�6   $��1��63   $ 1��6@   $ 50��6M   �� 5B  
   -@   5        $ LoginTextBox_NoIcon��5B     $
��Left=�6-   $��10��5C     $��469,174,0,0q�6   $��1��61   $	��Top=�5D     $��10��6(   $y�Hidden=�5F     ��5E       $ CrossInline=�
 �  ��5F     �� 5I  
   -A   5        $ ListOfProductsLabel��5I     $��List of products:��6-   $
��Left=�6I   $��325,215,0,0q�5J     $	��Top=�6%   $
��Left=�5K     $	��117��5L     $y�Visible=�6   $
��Bold"�61   �� 5M  
   -B   5         ��Name$ AllProductsListView��5M     $
��Left=�60   $��95��6K   $��325,246,0,0q�5N     $	��Top=�6(   $	��188��5O     $y�Visible=�6    �� View 5P     �� 5Q     .P�5R     �� $��Bill Number��6%    �� Width$	 100��6:    �� DisplayMemberBinding 6F   �� *
BillNumber+�� 5S     $��Details��6%   $ 85��66    �� CellTemplate 5T     �� 5U     .R�5V  "   �� $ DetailsButton��6)   -C   5        $��Details��5V  @   ��5W  )   �� *
BillNumber+5X     5Z     5[     5]     �� 5a  
   -D   5        $ EditModeButton��5a     $��	Edit mode��6)   $
��Left=�5b     $��
25,370,0,0q�5c     $	��Top=�6%   $��75��5d     $y�Hidden=�5e  ,   $��1��6@   �� 5h  
   -E   5        $ UpdateUserButton��5h     $��Update��6+   $
��Left=�5i     $��
25,370,0,0q�5j     $	��Top=�6%   $��75��5k     $y�Hidden=�5l  4   �� 5o  
   -F   5        $ CreateCustomerButton��5o     $��Create��6/   $
��Left=�5p     $��
25,370,0,0q�5q     $	��Top=�6%   $��75��5r     $y�Hidden=�5s  5   �� 5v  
   -G   5        $ SearchTextBox��5v     $
��Left=�6)   $��29��6D   $��160,41.2,0,0q�6P   $
 Wrap=�5w     $��Enter user name ...��6&   $	��Top=�6A   $	��553��6Y   $y�Visible=�5x     $��1��5y  9   $��Center=�6F   $��Italic$�5z     �� 5{  
   -H   5        $ SearchButton��5{     $
��Left=�6'   $��716,41.2,0,0q�6B   $��1��5|     $	��Top=�6   $��49��66   $��29��6A   ��5~     �� 5     ��6     $ Search=�5�     �� 5�  
   -I   5        $ AllCustomersListView��5�     $
��Left=�61   $	��280��6L   $��27,80.2,0,0q�5�     $	��Top=�6(   $	��740��5�     $y�Hidden=�6   $��1��63    5�     �� 5�     .P�5�     �� $
��Name��6%    63   �� *Name+�� 5�     $��Surname��6%    66   �� *	SurName+�� 5�     $��Customer Type��6%    6<   �� *CustomerType+�� 5�     $��Valid��6%    64   �� *Valid+�� 5�     $
��Mail��6%    63   �� *Mail+�� 5�     $��Phone��6%    64   �� *Phone+5�     5�     �� 5�  
   -J   5        $ ViewDetails��5�     $��Details��6&   $
��Left=�5�     $��
25,370,0,0q�5�     $	��Top=�6%   $��75��5�     $y�Visible=�5�  2   $��1��6G   �� 5�  
   -K   5        $ CustomerNotFoundLabel��5�     $$��CUSTOMER NOT FOUND. TRY AGAIN.��6/   $��Center=�6X   $��27,183,23.4,0q�5�     $	��Top=�6'   $��28��6?   $	��740��6K   $��14(�5�     $��Center=�6   $��Oblique$�6B   ���	#CC119EDA$y�Visible=�5�  '   �� 5�  
   -L   5        $ SignedInAsLabel��5�     $��Signed in as: ��6)   $
��Left=�6B   $��544,0.2,0,0q�5�     $��1��6%   $	��Top=�62   $��14(�6J   �� 5�  
   
���   �$��1q�6%   $
��Left=�69   $��6��5�     $��545,23.2,0,0q�6   $��1��6.   $	��Top=�6;   $	��234��5�     ���	#CC119EDA6   5�     5�     