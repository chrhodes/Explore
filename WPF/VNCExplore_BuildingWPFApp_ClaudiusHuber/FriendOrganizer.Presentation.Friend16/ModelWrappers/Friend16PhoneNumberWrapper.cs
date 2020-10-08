using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend16.ModelWrappers
{
    public class Friend16PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend16PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
