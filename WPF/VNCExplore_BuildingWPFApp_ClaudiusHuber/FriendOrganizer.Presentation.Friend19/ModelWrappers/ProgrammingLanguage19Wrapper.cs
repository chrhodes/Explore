using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer.Domain;
using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.ModelWrappers
{
    public class ProgrammingLanguage19Wrapper : ModelWrapper<ProgrammingLanguage12>
    {
        public ProgrammingLanguage19Wrapper(ProgrammingLanguage12 model) 
            : base(model)
        {
        }

        public int Id { get { return Model.Id; } }


        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }       
    }
}
