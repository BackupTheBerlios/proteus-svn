using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Ui
{
    public static class Form
    {
        public static FormType Create<FormType>() where FormType : System.Windows.Forms.Form, new()
        {
            FormType newForm = new FormType();
            newForm.Show();

            while (!newForm.IsHandleCreated)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            return newForm;
        }
    }
}
