   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   ? ;Bank, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BankBank 5   	   /clr-namespace:Bank.ValidatorBank.Validator 5   	      Bank.ProductWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   $localclr-namespace:Bank     5   	   /local1clr-namespace:Bank.Validator     5   	     � Title$  Product Window��5
   	   $	��450��6    $	��800��6-    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.      AccentColorBrush#���   5      $��2q�5       � 	Resources 5        TransactionListTemplate �� Padding � RowDefinitions � ColumnDefinitions ��Name �� Path	 �� StringFormat
 �� Triggers �� Value �� Binding `� 	Converter&   Bank.Validator.EqualityConverter �� ElementName �� 
TargetName �� Property �� Value��%�	  &	       �� 5   
   .R�5      �� $��border��6   
��� � �$��1q�67   $ 2q�5      $��1q�6!   ._�5      �  5      �� 5      �� 5       5      �� 5      $��60��6+   �� 5      $��60��6+   �� 5      $��60��6+   �� 5      $��80��6+   �� 5      $��80��6+   .+�5      �� $ IncomingOutgoingTextBlock��6    $��0��6C   $��0��6P   $��	Incoming ��5        $��Right=�61   �� 5!      $��0��6    $��1��6-   $��Date:  ��5"       $��Right=�6/   �� 5#      $��0��6    $��2��6-   ��5$       �� $ DateTransaction�$	 dd. MM. yyyy���� 5%      $ FromOnTextBlock��6    $��0��69   $��3��6F   $��From: ��5&       $��Right=�6.   �� 5'      $" BindingPayerBillNumTextBlock��6    $��0��6F   $��4��6S   $y�Visible=�5(   C   ��6    �� $ PayerBillNum�6C   �� 5)      $&  BindingRecipientBillNumTextBlock��6    $��0��6J   $��4��6W   $y�Hidden=�5*   G   ��6    �� $ RecipientBillNum�6G   �� 5,      $��1��6    $��0��6-   $��payment��5-       $��Right=�6/   �� 5.      $��1��6    $��1��6-   $��VS:  ��5/       $��Right=�6-   �� 50      $��1��6    $��2��6-   ��51       �� $ VariableSymbol��� 52      $  BindingAmountTextBlockPlus��6    $��1��6D   $��3��6Q   $��Right=�54       $y�Visible=�6<   ��53       �� $ Amount�$	 {0:+#,#.00 }��54   <   �� 55      $! BindingAmountTextBlockMinus��6    $��1��6E   $��3��6R   $��Right=�57       $y�Hidden=�6<   
���  ����56       �� $ Amount�$	 {0:-#,#.00 }��58       �� 59      $��1��6    $��4��6-   $��CZK  ��5:       $
��Left=�6-   5=      5>      
 5?      �� 5@      $
 True��6    5A      `� 5B       5C        5D   "   .1�5F      �� $ PayerBillNum�6&   �� 5G      $ BillNumberLabel��6&   $ Content�6D   5H      .a�5J      �� $ border��6    � ��
 �  ��6H   �� 5K      $" BindingPayerBillNumTextBlock��6    � y�$ Hidden=�6]   �� 5L      $&  BindingRecipientBillNumTextBlock��6    � y�$ Visible=�6a   �� 5M      $ FromOnTextBlock��6    � ��$	 On:��6J   �� 5N      $ IncomingOutgoingTextBlock��6    � ��$ 	Outgoing ��6T   �� 5O      $  BindingAmountTextBlockPlus��6    � y�$ Hidden=�6[   �� 5P      $! BindingAmountTextBlockMinus��6    � y�$ Visible=�6\   5Q      5S      5T      .��5V      � .+�5W   
   �� -   5         ��Name$ UserInfoLabel��5W      $��	Customer:��6'   $
��Left=�6;   $��21,6,0,0q�5X      $	��Top=�6"   $	��110��6:   $
��Bold"�6F   �� 5Y   
   -   5        $ 	UserInfo1��5Y      $��-��6#   $
��Left=�6/   $��	31,26,0,0q�5Z      $	��Top=�6#   $	��300��6;   �� 5[   
   -   5        $ 	UserInfo2��5[      $��-��6#   $
��Left=�6/   $��	31,46,0,0q�5\      $	��Top=�6#   $	��300��6;   �� 5]   
   -   5        $ 	UserInfo3��5]      $��-��6#   $
��Left=�6/   $��	31,66,0,0q�5^      $	��Top=�6#   $	��300��6;   �� 5`   
   -   5        $ BillInfoLabel��5`      $��Bill:��6'   $
��Left=�67   $��	22,91,0,0q�5a      $	��Top=�6#   $	��110��6;   $
��Bold"�6G   �� 5b   
   -   5        $ 	BillInfo1��5b      $��-��6#   $
��Left=�6/   $��
32,111,0,0q�5c      $	��Top=�6$   $	��300��6<   �� 5d   
   -   5        $ 	BillInfo2��5d      $��-��6#   $
��Left=�6/   $��
32,131,0,0q�5e      $	��Top=�6$   $	��300��6<   �� 5g   
   -   5         ��Name$ TransactionListBox��5g      $	��385��6.   $��
384,54,0,0q�6:   $	��351��5h      $
��Left=�5i      $	��Top=�6-   #��[ 5j      5k      �� 5l   
   -	   5        $ BillNumberLabel��5l      $��test_bill_number��6)   $
��Left=�6D   $��665,130,0,0q�5m      $	��Top=�6%   $	��110��6=   $
��Bold"�6I   $y�Hidden=�6[   �� 5o   
   -
   5         ��Name$ BackToCustomerButton��5o      $��Go back to
 customer��6/   $
��Left=�6R   $��
294,12,0,0q�5p      $	��Top=�6%   $��73��6=   $��44��5q   4   �� 5r   
   $��DATA FILTERING:��6   $
��Left=�6*   $��
23,157,0,0q�5s      $	��Top=�6$   $
��Bold"�6<   �� 5t   
   $��Incoming/Outgoing��6   $
��Left=�6,   $��
23,177,0,0q�5u      $	��Top=�6!   $
��Bold"�69   	� 5v   
   -   5         	�Name$ AllPaymentsRadioButton��5v      $��All payments��66   $
��Left=�6M   $��
28,204,0,0q�5w      $	��Top=�6*    d� 	IsChecked$
 TrueY�6B    	� 	GroupName$ ÏnOut��5x   :   	� 5y   
   -   5        $ OnlyIncomingRadioButton��5y      $��Only incoming��67   $
��Left=�6O   $��
28,229,0,0q�5z      $	��Top=�6*   $ FalseY�6B   $ ÏnOut��5{   :   	� 5|   
   -   5        $ OnlyOutgoingRadioButton��5|      $��Only outgoing��67   $
��Left=�6O   $��
28,254,0,0q�5}      $	��Top=�6*   $ FalseY�6B   $ ÏnOut��5~   :   �� 5   
   $��
Date from:��6   $
��Left=�6%   $��147,177,0,0q�5�      $	��Top=�6"   $
��Bold"�6:   (  "System.Windows.Controls.DatePicker  5�   
   -   5          Name$ TransactionDateFrom��5�      $
��Left=�62   $��149,200,0,0q�5�      $	��Top=�6*   5�      �� 5�   
   $��Date to:��6   $
��Left=�6#   $��147,224,0,0q�5�      $	��Top=�6"   $
��Bold"�6:     5�   
   -   5        $ TransactionDateTo��5�      $
��Left=�60   $��149,249,0,0q�5�      $	��Top=�6*   5�      �� 5�   
   $��Amount from:��6   $
��Left=�6'   $��260,177,0,0q�5�      $	��Top=�6"   $
��Bold"�6:   �� 5�   
   -   5         ��Name$ AmountFromTextBox��5�      $
��Left=�6-   $��23��6H   $��262,200,0,0q�6T    �� TextWrapping$
 Wrap=�5�      $�� ��6&   $	��Top=�6.   $��92��6F   5�      �� 5�   
   $��
Amount to:��6   $
��Left=�6%   $��260,224,0,0q�5�      $	��Top=�6"   $
��Bold"�6:   �� 5�   
   -   5        $ AmountToTextBox��5�      $
��Left=�6+   $��23��6F   $��262,249,0,0q�6R   $
 Wrap=�5�      $�� ��6&   $	��Top=�6.   $��92��6F   5�      �� 5�   
   $��Incoming payments:��6   $
��Left=�6-   $��	383,2,0,0q�5�      $	��Top=�6#   $
��Bold"�6;   �� 5�   
   -   5        $ SumIncomingPaymentsLabel��5�      $��0 Kč��62   $
��Left=�6A   $��
383,24,0,0q�5�      $	��Top=�6$   �� 5�   
   $��Outgoing payments:��6   $
��Left=�6-   $��	526,2,0,0q�5�      $	��Top=�6#   $
��Bold"�6;   �� 5�   
   -   5        $ SumOutgoingPaymentsLabel��5�      $��0 Kč��62   $
��Left=�6A   $��
526,24,0,0q�5�      $	��Top=�6$   
���  ��6<   �� 5�   
   $��Balance:��6   $
��Left=�6#   $��	670,2,0,0q�5�      $	��Top=�6#   $
��Bold"�6;   �� 5�   
   -   5        $$ BalanceBySelectedCriteriaLabel��5�      $��0 Kč��68   $
��Left=�6G   $��
670,24,0,0q�5�      $	��Top=�6$   �� 5�   
   $��DATA SORTING:��6   $
��Left=�6(   $��
23,314,0,0q�5�      $	��Top=�6!   $
��Bold"�69   �� 5�   
   $��By Date:��6   $
��Left=�6#   $��
23,335,0,0q�5�      $	��Top=�6!   $
��Bold"�69   	� 5�   
   -   5        $ NewestToOldest��5�      $��From Newest to Oldest��6.   $
��Left=�5�      $��
28,362,0,0q�61   $	��Top=�5�      $ FalseY�6.   $ Sorting��5�   7   	� 5�   
   -   5        $ OldestToNewest��5�      $��From Oldest to Newest��6.   $
��Left=�5�      $��
28,387,0,0q�61   $	��Top=�5�      $ FalseY�6.   $ Sorting��5�   7   �� 5�   
   $��
By Amount:��6   $
��Left=�6%   $��191,335,0,0q�5�      $	��Top=�6"   $
��Bold"�6:   	� 5�   
   -   5        $ HighestToLowest��5�      $��From Highest to Lowest��6/   $
��Left=�5�      $��196,362,0,0q�6(   $	��Top=�5�      $ FalseY�6%   $ Sorting��5�   .   	� 5�   
   -   5        $ LowestToHighest��5�      $��From Lowest to Highest��6/   $
��Left=�5�      $��196,387,0,0q�6(   $	��Top=�5�      $ FalseY�6%   $ Sorting��5�   .   �� 5�   
   $	��VS:��6   $
��Left=�6   $��
25,283,0,0q�5�      $	��Top=�6!   $
��Bold"�69   �� 5�   
   -   5        $ VariableSymbolTextBox��5�      $
��Left=�61   $��23��6L   $��
53,284,0,0q�6X   $
 Wrap=�5�      $�� ��6!   $	��Top=�6)   $	��113��6A   5�      �� 5�   
   $��Bill Number:��6   $
��Left=�6'   $��176,283,0,0q�5�      $	��Top=�6"   $
��Bold"�6:   �� 5�   
   -   5        $ BillNumberTextBox��5�      $
��Left=�6-   $��23��6H   $��262,284,0,0q�6T   $
 Wrap=�5�      $�� ��6!   $	��Top=�6)   $��92��6A   5�      �� 5�   
   -   5        $ DefaultView��5�      $��Default
   view��6&   $
��Left=�6D   $��294,132,0,0q�5�      $	��Top=�6"   $��73��6:   $��44��5�   -   5�      5�      