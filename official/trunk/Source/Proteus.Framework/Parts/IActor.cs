using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IActor 
        :   Kernel.Configuration.IConfigureable, 
            State.IState,
            IPart,
            IDisposable   
    {
        bool                Active      { get; set; }
                      
        string              TypeName    { get; }
        string              BaseType    { get; }

        IEnvironment        Environment { get; }

        IProperty[]         Properties  { get; }

        MessagePrototype[]  Messages    { get; }

        object              SendMessage( string name,IActor sender,params object[] parameters );

        bool                Update(double deltaTime);
        bool                Initialize(IEnvironment environment);

        InterfaceType       QueryInterface<InterfaceType>() where InterfaceType : class;
    }
}
