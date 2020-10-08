using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend15.ModelWrappers
{
    public class Friend15PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend15PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
