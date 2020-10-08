using System;

using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.ModelWrappers
{
    public class Meeting19Wrapper : ModelWrapper<Meeting19>
    {
        public Meeting19Wrapper(Meeting19 model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime DateFrom
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);

                if (DateFrom > DateTo)
                {
                    DateTo = DateFrom;
                }
            }
        }

        public DateTime DateTo
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);

                if (DateTo < DateFrom)
                {
                    DateFrom = DateTo;
                }
            }
        }

    }
}
