using System.Collections.Generic;
using PrismDemo.Business;
using System;

namespace VNCExplore_LearnPrism_BrianLagunas.Infrastructure
{
    public interface IPersonService
    {
        IList<Person> GetPeople();
        void GetPeopleAsync(EventHandler<ServiceResult<IList<Person>>> callback);
    }
}