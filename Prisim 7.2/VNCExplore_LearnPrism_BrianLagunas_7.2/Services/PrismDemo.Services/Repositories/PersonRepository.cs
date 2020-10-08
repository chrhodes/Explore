﻿using System;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace PrismDemo.DomainServices.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        // Use this to show we are getting the same instance of service.
        int count = 0;

        public PersonRepository()
        {
        }

        public int SavePerson(Business.Person person)
        {
            count++;
            person.LastUpdated = DateTime.Now;
            return count;
        }
    }
}
