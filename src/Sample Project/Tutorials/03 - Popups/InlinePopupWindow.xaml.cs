﻿using System.Windows;

namespace Samples.Tutorials.Popups
{
    /// <summary>
    /// Interaction logic for InlinePopupWindow.xaml
    /// </summary>
    public partial class InlinePopupWindow : Window
    {
        public InlinePopupWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            MyNotifyIcon.Dispose();

            base.OnClosing(e);
        }
    }
}