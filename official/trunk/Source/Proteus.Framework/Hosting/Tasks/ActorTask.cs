using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Hosting.Tasks
{
    public sealed class ActorTask : Kernel.Pattern.Disposable,ITask
    {
        private Parts.Basic.RootActor rootActor = null;

        #region ITask Members

        public bool Initialize( Engine engine )
        {
            rootActor = new Parts.Basic.RootActor(engine);
            rootActor.Url = Kernel.Registry.Manager.Instance.GetValue("Framework.RootFile",(string)engine.Input[Input.InputType.Name] + ".actor" );
            rootActor.ForceRead();
            
            return true;
        }

        public bool Update(double deltaTime)
        {
            return rootActor.Update(deltaTime);
        }

        #endregion     
    
        protected override void ReleaseManaged()
        {
            rootActor.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }
    }
}
