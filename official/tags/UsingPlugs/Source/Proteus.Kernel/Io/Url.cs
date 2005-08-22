using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Proteus.Kernel.Io
{
    public static class Url
    {
        private static Regex urlRegex = null;

        public static bool IsAbsolute(string url)
        {
            if (IsValid(url) && url.StartsWith("/"))
                return true;

            return false;
        }

        public static bool IsRelative(string url)
        {
            if (IsValid(url) && !url.StartsWith("/"))
                return true;

            return false;
        }

        public static bool IsDirectory(string url)
        {
            if (IsValid(url) && !url.Contains("."))
                return true;

            return false;
        }

        public static bool IsFilename(string url)
        {
            if (IsValid(url) && url.Contains("."))
                return true;

            return false;
        }

        public static string GetExtension(string url)
        {
            if (IsFilename(url))
            {
                string[] parts = url.Split(new char[] { '.' });
                return parts[1];
            }
            return string.Empty;
        }

        public static string GetFilename(string url)
        {
            if (IsFilename(url) )
            {
                if (url.Contains("/"))
                {
                    string[] parts = url.Split(new char[] { '/' });
                    return parts[parts.Length - 1];
                }
                return url;
            }
            return string.Empty;
        }

        public static string GetDirectory(string url)
        {
            if (IsFilename(url) )
            {
                string filename = GetFilename(url);
                return url.Substring(0, url.Length - filename.Length);
            }
            return url;
        }

        public static bool IsValid(string url)
        {
            Match match = urlRegex.Match(url);

            if (url == string.Empty || url == "/")
                return true;

            if (match.Success)
            {
                if (match.Length == url.Length)
                {
                    // Special case checks ( TODO: Fix the regex ) 
                    if (!url.Contains("//"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static Url()
        {
            urlRegex = new Regex(
                                @"/?(\w+/?)*(.\w+)*",
                                RegexOptions.IgnoreCase
                                | RegexOptions.Multiline
                                | RegexOptions.IgnorePatternWhitespace
                                | RegexOptions.Compiled
                                );
        }
    }
}
