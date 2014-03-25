using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Keyboard.iOS
{
    public class StatePickerModel : UIPickerViewModel
    {
        public IList<string> states { get; set; }
        public event EventHandler PickerChanged;
        public StatePickerModel()
        {
            states = new List<string>() { "AL", "AK", "AZ", "AR",
                "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL",
                "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI",
                "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM",
                "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI",
                "WY" 
            };
        }
    
        public override int GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override int GetRowsInComponent(UIPickerView picker,
            int component)
        {
            return states.Count;
        }

        public override string GetTitle(UIPickerView picker, int
            row, int component)
        {
            return states[row].ToString();
        }

        public override float GetRowHeight(UIPickerView picker, int
            component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView picker, int row,
            int component)
        {
            if (this.PickerChanged != null)
            {
                this.PickerChanged(this, new PickerChangedEventArgs   
                { SelectedValue = states[row] });
            }
        }

        class PickerChangedEventArgs : EventArgs
        {
            public object SelectedValue { get; set; }
        }
    }
}