using System.ComponentModel;
using VisualStudioExternalToolsApp.Classes;

namespace VisualStudioExternalToolsApp;

public partial class Form1 : Form
{
    private BindingList<ExternalTool> _bindingList;
    private BindingSource _bindingSource = new BindingSource();
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
        
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        var userName = Environment.UserName;
        var filePath = $"C:\\Users\\{userName}\\AppData\\Local\\Microsoft\\VisualStudio\\17.0_f56beab6\\Settings";
        if (!Directory.Exists(filePath)) return;

        List<ExternalTool> tools = ExternalToolsOperations.ReadExternalTools(Path.Combine(filePath, "CurrentSettings.vssettings")).ToList();
        _bindingList = new BindingList<ExternalTool>(tools);
        _bindingSource.DataSource = _bindingList;

        NameTextBox.DataBindings.Add("Text", _bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
        CommandTextBox.DataBindings.Add("Text", _bindingSource, "Command", true, DataSourceUpdateMode.OnPropertyChanged);
        TitleTextBox.DataBindings.Add("Text", _bindingSource, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
        InitialDirectoryTextBox.DataBindings.Add("Text", _bindingSource, "InitialDirectory", true, DataSourceUpdateMode.OnPropertyChanged);

        BindingNavigator1.BindingSource = _bindingSource;
    }
}
