﻿using System;
using System.Collections.Generic;
using FriendOrganizer.Domain;

using VNC.Core.Mvvm;

namespace FriendOrganizer.UI.Wrapper
{
    public class Friend10BWrapper : ModelWrapper<Friend05>
    {
        public Friend10BWrapper(Friend05 model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robots are not valid friends";
                    }
                    break;
            }
        }
    }
}