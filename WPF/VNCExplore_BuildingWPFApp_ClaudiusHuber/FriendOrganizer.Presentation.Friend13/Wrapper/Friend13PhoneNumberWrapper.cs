using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend13.Wrapper
{
    public class Friend13PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend13PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
