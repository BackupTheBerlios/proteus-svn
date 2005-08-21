using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Manipulation
{
    internal sealed class Parser
    {
        private static string[] SplitString(string input)
        {
            List<string> parts = new List<string>();
            StringBuilder builder = new StringBuilder();
            bool inQuotes = false;
            input = input.Trim();

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '"':
                    {
                        if (inQuotes)
                        {
                            // Finish up the word.
                            parts.Add( builder.ToString() );
                            builder.Remove(0,builder.Length);
                            inQuotes = false;
                        }
                        else
                        {
                            inQuotes = true;
                        }

                        break;
                    }
                    case ' ':
                    {
                        if (!inQuotes)
                        {
                            // End previous word.
                            if (builder.Length > 0)
                            {
                                parts.Add(builder.ToString());
                                builder.Remove(0, builder.Length);
                            }
                        }
                        else
                        {
                            builder.Append(input[i]);
                        }

                        break;
                    }
                    default:
                    {
                        builder.Append(input[i]);
                        break;
                    }
                }
            }

            if (builder.Length > 0)
            {
                parts.Add( builder.ToString() );
            }

            return parts.ToArray();
        }
    }
}
