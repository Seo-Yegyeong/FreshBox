﻿using FreshBox.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreshBox.Views
{
    /// <summary>
    /// Interaction logic for MainVisual.xaml
    /// </summary>
    public partial class MainVisualView : UserControl
    {
        public MainVisualView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainVisualViewModel vm)
            {
                vm.LoadUserInfo();
            }
        }
    }
}
