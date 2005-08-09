using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Configuration
{
    public class CommandLine
    {
        private class Option
        {
            public string   shortName       = string.Empty;
            public string   longName        = string.Empty;
            public bool     isFlag          = false;
            public bool     isMandatory     = false;
            public string   documentation   = string.Empty;
        }

        private List<Option>    options     = new List<Option>();
        private string[]        arguments   = null;

        public bool Parse()
        {
            return Verify();
        }

        public ValueType GetOption<ValueType>(string name, ValueType def)
        {
            Option foundOption = FindOption(name);
            if (foundOption != null)
            {
                string optionValue = string.Empty;
                if (GetOption(foundOption, ref optionValue))
                {
                    return Reflection.Converter.Convert(optionValue, def);
                }
            }
            return def;
        }

        public bool GetFlag(string name)
        {
            Option foundOption = FindOption(name);
            if (foundOption != null)
            {
                string optionValue = string.Empty;
                return GetOption(foundOption, ref optionValue);
            }
            return false;
        }

        public void AddOption(  string shortName,
                                string longName,
                                string documentation)
        {
            AddOption(shortName, longName, documentation,false, false);
        }

        public void AddOption(  string shortName, 
                                string longName, 
                                string documentation, 
                                bool mandatory, 
                                bool isFlag)
        {
            Option option = new Option();
            option.shortName = shortName;
            option.longName = longName;
            option.documentation = documentation;
            option.isMandatory = mandatory;

            if (isFlag)
                option.isMandatory = false;

            option.isFlag = isFlag;

            if (FindOption(shortName) == null)
            {
                options.Add(option);
            }
        }

        private Option FindOption(string name)
        {
            Option foundOption = null;
            foreach (Option o in options)
            {
                if (o.shortName == name || o.longName == name)
                {
                    foundOption = o;
                    break;
                }
            }

            return foundOption;
        }

        private bool GetOption( Option option,ref string optionValue )
        {
            bool success = false;
            int index = 0;

            if ( option != null)
            {
                string[] optionStrings = GetOptionIds(option);

                // Search each argument for the id.
                int foundIndex = -1;

                for (int i = 0; i < arguments.Length; i++)
                {
                    foreach (string s in optionStrings)
                    {
                        if (arguments[i] == s)
                        {
                            success = true;
                            break;
                        }
                    }

                    if (success)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (success)
            {
                // Check if we need a flag.
                if (!option.isFlag && index + 1 < arguments.Length )
                {
                    if (!IsOptionString(arguments[index + 1]))
                    {
                        optionValue = arguments[index + 1];
                        return true;
                    }
                }
                else if (option.isFlag)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsOptionString(string testString)
        {
            foreach (Option o in options)
            {
                string[] optionStrings = GetOptionIds(o);

                foreach (string s in optionStrings)
                {
                    if (testString == s)
                        return true;
                }
            }

            return false;
        }

        private string[] GetOptionIds(Option option)
        {
            string[] optionStrings = new string[4];

            optionStrings[0] = "/" + option.shortName;
            optionStrings[1] = "-" + option.shortName;
            optionStrings[2] = "/" + option.longName;
            optionStrings[3] = "-" + option.longName;
         
            return optionStrings;
        }

        private bool Verify()
        {
            foreach (Option o in options)
            {
                if (o.isMandatory)
                {
                    string optionValue = string.Empty;
                    if ( !GetOption(o,ref optionValue ) )
                        return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Wrong or missing command line arguments:");
            builder.AppendLine();
            builder.AppendFormat("Possible command line arguments for {0}", Information.Program.Name);
            builder.AppendLine();
            builder.AppendLine();

            foreach (Option o in options)
            {
               builder.AppendFormat("-{0}[-{1}] Mandatory: {2} {3}", o.shortName, o.longName, o.isMandatory, o.documentation);
               builder.AppendLine();
            }

            return builder.ToString();
        }

        public CommandLine()
        {
            string[] commandLineParts = Environment.CommandLine.Split(new char[] { ' ' });

            if ( commandLineParts.Length > 1 )
            {
                arguments = new string[ commandLineParts.Length - 1];
                for ( int i = 1; i < commandLineParts.Length; i++ )
                {
                    arguments[i -1]= commandLineParts[i];
                }
            }
        }
    }
}
