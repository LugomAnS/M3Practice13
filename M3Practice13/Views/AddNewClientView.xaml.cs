﻿using M3Practice13.ViewModels;
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

namespace M3Practice13.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewClientView.xaml
    /// </summary>
    public partial class AddNewClientView : UserControl
    {
        public AddNewClientView()
        {
            InitializeComponent();
            this.DataContext = new AddNewClientVM();
        }
    }
}
