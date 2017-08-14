namespace APD.Client.Win
{
    partial class Main
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
            this.btnAddAction = new System.Windows.Forms.Button();
            this.btnRunActions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddAction
            // 
            this.btnAddAction.Location = new System.Drawing.Point(466, 12);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(75, 23);
            this.btnAddAction.TabIndex = 0;
            this.btnAddAction.Text = "Add Action";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            // 
            // btnRunActions
            // 
            this.btnRunActions.Location = new System.Drawing.Point(466, 55);
            this.btnRunActions.Name = "btnRunActions";
            this.btnRunActions.Size = new System.Drawing.Size(75, 23);
            this.btnRunActions.TabIndex = 1;
            this.btnRunActions.Text = "Run Actions";
            this.btnRunActions.UseVisualStyleBackColor = true;
            this.btnRunActions.Click += new System.EventHandler(this.btnRunActions_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 348);
            this.Controls.Add(this.btnRunActions);
            this.Controls.Add(this.btnAddAction);
            this.Name = "Main";
            this.Text = "Action Test Harness";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.Button btnRunActions;
    }
}

