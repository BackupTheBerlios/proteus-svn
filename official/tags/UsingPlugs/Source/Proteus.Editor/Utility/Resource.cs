using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Proteus.Editor.Utility
{
    public sealed class Resource
    {
        public static Image GetIcon(string name)
        {
            return GetImage( "Proteus.Editor.Images.Icons." + name );
        }

        public static Image GetImage(string name)
        {
            Stream stream = typeof(Resource).Assembly.GetManifestResourceStream( name );
            if (stream != null)
            {
                Image newImage = Image.FromStream(stream);
                stream.Close();
                return newImage;
            }
            return null;
        }

        private Resource()
        {
        }
    }
}
