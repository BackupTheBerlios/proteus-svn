using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Proteus.Editor.Utility
{
    public sealed class Resource
    {
        private static System.Windows.Forms.ImageList imageList = null;

        public static Image GetIcon(string name)
        {
            Image newImage = GetImage( "Proteus.Editor.Images.Icons." + name );
            if (newImage != null)
            {
                imageList.Images.Add( newImage );
                return imageList.Images[ imageList.Images.Count - 1 ];
            }
            return null;
        }

        public static Image GetImage(string name)
        {
            Stream stream = typeof(Resource).Assembly.GetManifestResourceStream( name );
            if (stream != null)
            {
                Image newImage = Image.FromStream(stream);
                //stream.Close();
                return newImage;
            }
            return null;
        }

        private Resource()
        {
        }

        static Resource()
        {
            imageList = new System.Windows.Forms.ImageList();
            imageList.ImageSize = new Size( 16,15 );
            imageList.TransparentColor = Color.FromArgb(255,255,0,255 );
        }
    }
}
