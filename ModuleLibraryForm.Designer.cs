﻿namespace com.clusterrr.hakchi_gui
{
    partial class ModuleLibraryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleLibraryForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.updateRepoButton = new System.Windows.Forms.Button();
            this.addRepoButton = new System.Windows.Forms.Button();
            this.repositoryListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moduleListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moduleDownloadButton = new System.Windows.Forms.Button();
            this.moduleDescriptionBrowser = new System.Windows.Forms.WebBrowser();
            this.moduleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.updateRepoButton);
            this.groupBox1.Controls.Add(this.addRepoButton);
            this.groupBox1.Controls.Add(this.repositoryListBox);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // updateRepoButton
            // 
            resources.ApplyResources(this.updateRepoButton, "updateRepoButton");
            this.updateRepoButton.Name = "updateRepoButton";
            this.updateRepoButton.UseVisualStyleBackColor = true;
            this.updateRepoButton.Click += new System.EventHandler(this.updateRepoButton_Click);
            // 
            // addRepoButton
            // 
            resources.ApplyResources(this.addRepoButton, "addRepoButton");
            this.addRepoButton.Name = "addRepoButton";
            this.addRepoButton.UseVisualStyleBackColor = true;
            this.addRepoButton.Click += new System.EventHandler(this.addRepoButton_Click);
            // 
            // repositoryListBox
            // 
            this.repositoryListBox.FormattingEnabled = true;
            resources.ApplyResources(this.repositoryListBox, "repositoryListBox");
            this.repositoryListBox.Name = "repositoryListBox";
            this.repositoryListBox.SelectedIndexChanged += new System.EventHandler(this.repositoryListBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moduleListBox);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // moduleListBox
            // 
            resources.ApplyResources(this.moduleListBox, "moduleListBox");
            this.moduleListBox.FormattingEnabled = true;
            this.moduleListBox.Name = "moduleListBox";
            this.moduleListBox.SelectedIndexChanged += new System.EventHandler(this.moduleListBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.moduleLabel);
            this.panel1.Controls.Add(this.moduleDownloadButton);
            this.panel1.Controls.Add(this.moduleDescriptionBrowser);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // moduleDownloadButton
            // 
            resources.ApplyResources(this.moduleDownloadButton, "moduleDownloadButton");
            this.moduleDownloadButton.Name = "moduleDownloadButton";
            this.moduleDownloadButton.UseVisualStyleBackColor = true;
            this.moduleDownloadButton.Click += new System.EventHandler(this.moduleDownloadButton_Click);
            // 
            // moduleDescriptionBrowser
            // 
            this.moduleDescriptionBrowser.AllowWebBrowserDrop = false;
            this.moduleDescriptionBrowser.IsWebBrowserContextMenuEnabled = false;
            resources.ApplyResources(this.moduleDescriptionBrowser, "moduleDescriptionBrowser");
            this.moduleDescriptionBrowser.Name = "moduleDescriptionBrowser";
            this.moduleDescriptionBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // moduleLabel
            // 
            resources.ApplyResources(this.moduleLabel, "moduleLabel");
            this.moduleLabel.Name = "moduleLabel";
            // 
            // ModuleLibraryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ModuleLibraryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleLibraryForm_FormClosing);
            this.Load += new System.EventHandler(this.ModuleLibraryForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addRepoButton;
        private System.Windows.Forms.ListBox repositoryListBox;
        private System.Windows.Forms.ListBox moduleListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button moduleDownloadButton;
        private System.Windows.Forms.WebBrowser moduleDescriptionBrowser;
        private System.Windows.Forms.Button updateRepoButton;
        private System.Windows.Forms.Label moduleLabel;
    }
}