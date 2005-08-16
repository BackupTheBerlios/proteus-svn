using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Shading
{
    public sealed class Parameter
    {
        public enum Scope
        {
            Scene,
            Instance,
            Material,
        }

        public enum Type
        {
            Float,
            Vector2,
            Vector3,
            Vector4,
            Matrix3,
            Matrix4,
        }

        public enum Source
        {
        }

        public enum Mode
        {
            Default,
            Inverse,
        }
    }
}
