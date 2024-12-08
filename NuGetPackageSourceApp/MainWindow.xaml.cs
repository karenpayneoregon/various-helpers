using NuGetPackageSourceApp.Models;
using System.Windows;
using NuGetPackageSourceApp.Classes;

namespace NuGetPackageSourceApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<NuGetPackage> ListBoxData { get; set; }
    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        ListBoxData = Work.Packages();
        NuGetListBox.Focus();
        NuGetListBox.SelectedIndex = 0;

    }
}

