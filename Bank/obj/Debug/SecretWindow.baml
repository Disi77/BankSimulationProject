   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   ? ;Bank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BankBank 5   	      Bank.SecretWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   $localclr-namespace:Bank     5   	     � Title$  Chamber of Secrets��5	   	   $	��450��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�5
       � 
ResizeMode$ NoResize=�6<      AccentColorBrush#���   5      $��2q�5      .��5      � .+�5   
   �� -   5         ��Name$ 
PayerLabel��5      $��Payer: ��6$   $
��Left=�66   $��	40,15,0,0q�5      $	��Top=�6#   $	��199��6;   �� 5   
   -   5         ��Name$ 	PayerList��5      $
��Left=�6&   $	��183��6A   $��	40,60,0,0q�5      $	��Top=�6&   $	��143��5      $y�Visible=�6    �� View 5      �� 5      .P�5      �� $��Bill Number��6%    �� DisplayMemberBinding 6:   �� *
BillNumber+5      5      �� 5   
   -   5        $ RecipientLabel��5      $��Recipient: ��6(   $
��Left=�6>   $��
200,15,0,0q�5      $	��Top=�6$   $	��199��6<   $��45��6H   �� 5   
   -   5        $ RecipientList��5      $
��Left=�6*   $	��183��6E   $��
200,60,0,0q�5      $	��Top=�6'   $	��146��5      $y�Visible=�6    5       �� 5!      .P�5"      �� $��Bill Number��6%    6:   �� *
BillNumber+5#      5%      �� 5(   
   $��Variable symbol��6   $
��Left=�6*   $��
361,34,0,0q�6E   $	��Top=�6Y   $	��119��6q   �� 5)   
   -   5         ��Name$ VariableSymbolTextBox��5)      $
��Left=�61   $��23��5*      $��
361,65,0,0q�6    �� TextWrapping$
 Wrap=�62   $�� ��5+      $	��Top=�6   $	��120��62   �� 5-   
   $��Amount��6   $
��Left=�6!   $��361,105,0,0q�6<   $	��Top=�6Q   $	��119��6i   �� 5.   
   -   5        $ AmountTextBox��5.      $
��Left=�6)   $��23��5/      $��360,130,0,0q�6   $
 Wrap=�50      $�� ��6&   $	��Top=�51      $	��120��6*   (  "System.Windows.Controls.DatePicker  53   
   -   5        	  Name$	 DateSelectionBox��53      $
��Left=�6/   $��361,167,0,0q�54      $	��Top=�6*   $	��119��55      �� 56   
   -   5        $��Create Transaction��56      $
��Left=�6.   $��361,214,0,0q�57      $	��Top=�6&   $	��120��58      $��29��6   6)   �� 59   
   -	   5        $ CreateTransactionLabel��59      $��Label��60   $
��Left=�6@   $��497,212,0,0q�6[   $	��Top=�6p   $��61��6�   $	��249��6�   �� 5:   
   -
   5        $��Close Secret Page��5:      $
��Left=�6-   $��
660,10,0,0q�5;      $	��Top=�6%   $	��120��6=   $��26��6I   5<      �� 5=   
   -   5        
 ��Name$
 LastTransactionsListBox��5=      $
��Left=�63   $	��138��6N   $��
40,267,0,0q�5>      $	��Top=�6&   $	��706��6>   $y�Visible=�5?      �� 5@   
   $��Last 5 Transactions:��6   $
��Left=�6/   $��
40,242,0,0q�5A      $	��Top=�6$   5E      5F      