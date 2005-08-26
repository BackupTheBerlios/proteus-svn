using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Dp = WeifenLuo.WinFormsUI;

namespace Proteus.Editor.DockForms
{
    public partial class DockableForm : Dp.DockContent
    {   
        protected Type              dockTargetType      = null;
        protected DragDropEffects   dockTargetEffect    = DragDropEffects.Copy;
        protected object            dockLastDragged     = null;
        protected bool              dockIsSource        = false;

        public virtual Dp.DockState DefaultDockState
        {
            get { return Dp.DockState.DockLeft; }
        }

        private void DockableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.Instance.Remove(this);
        }

        protected virtual void OnDropReceived(object data, int x, int y, int keystate)
        {
        }

        protected virtual object OnDragRequest(int x, int y, MouseButtons buttons)
        {
            return null;
        }

        protected virtual object OnDragRequest(MouseButtons buttons, object item)
        {
            return null;
        }

        protected virtual void OnDragComplete(object dataObject)
        {           
        }

        protected void ActivateDrag()
        {
            ActivateDragDrop( true,null,DragDropEffects.None );
        }

        protected void ActivateDrop(Type targetType, DragDropEffects effect)
        {
            ActivateDragDrop(false,targetType,effect);
        }

        protected void ActivateDragDrop(bool isSource, Type targetType,DragDropEffects effect )
        {
            if (targetType != null)
            {
                dockTargetType      = targetType;
                dockTargetEffect    = effect;
                this.AllowDrop      = true;
            }

            if (isSource)
            {
                dockIsSource = true;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            // Add event handlers.
            if (!(e.Control is TreeView))
            {
                e.Control.MouseDown += new MouseEventHandler(Control_MouseDown);
            }
            else
            {
                TreeView tree = (TreeView)e.Control;
                tree.ItemDrag += new ItemDragEventHandler(tree_ItemDrag);
            }

            e.Control.QueryContinueDrag += new QueryContinueDragEventHandler(Control_QueryContinueDrag);
            e.Control.DragEnter += new DragEventHandler(Control_DragEnter);
            e.Control.DragDrop += new DragEventHandler(Control_DragDrop);
        }

        private void tree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.OnItemDrag( e );
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            this.OnDragDrop( e );
        }

        private void Control_DragEnter(object sender, DragEventArgs e)
        {
            this.OnDragEnter( e );
        }

        private void Control_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            this.OnQueryContinueDrag( e );
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        protected virtual void OnItemDrag(ItemDragEventArgs e)
        {
            if (dockIsSource)
            {
                object dataObject = OnDragRequest(e.Button,e.Item);

                if (dataObject != null)
                {
                    dockLastDragged = dataObject;
                    this.DoDragDrop(dataObject, DragDropEffects.All);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (dockIsSource)
            {
                object dataObject = OnDragRequest(e.X, e.Y, e.Button);

                if (dataObject != null)
                {
                    dockLastDragged = dataObject;
                    this.DoDragDrop(dataObject, DragDropEffects.All);
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            if (dockIsSource)
            {
                if (qcdevent.Action == DragAction.Drop && dockLastDragged != null)
                {
                    OnDragComplete(dockLastDragged);
                    dockLastDragged = null;
                }
            }

            base.OnQueryContinueDrag(qcdevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (this.AllowDrop && dockTargetType != null )
            {
                if (drgevent.Data.GetDataPresent(dockTargetType))
                {
                    // We got correct data.
                    drgevent.Effect = dockTargetEffect;
                }
                else
                {
                    drgevent.Effect = DragDropEffects.None;
                }
            }

            base.OnDragEnter( drgevent );
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (this.AllowDrop && dockTargetType != null)
            {
                if (drgevent.Data.GetDataPresent(dockTargetType))
                {
                    object dataObject = drgevent.Data.GetData(dockTargetType);

                    // Call method
                    OnDropReceived(dataObject, drgevent.X, drgevent.Y, drgevent.KeyState);
                }
            }

            base.OnDragDrop( drgevent );
        }

        protected DockableForm()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(DockableForm_FormClosing);
        }  
    }
}