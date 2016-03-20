namespace WinSync.Controls
{
    partial class StatusProgressBar
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel_statusProgress = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanel_statusProgress
            // 
            this.flowLayoutPanel_statusProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel_statusProgress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel_statusProgress.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_statusProgress.Name = "flowLayoutPanel_statusProgress";
            this.flowLayoutPanel_statusProgress.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel_statusProgress.Size = new System.Drawing.Size(695, 68);
            this.flowLayoutPanel_statusProgress.TabIndex = 54;
            // 
            // StatusProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel_statusProgress);
            this.Name = "StatusProgressBar";
            this.Size = new System.Drawing.Size(695, 68);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_statusProgress;
    }
}
