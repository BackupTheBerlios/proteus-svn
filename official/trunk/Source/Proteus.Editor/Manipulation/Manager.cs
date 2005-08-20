using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Framework.Parts;

namespace Proteus.Editor.Manipulation
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        public delegate void    SelectionDelegate( IActor selectedActor,List<IActor> selectedActors );
        public delegate void    ActionDelegate( IActor selectedActor );
        public delegate void    ActorDelegate( IActor actor );

        public event            ActorDelegate       ActorAdded;
        public event            ActorDelegate       ActorRemoved;
        public event            SelectionDelegate   SelectionChanged;
        public event            ActionDelegate      DefaultAction;

        private IActor          singleSelection = null;
        private List<IActor>    multiSelection  = new List<IActor>();

        public IActor SelectedActor
        {
            get { return singleSelection; }
            set 
            { 
                singleSelection = value; 
                OnSelectionChanged();
            }
        }

        public void PerformDefaultAction(IActor actor)
        {
            this.SelectedActor = actor;
            PerformDefaultAction();
        }

        public void PerformDefaultAction()
        {
            if ( DefaultAction != null && singleSelection != null )
                DefaultAction( singleSelection );
        }

        public void AfterActorAdded(IActor actor)
        {
            if ( ActorAdded != null )
                ActorAdded(actor);
        }

        public void AfterActorRemoved(IActor actor)
        {
            if ( ActorRemoved != null )
                ActorRemoved(actor);
        }

        public void ClearSelection()
        {
            multiSelection.Clear();
            OnSelectionChanged();
        }

        public void AddSelection(IActor actor)
        {
            multiSelection.Add( actor );
            OnSelectionChanged();
        }

        public void AddSelection(IActor[] actors)
        {
            multiSelection.AddRange( actors );
            OnSelectionChanged();
        }

        protected void OnSelectionChanged()
        {
            if ( SelectionChanged != null )
                SelectionChanged( singleSelection,multiSelection );
        }
    }
}
