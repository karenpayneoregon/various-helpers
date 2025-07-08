using System.ComponentModel;
using VisualStudioExternalToolsApp.Classes;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace VisualStudioExternalToolsApp;

public partial class Form1 : Form
{
    private BindingList<ExternalTool> _bindingList;
    private BindingSource _bindingSource = new();
    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
        titleLabel.Click += TitleLabelClickHandler;
    }

    private void TitleLabelClickHandler(object? sender, EventArgs e)
    {
        TitleTextBox.Focus();
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        if (!EnvironmentSettings.Instance.DirectoryExists) return;

        List<ExternalTool> tools = ExternalToolsOperations
            .ReadExternalTools(EnvironmentSettings.Instance.FileName)
            .ToList();

        _bindingList = new BindingList<ExternalTool>(tools);
        _bindingSource.DataSource = _bindingList;

        NameTextBox.DataBindings.Add("Text", _bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
        CommandTextBox.DataBindings.Add("Text", _bindingSource, "Command", true, DataSourceUpdateMode.OnPropertyChanged);
        TitleTextBox.DataBindings.Add("Text", _bindingSource, "Title", true, DataSourceUpdateMode.OnPropertyChanged);
        InitialDirectoryTextBox.DataBindings.Add("Text", _bindingSource, "InitialDirectory", true, DataSourceUpdateMode.OnPropertyChanged);

        BindingNavigator1.BindingSource = _bindingSource;
    }

    private void OpenSourceFileButton_Click(object sender, EventArgs e)
    {
        
        var fileOperations = new FileOperations();
        fileOperations.OpenSettingsFile(EnvironmentSettings.Instance.FileName);
    }
}