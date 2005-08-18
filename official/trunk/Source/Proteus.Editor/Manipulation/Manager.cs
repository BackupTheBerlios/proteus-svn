using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Framework.Parts;

namespace Proteus.Editor.Manipulation
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        public delegate void    SelectionDelegate( IActor selectedActor,List<IActor> selectedActors );

        public event            SelectionDelegate SelectionChanged;

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
