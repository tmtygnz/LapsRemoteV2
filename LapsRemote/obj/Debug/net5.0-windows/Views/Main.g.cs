﻿#pragma checksum "..\..\..\..\Views\Main.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6DCD9C02090591CCFA8EB8D284B71DAC0EC9E5EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LapsRemote.Screens;
using LiveCharts.Wpf;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
using ScottPlot;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace LapsRemote.Screens {
    
    
    /// <summary>
    /// Main
    /// </summary>
    public partial class Main : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TemperatureRect;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TemperatureTextView;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border OxyStatRect;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock OxyStatTextIvew;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BPMRect;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BPMTextView;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border RespRateRect;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RespRateTextView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LapsRemote;component/views/main.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Main.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\..\Views\Main.xaml"
            ((LapsRemote.Screens.Main)(target)).Activated += new System.EventHandler(this.MetroWindow_Activated);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\..\Views\Main.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenRepo);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 27 "..\..\..\..\Views\Main.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ExitClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TemperatureRect = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.TemperatureTextView = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.OxyStatRect = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.OxyStatTextIvew = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.BPMRect = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.BPMTextView = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.RespRateRect = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.RespRateTextView = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

