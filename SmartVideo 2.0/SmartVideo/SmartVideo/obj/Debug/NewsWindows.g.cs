﻿#pragma checksum "..\..\NewsWindows.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E64F61861B0E319C8B42DAF6AB7CA4F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using SmartVideo;
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


namespace SmartVideo {
    
    
    /// <summary>
    /// NewsWindows
    /// </summary>
    public partial class NewsWindows : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox newsLB;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ajouterBtn;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button supprimerBtn;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox titreTB;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sauverBtn;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\NewsWindows.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox contenuTB;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartVideo;component/newswindows.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewsWindows.xaml"
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
            this.newsLB = ((System.Windows.Controls.ListBox)(target));
            
            #line 14 "..\..\NewsWindows.xaml"
            this.newsLB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.newsLB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ajouterBtn = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\NewsWindows.xaml"
            this.ajouterBtn.Click += new System.Windows.RoutedEventHandler(this.ajouter_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.supprimerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\NewsWindows.xaml"
            this.supprimerBtn.Click += new System.Windows.RoutedEventHandler(this.supprimerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.titreTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.sauverBtn = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\NewsWindows.xaml"
            this.sauverBtn.Click += new System.Windows.RoutedEventHandler(this.sauverBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.contenuTB = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

