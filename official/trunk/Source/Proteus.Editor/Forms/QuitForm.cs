using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.Forms
{
    public partial class QuitForm : Form
    {
        private bool quitRequested = false;
        private bool saveDocuments = true;

        public bool QuitRequested
        {
            get { return quitRequested; }
            set { quitRequested = value; }
        }

        public bool SaveDocuments
        {
            get { return saveDocuments; }
            set { saveDocuments = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            quitRequested = true;
            saveDocuments = checkBox1.Checked;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            quitRequested = false;
            saveDocuments = checkBox1.Checked;
            this.Close();
        }

        public static bool CheckForQuit()
        {
            QuitForm newForm = new QuitForm();
            newForm.ShowDialog();

            // Check for document saving
            if (Documents.Manager.Instance.IsDirty && 
                newForm.SaveDocuments &&
                newForm.QuitRequested )
            {
                Documents.Manager.Instance.SaveAll();
            }

            return newForm.QuitRequested;
        }
        
        public QuitForm()
        {
            InitializeComponent();

            // Setup data
            if (!Documents.Manager.Instance.IsDirty)
            {
                checkBox1.Visible = false;
                label2.Visible = false;
            }
        }
    }
}