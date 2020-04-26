﻿#pragma checksum "..\..\SecretWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "12749BBCDE26FBAF75846E27E499047262A804EE86B08B38B71C4A856BFCDA38"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bank;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Bank {
    
    
    /// <summary>
    /// SecretWindow
    /// </summary>
    public partial class SecretWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PayerLabel;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView PayerList;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label RecipientLabel;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView RecipientList;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VariableSymbolTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AmountTextBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateSelectionBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\SecretWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CreateTransactionLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bank;component/secretwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SecretWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PayerLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.PayerList = ((System.Windows.Controls.ListView)(target));
            
            #line 17 "..\..\SecretWindow.xaml"
            this.PayerList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PayerSelection);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RecipientLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.RecipientList = ((System.Windows.Controls.ListView)(target));
            
            #line 30 "..\..\SecretWindow.xaml"
            this.RecipientList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RecipientSelection);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VariableSymbolTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AmountTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.DateSelectionBox = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            
            #line 55 "..\..\SecretWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateTransactionButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CreateTransactionLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

