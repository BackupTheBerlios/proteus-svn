namespace Proteus.Editor.DockForms
{
    partial class DiagramForm
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
            this.components = new System.ComponentModel.Container();
            this.graphControl1 = new Netron.GraphLib.UI.GraphControl();
            this.SuspendLayout();
            // 
            // graphControl1
            // 
            this.graphControl1.AllowAddConnection = true;
            this.graphControl1.AllowAddShape = true;
            this.graphControl1.AllowDeleteShape = true;
            this.graphControl1.AllowDrop = true;
            this.graphControl1.AllowMoveShape = true;
            this.graphControl1.AutomataPulse = 10;
            this.graphControl1.AutoScroll = true;
            this.graphControl1.BackgroundColor = System.Drawing.Color.White;
            this.graphControl1.BackgroundImagePath = null;
            this.graphControl1.BackgroundType = Netron.GraphLib.CanvasBackgroundType.Gradient;
            this.graphControl1.DefaultConnectionEnd = Netron.GraphLib.ConnectionEnd.NoEnds;
            this.graphControl1.DefaultConnectionPath = "Default";
            this.graphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControl1.DoTrack = false;
            this.graphControl1.EnableContextMenu = true;
            this.graphControl1.EnableLayout = true;
            this.graphControl1.EnableToolTip = true;
            this.graphControl1.FileName = null;
            this.graphControl1.GradientBottom = System.Drawing.Color.White;
            this.graphControl1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.graphControl1.GradientTop = System.Drawing.Color.LightSteelBlue;
            this.graphControl1.GraphLayoutAlgorithm = Netron.GraphLib.GraphLayoutAlgorithms.SpringEmbedder;
            this.graphControl1.GridSize = 20;
            this.graphControl1.Location = new System.Drawing.Point(0, 0);
            this.graphControl1.Name = "graphControl1";
            this.graphControl1.RestrictToCanvas = true;
            this.graphControl1.ShowAutomataController = false;
            this.graphControl1.ShowGrid = false;
            this.graphControl1.Size = new System.Drawing.Size(503, 446);
            this.graphControl1.Snap = false;
            this.graphControl1.TabIndex = 0;
            this.graphControl1.Text = "graphControl1";
            this.graphControl1.Zoom = 1F;
            // 
            // DiagramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 446);
            this.Controls.Add(this.graphControl1);
            this.Name = "DiagramForm";
            this.Text = "DiagramForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Netron.GraphLib.UI.GraphControl graphControl1;
    }
}