using System;
using System.Collections.Generic;
using System.Text;

namespace EACApplicationTemplateCSharp1
{
    public class ScreenSwapEventArgs : EventArgs
    {
        public User_Interface.ucScreenBase FromScreen;
        // We don't want to pass an instance of a type, we want to pass a type to be loaded.
        //public User_Interface.ucSwapScreenBase2 ToScreenType;
        // How to make this a User_Interface.ucSwapScreenBase2 type?
        public Type ToScreenType;

        public ScreenSwapEventArgs(User_Interface.ucScreenBase fromScreen, Type toScreenType)
        {
            FromScreen = fromScreen;
            ToScreenType = toScreenType;
        }
    }
}
