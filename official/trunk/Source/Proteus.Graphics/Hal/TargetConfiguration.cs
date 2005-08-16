using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TargetConfiguration
    {
        private List<IRenderTarget> targets = new List<IRenderTarget>();

        public bool IsValid
        {
            get { return true; }
        }

        public bool Add(RenderTextureBase renderTexture)
        {
            if (targets.Count == 1)
            {
                if ( targets[0] is FrameBuffer )
                    return false;
            }

            targets.Add( renderTexture );
            return true;
        }

        public bool Add(FrameBuffer frameBuffer)
        {
            if ( targets.Count == 0 )
            {   
                targets.Add( frameBuffer );
                return true;
            }
            return false;
        }

        public bool MakeCurrent()
        {
            return MakeCurrent(0);
        }

        public bool MakeCurrent(int surface)
        {
            int[] surfaces = new int[ targets.Count ];
            for( int i = 0; i < surfaces.Length; i++ )
                surfaces[i] = surface;

            return MakeCurrent( surfaces );
        }

        public bool MakeCurrent(int[] surfaces)
        {
            if (surfaces.Length == targets.Count)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!targets[i].SetAsTarget(i,surfaces[i]))
                        return false;
                }

                return true;
            }
            return false;
        }

        public TargetConfiguration()
        {
        }
    }
}
