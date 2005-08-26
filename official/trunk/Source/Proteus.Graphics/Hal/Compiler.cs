using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Compiler : Kernel.Pattern.Disposable
    {
        private D3d.EffectCompiler  d3dCompiler = null;
        private Device              d3dDevice   = null;
        
        protected override void ReleaseManaged()
        {
        }

        protected override void ReleaseUnmanaged()
        {
        }

        private bool Initialize(Device _device)
        {
         

            return true;
        }

        public static Compiler Create(Device _device)
        {
            Compiler compiler = new Compiler();
            if ( compiler.Initialize( _device ) )
                return compiler;

            return null;
        }

        private Compiler()
        {
        }
    }
}
