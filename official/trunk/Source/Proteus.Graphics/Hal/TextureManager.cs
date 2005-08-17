using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TextureManager : Kernel.Pattern.Disposable
    {
        private Device                  textureDevice   = null;
        private int                     textureDivider  = 1;
        private List<TextureBase>       textures        = new List<TextureBase>();
        private List<RenderTextureBase> textureTargets  = new List<RenderTextureBase>();

        public Device Device
        {
            get { return textureDevice; }
        }

        public Texture2d CreateTexture2d(D3d.Format format, int width, int height, bool dynamic,bool mipmap )
        {
            int depth = 128;

            if (ModifyParameters(D3d.ResourceType.Textures, format, dynamic, mipmap, false, ref width, ref height, ref depth))
            {
                Texture2d newTexture = Texture2d.Create(this, format, width, height, dynamic, mipmap);
                if (newTexture != null)
                {
                    textures.Add(newTexture);
                    return newTexture;
                }
            }

            return null;
        }

        public TextureCube CreateTextureCube(D3d.Format format, int size, bool dynamic, bool mipmap)
        {
            int height = 128;
            int depth = 128;

            if (ModifyParameters(D3d.ResourceType.CubeTexture, format, dynamic, mipmap, false, ref  size, ref height, ref depth))
            {
                TextureCube newTexture = TextureCube.Create(this, format, size, dynamic, mipmap);
                if (newTexture != null)
                {
                    textures.Add(newTexture);
                    return newTexture;
                }
            }

            return null;
        }

        public TextureVolume CreateTextureVolume(D3d.Format format, int width, int height, int depth, bool dynamic, bool mipmap)
        {
            if (ModifyParameters(D3d.ResourceType.VolumeTexture, format, dynamic, mipmap, false, ref width, ref height, ref depth))
            {
                TextureVolume newTexture = TextureVolume.Create(this, format, width, height, depth, dynamic, mipmap);
                if (newTexture != null)
                {
                    textures.Add(newTexture);
                    return newTexture;
                }
            }

            return null;
        }

        public RenderTexture2d CreateRenderTexture2d(D3d.Format format, int width, int height, bool mipmap, int multisample)
        {
            int depth = 128;
            if (ModifyParameters(D3d.ResourceType.Textures, format, false, mipmap, true, ref width, ref height, ref depth))
            {
                RenderTexture2d newTexture = RenderTexture2d.Create(this, format, width, height, mipmap,multisample );
                if (newTexture != null)
                {
                    textures.Add(newTexture);
                    textureTargets.Add(newTexture);
                    return newTexture;
                }
            }

            return null;
        }

        public RenderTextureCube CreateRenderTextureCube(D3d.Format format, int size, bool mipmap, int multisample)
        {
            int height  = 128;
            int depth   = 128;
            if (ModifyParameters(D3d.ResourceType.CubeTexture, format, false, mipmap, true, ref size, ref height, ref depth))
            {
                RenderTextureCube newTexture = RenderTextureCube.Create(this, format, size, mipmap, multisample);
                if (newTexture != null)
                {
                    textures.Add(newTexture);
                    textureTargets.Add(newTexture);
                    return newTexture;
                }
            }

            return null;
        }

        public DepthMap2d CreateDepthMap2d(int width, int height, bool mipmap, bool pcf)
        {
            return null;
        }

        public DepthMapCube CreateDepthCube(int size, bool mipmap, bool pcf)
        {
            return null;
        }

        public bool SetAsSampler(ITexture texture,int channel)
        {
            textureDevice.D3dDevice.SetTexture( channel,texture.BaseTexture );
            return true;
        }

        public D3d.Usage GetUsageFlags(bool dynamic, bool mipmap, bool isTarget)
        {
            D3d.Usage usageFlags = D3d.Usage.WriteOnly;

            if (dynamic)
                usageFlags |= D3d.Usage.Dynamic;

            if (mipmap)
                usageFlags |= D3d.Usage.AutoGenerateMipMap;

            if (isTarget)
                usageFlags |= D3d.Usage.RenderTarget;

            return usageFlags;
        }

        public int GetMipLevelCount(bool mipmap)
        {
            if (mipmap)
                return 0;
            return 1;
        }

        private bool ModifyParameters(  D3d.ResourceType type,
                                        D3d.Format format,
                                        bool dynamic,
                                        bool mipmap,
                                        bool isTarget,
                                        ref int width,
                                        ref int height,
                                        ref int depth )
        {
            // First fix any dimensional errors.
            width   = width     / textureDivider;
            height  = height    / textureDivider;
            depth   = depth     / textureDivider;

            // Make sure we stay within maximum allowed dimensions.
            if ( width > textureDevice.Capabilities.textureMaxWidth )
                width = textureDevice.Capabilities.textureMaxWidth;
           
            if ( height > textureDevice.Capabilities.textureMaxHeight )
                height = textureDevice.Capabilities.textureMaxHeight;

            if ( depth > textureDevice.Capabilities.textureMaxDepth )
                depth = textureDevice.Capabilities.textureMaxDepth;

            D3d.Usage d3dUsage = GetUsageFlags( dynamic,mipmap,isTarget );

            // Get the current display mode format for later use.
            D3d.Format displayFormat = D3d.Manager.Adapters[ Device.Settings.adapterIndex ].CurrentDisplayMode.Format;

            if (D3d.Manager.CheckDeviceFormat(  Device.Settings.adapterIndex,
                                                Device.Settings.deviceType,
                                                Device.Settings.backBufferFormat,
                                                d3dUsage, type, format))
            {
                if (isTarget)
                {
                    if (D3d.Manager.CheckDepthStencilMatch(Device.Settings.adapterIndex,
                                                            Device.Settings.deviceType,
                                                            displayFormat,
                                                            format,
                                                            Device.Settings.depthBufferFormat))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        protected override void ReleaseManaged()
        {
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public static TextureManager Create(Device _device)
        {
            TextureManager textureManager = new TextureManager();
            if (textureManager.Initialize(_device))
                return textureManager;

            return null;
        }

        private bool Initialize(Device device)
        {
            this.textureDevice = device;
            return true;
        }

        private TextureManager()
        {
        }     
    }
}
