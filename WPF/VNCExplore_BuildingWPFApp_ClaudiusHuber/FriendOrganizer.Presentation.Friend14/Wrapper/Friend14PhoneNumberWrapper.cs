using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend14.Wrapper
{
    public class Friend14PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend14PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
