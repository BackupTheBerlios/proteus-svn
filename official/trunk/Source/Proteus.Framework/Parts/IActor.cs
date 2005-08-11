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
        Type[]              Interfaces  { get; }

        IInputPlug[]        GetInputPlugs(IActor outputActor, IOutputPlug outputPlug);
        IOutputPlug[]       GetOutputPlugs(IActor inputActor, IInputPlug inputPlug);

        bool                Update(double deltaTime);
        bool                Initialize(IEnvironment environment);

        object              QueryInterface(Type interfaceType);
        InterfaceType       QueryInterface<InterfaceType>();
    }
}
