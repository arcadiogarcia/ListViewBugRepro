using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


        }

        public static TreeNode BuildTree(int depth)
        {
            var node = new TreeNode();
            if (depth > 0)
            {
                node.Children.Add(BuildTree(depth - 1));
            }
            return node;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tree = BuildTree(7);

            domTreeView.ItemsSource = tree.Children;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            foreach( var node in (ObservableCollection<TreeNode>)domTreeView.ItemsSource)
            {
                node.Expand();
            }
        }
    }

    public class TreeNode : INotifyPropertyChanged
    {
        public ObservableCollection<TreeNode> Children { get; } = new ObservableCollection<TreeNode>();

        bool isExpanded = false;
        public bool IsExpanded { get => isExpanded; set { isExpanded = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void Expand()
        {
            IsExpanded = true;
            foreach(var child in Children)
            {
                child.Expand();
            }
        }
    }
}
