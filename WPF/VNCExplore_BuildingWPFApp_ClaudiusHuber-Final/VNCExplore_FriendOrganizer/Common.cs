using System;
using System.Security.Principal;

namespace VNCExplore.FriendOrganizer
{
    public class Common
    {
        public const string PROJECT_NAME = "VNCExplore_FriendOrganizer";
        public const string LOG_APPNAME = "VNCExplore_FriendOrganizer";
        public const string APPNAME = "VNCExplore_FriendOrganizer";

        //public const string cCONFIG_FILE = @"C:\temp\CodeCommandConsole_Config.xml";

        public static event EventHandler AutoHideGroupSpeedChanged;

        private static int _AutoHideGroupSpeed = 250;

        public static int AutoHideGroupSpeed
        {
            get { return _AutoHideGroupSpeed; }
            set
            {
                _AutoHideGroupSpeed = value;

                EventHandler evt = AutoHideGroupSpeedChanged;

                if (evt != null)
                {
                    evt(null, EventArgs.Empty); ;
                }
            }
        }

        //public static void RaiseAutoHideGroupSpeedChanged()
        //{
        //    EventHandler evt = AutoHideGroupSpeedChanged;

        //    if (evt != null)
        //    {
        //        evt(null, EventArgs.Empty); ;
        //    }
        //}



        public static IPrincipal CurrentUser
        {
            get;
            set;
        }

        public static bool IsAdministrator { get; set; }
        public static bool IsBetaUser { get; set; }
        public static bool IsDeveloper { get; set; }
        //public static bool IsAdvancedUser { get; set; }

    }
}
