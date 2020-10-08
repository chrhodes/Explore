using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.Presentation.Friend
{
    public class Common
    {
        public const string PROJECT_NAME = "FriendOrganizer_Presentation_Friend";
        public const string LOG_APPNAME = "FriendOrganizer_Presentation_Friend";
        public const string APPNAME = "FriendOrganizer_Presentation_Friend";

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
