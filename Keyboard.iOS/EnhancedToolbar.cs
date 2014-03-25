using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Keyboard.iOS
{
    public class EnhancedToolbar : UIToolbar
    {
        public UITextField prevTextFieldOrView { get; set; }
        public UITextField currentTextFieldOrView { get; set; }
        public UITextField nextTextFieldOrView { get; set; }

        public EnhancedToolbar()
            : base()
        { }

        public EnhancedToolbar(UITextField current, UITextField previous, UITextField next)
        {
            this.currentTextFieldOrView = current;
            this.prevTextFieldOrView = previous;
            this.nextTextFieldOrView = next;
            SetupToolbar();
        }

        void SetupToolbar()
        {
            Frame = new RectangleF(0.0f, 0.0f, 320, 44.0f);
            TintColor = UIColor.DarkGray;
            Translucent = false;
            Items = new UIBarButtonItem[]
            {
                new UIBarButtonItem("Prev", UIBarButtonItemStyle.Bordered, delegate
                {
                    prevTextFieldOrView.BecomeFirstResponder();
                }) { Enabled = prevTextFieldOrView != null }, 
                new UIBarButtonItem("Next", UIBarButtonItemStyle.Bordered, delegate
                {
                    nextTextFieldOrView.BecomeFirstResponder();
                }) { Enabled = nextTextFieldOrView != null }, 
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), 
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    currentTextFieldOrView.ResignFirstResponder();
                }) 
            };

        }
    }
}