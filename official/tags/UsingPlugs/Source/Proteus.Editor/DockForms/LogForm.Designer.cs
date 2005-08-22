namespace Proteus.Editor.DockForms
{
    partial class LogForm
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
            this.controlSink1 = new Proteus.Kernel.Diagnostics.ControlSink();
            this.SuspendLayout();
            // 
            // controlSink1
            // 
            this.controlSink1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSink1.Location = new System.Drawing.Point(0, 0);
            this.controlSink1.Name = "controlSink1";
            this.controlSink1.Size = new System.Drawing.Size(521, 186);
            this.controlSink1.TabIndex = 0;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 186);
            this.Controls.Add(this.controlSink1);
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Proteus.Kernel.Diagnostics.ControlSink controlSink1;
    }
}