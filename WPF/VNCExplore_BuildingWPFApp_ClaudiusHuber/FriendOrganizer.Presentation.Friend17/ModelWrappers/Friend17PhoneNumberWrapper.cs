using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend17.ModelWrappers
{
    public class Friend17PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend17PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
