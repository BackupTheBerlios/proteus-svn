using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Basic
{
    [Actor("Group")]
    [Documentation("A static actor group","Stores actors that can be configured at load time.")]
    public class GroupActor : Default.CollectionActor
    {
        public GroupActor()
        {
        }
    }
}
