using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend18.ModelWrappers
{
    public class Friend18PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend18PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
