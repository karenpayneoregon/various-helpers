namespace VisualStudioExternalToolsApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        label1 = new Label();
        BindingNavigator1 = new VisualStudioExternalToolsApp.Classes.CoreBindingNavigator();
        NameTextBox = new TextBox();
        label2 = new Label();
        CommandTextBox = new TextBox();
        titleLabel = new Label();
        TitleTextBox = new TextBox();
        label4 = new Label();
        InitialDirectoryTextBox = new TextBox();
        OpenSourceFileButton = new Button();
        (BindingNavigator1).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(23, 55);
        label1.Name = "label1";
        label1.Size = new Size(49, 20);
        label1.TabIndex = 0;
        label1.Text = "Name";
        // 
        // BindingNavigator1
        // 
        BindingNavigator1.ImageScalingSize = new Size(20, 20);
        BindingNavigator1.Location = new Point(0, 0);
        BindingNavigator1.Name = "BindingNavigator1";
        BindingNavigator1.Size = new Size(877, 27);
        BindingNavigator1.TabIndex = 1;
        BindingNavigator1.Text = "coreBindingNavigator1";
        // 
        // NameTextBox
        // 
        NameTextBox.Location = new Point(21, 78);
        NameTextBox.Name = "NameTextBox";
        NameTextBox.Size = new Size(246, 27);
        NameTextBox.TabIndex = 2;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(22, 192);
        label2.Name = "label2";
        label2.Size = new Size(78, 20);
        label2.TabIndex = 3;
        label2.Text = "Command";
        // 
        // CommandTextBox
        // 
        CommandTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        CommandTextBox.Location = new Point(21, 215);
        CommandTextBox.Name = "CommandTextBox";
        CommandTextBox.Size = new Size(831, 27);
        CommandTextBox.TabIndex = 4;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(23, 121);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(38, 20);
        titleLabel.TabIndex = 5;
        titleLabel.Text = "Title";
        // 
        // TitleTextBox
        // 
        TitleTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        TitleTextBox.Location = new Point(22, 144);
        TitleTextBox.Name = "TitleTextBox";
        TitleTextBox.Size = new Size(831, 27);
        TitleTextBox.TabIndex = 6;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(23, 267);
        label4.Name = "label4";
        label4.Size = new Size(111, 20);
        label4.TabIndex = 7;
        label4.Text = "Initial Directory";
        // 
        // InitialDirectoryTextBox
        // 
        InitialDirectoryTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        InitialDirectoryTextBox.Location = new Point(21, 290);
        InitialDirectoryTextBox.Name = "InitialDirectoryTextBox";
        InitialDirectoryTextBox.Size = new Size(831, 27);
        InitialDirectoryTextBox.TabIndex = 8;
        // 
        // OpenSourceFileButton
        // 
        OpenSourceFileButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        OpenSourceFileButton.Location = new Point(609, 324);
        OpenSourceFileButton.Name = "OpenSourceFileButton";
        OpenSourceFileButton.Size = new Size(244, 29);
        OpenSourceFileButton.TabIndex = 9;
        OpenSourceFileButton.Text = "Open source file";
        OpenSourceFileButton.UseVisualStyleBackColor = true;
        OpenSourceFileButton.Click += OpenSourceFileButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(877, 365);
        Controls.Add(OpenSourceFileButton);
        Controls.Add(InitialDirectoryTextBox);
        Controls.Add(label4);
        Controls.Add(TitleTextBox);
        Controls.Add(titleLabel);
        Controls.Add(CommandTextBox);
        Controls.Add(label2);
        Controls.Add(NameTextBox);
        Controls.Add(BindingNavigator1);
        Controls.Add(label1);
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "External Tools";
        (BindingNavigator1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Classes.CoreBindingNavigator BindingNavigator1;
    private TextBox NameTextBox;
    private Label label2;
    private TextBox CommandTextBox;
    private Label titleLabel;
    private TextBox TitleTextBox;
    private Label label4;
    private TextBox InitialDirectoryTextBox;
    private Button OpenSourceFileButton;
}
