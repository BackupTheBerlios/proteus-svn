using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Device : Kernel.Pattern.Disposable 
    {
        private D3d.Device d3dDevice = null;

        protected override void ReleaseManaged()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void ReleaseUnmanaged()
        {
            throw new Exception("The method or operation is not implemented.");
        }
}
}
