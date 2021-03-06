﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace MULTIPLAYER.UI.Extensions
{
    public static class ControlExtensions
    {
        public static IEnumerable<Control> All(this Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                foreach (Control grandChild in control.Controls.All())
                    yield return grandChild;

                yield return control;
            }
        }
    }
}