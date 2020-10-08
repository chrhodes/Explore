using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.ModelWrappers
{
    public class Friend19PhoneNumberWrapper : ModelWrapper<FriendPhoneNumber13>
    {
        public Friend19PhoneNumberWrapper(FriendPhoneNumber13 model) : base(model)
        {
        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }      
    }
}
