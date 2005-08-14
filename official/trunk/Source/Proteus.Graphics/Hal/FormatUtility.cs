using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public class FormatUtility
    {
        private static SortedList<D3d.Format,int> formatSizes = 
                   new SortedList<D3d.Format,int>();

        public static int GetSize(D3d.Format format)
        {
            if ( formatSizes.ContainsKey(format) )
                return formatSizes[format];

            return 0;
        }

        private FormatUtility()
        {
        }

        static FormatUtility()
        {
            formatSizes.Add( D3d.Format.A16B16G16R16,64 );
            formatSizes.Add( D3d.Format.A16B16G16R16F,64 );
            formatSizes.Add( D3d.Format.A1R5G5B5,16 );

        }
    }
}
