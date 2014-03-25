using System;
using System.Drawing;

using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Keyboard.iOS
{
    [Register("KeyboardView")]
    public class KeyboardView : UIViewController
    {
        private const int fieldX = 20;
        private const int fieldWidth = 280;
        private const int fieldHeight = 40;
        private UILabel header;
        private UITextField firstName;
        private UITextField lastName;
        private UITextField address1;
        private UITextField address2;
        private UITextField city;
        private UITextField state;
        private UITextField zip;
        private readonly KeyboardHandler kbHandler;
        private NSObject keyboardUp;
        private NSObject keyboardDown;

        public KeyboardView()
        {
            
            kbHandler = new KeyboardHandler();


            header = new UILabel(new RectangleF(75, 50, 100, 40));
            header.Text = "CTS Form";
            InitializeTextField(out firstName, "First Name");
            InitializeTextField(out lastName, "Last Name", y: 200);
            InitializeTextField(out address1, "Address 1", y: 250);
            InitializeTextField(out address2, "Address 2", y: 300);
            InitializeTextField(out city, "City", y: 350);
            InitializeTextField(out state, "State", y: 400, width: 100);
            InitializeTextField(out zip, "Zip", x: 130, y: 400, width: 170);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //this.ParentViewController.View.BackgroundColor = UIColor.White;
            View.Frame = UIScreen.MainScreen.Bounds;
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth |
                UIViewAutoresizing.FlexibleHeight;

            firstName.InputAccessoryView = new EnhancedToolbar(firstName, null, lastName);
            lastName.InputAccessoryView = new EnhancedToolbar(lastName, firstName, address1);
            address1.InputAccessoryView = new EnhancedToolbar(address1, lastName, address2);
            address2.InputAccessoryView = new EnhancedToolbar(address2, address1, city);
            city.InputAccessoryView = new EnhancedToolbar(city, address2, state);
            state.InputAccessoryView = new EnhancedToolbar(state, city, zip);
            zip.InputAccessoryView = new EnhancedToolbar(zip, state, null);
            zip.KeyboardType = UIKeyboardType.NumberPad;

            View.AddSubview(header);
            View.AddSubview(firstName);
            View.AddSubview(lastName);
            View.AddSubview(address1);
            View.AddSubview(address2);
            View.AddSubview(city);
            View.AddSubview(state);
            View.AddSubview(zip);

            var g = new UITapGestureRecognizer(() =>
            {
                firstName.ResignFirstResponder();
                lastName.ResignFirstResponder();
                address1.ResignFirstResponder();
                address2.ResignFirstResponder();
                city.ResignFirstResponder();
                state.ResignFirstResponder();
                zip.ResignFirstResponder();
            });
            this.View.AddGestureRecognizer(g);

        }

        private void InitializeTextField(out UITextField field, string placeholder, int x = fieldX, int y = 150, int width = fieldWidth, int height = fieldHeight)
        {
            field = new UITextField()
            {
                Frame = new RectangleF(x, y, width, height),
                Placeholder = placeholder,
                BackgroundColor = UIColor.White
            };
            field.Layer.BorderColor = new CGColor(0.8f, 0.8f, 0.8f);
            field.Layer.BorderWidth = 1.0f;
            field.Layer.CornerRadius = 5.0f;
            //These two lines set the left padding
            field.LeftView = new UIView(new RectangleF(0, 0, 10, 40));
            field.LeftViewMode = UITextFieldViewMode.Always;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            kbHandler.View = this.View;
            keyboardUp = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, kbHandler.KeyboardUpNotification);
            keyboardDown = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, kbHandler.KeyboardDownNotification);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            if (keyboardUp != null && keyboardDown != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardUp);
                NSNotificationCenter.DefaultCenter.RemoveObserver(keyboardDown);
            }
        }
    }
}