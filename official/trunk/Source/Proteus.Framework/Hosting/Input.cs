using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Proteus.Framework.Hosting
{
    public class Input
    {
        private         SortedList                      inputObjects    = new SortedList();
        private static  Kernel.Diagnostics.Log<Input>   log             = new Kernel.Diagnostics.Log<Input>();

        public enum InputType
        {
            MainWindow,
            Name,
            Width,
            Height,
        }

        public object this[ InputType type ]
        {
            get 
            {
                if (inputObjects.ContainsKey(type))
                {
                    return inputObjects[type];
                }
                else
                {
                    log.Warning("Engine hosting input: {0} not set.", type);
                    return null;
                }
            }
            set { inputObjects[type] = value; }
        }
    }
}
