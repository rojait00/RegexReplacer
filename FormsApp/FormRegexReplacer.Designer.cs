namespace RegexReplacer.FormsApp
{
    partial class FormRegexReplacer
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
            this.tbInput = new System.Windows.Forms.RichTextBox();
            this.tbOutput = new System.Windows.Forms.RichTextBox();
            this.btnManageRules = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.comboBoxRuleSets = new System.Windows.Forms.ComboBox();
            this.btnReload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInput.Location = new System.Drawing.Point(3, 3);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(250, 386);
            this.tbInput.TabIndex = 0;
            this.tbInput.Text = "";
            this.tbInput.TextChanged += new System.EventHandler(this.InputChangedEventHandler);
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(3, 3);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(504, 386);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "";
            // 
            // btnManageRules
            // 
            this.btnManageRules.Location = new System.Drawing.Point(12, 16);
            this.btnManageRules.Name = "btnManageRules";
            this.btnManageRules.Size = new System.Drawing.Size(135, 23);
            this.btnManageRules.TabIndex = 2;
            this.btnManageRules.Text = "Manage Rules";
            this.btnManageRules.UseVisualStyleBackColor = true;
            this.btnManageRules.Click += new System.EventHandler(this.BtnManageRules_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 46);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tbInput);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tbOutput);
            this.splitContainer.Size = new System.Drawing.Size(770, 392);
            this.splitContainer.SplitterDistance = 256;
            this.splitContainer.TabIndex = 3;
            // 
            // comboBoxRuleSets
            // 
            this.comboBoxRuleSets.FormattingEnabled = true;
            this.comboBoxRuleSets.Location = new System.Drawing.Point(153, 16);
            this.comboBoxRuleSets.Name = "comboBoxRuleSets";
            this.comboBoxRuleSets.Size = new System.Drawing.Size(132, 23);
            this.comboBoxRuleSets.TabIndex = 1;
            this.comboBoxRuleSets.SelectedIndexChanged += new System.EventHandler(this.InputChangedEventHandler);
            this.comboBoxRuleSets.TextChanged += new System.EventHandler(this.InputChangedEventHandler);
            this.comboBoxRuleSets.Leave += new System.EventHandler(this.InputChangedEventHandler);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(291, 16);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(44, 23);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Load";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // FormRegexReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 450);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.comboBoxRuleSets);
            this.Controls.Add(this.btnManageRules);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(360, 150);
            this.Name = "FormRegexReplacer";
            this.Text = "RegexReplacer";
            this.Load += new System.EventHandler(this.Form_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox tbInput;
        private RichTextBox tbOutput;
        private Button btnManageRules;
        private SplitContainer splitContainer;
        private ComboBox comboBoxRuleSets;
        private Button btnReload;
    }
}