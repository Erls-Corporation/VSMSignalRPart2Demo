using System;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using VSMSignalRPart2Demo.Models;

namespace VSMSignalRPart2Demo.Hubs
{
    public class PersonHub : Hub
    {
        public void GetAll()
        {
            using (var context = new PersonContext())
            {
                var people = context.People.ToArray();
                Clients.Caller.allPeopleRetrieved(people);
            }
        }

        public bool Add(Person newPerson)
        {
            bool result = false;

            try
            {
                using (var context = new PersonContext())
                {
                    var person = context.People.Create();
                    person.FirstName = newPerson.FirstName;
                    person.LastName = newPerson.LastName;
                    person.Email = newPerson.Email;
                    context.People.Add(person);
                    context.SaveChanges();
                    Clients.All.personCreated(person);
                    result = true;
                }
            }
            catch (Exception)
            {
                Clients.Caller.raiseError("Unable to create a new Person.");
            }

            return result;
        }

        public bool Update(Person updatedPerson)
        {
            bool result = false;

            try
            {
                using (var context = new PersonContext())
                {
                    var existingPerson = context.People.FirstOrDefault(x => x.Id == updatedPerson.Id);

                    if (existingPerson != null)
                    {
                        existingPerson.FirstName = updatedPerson.FirstName;
                        existingPerson.LastName = updatedPerson.LastName;
                        existingPerson.Email = updatedPerson.Email;
                        context.SaveChanges();
                        Clients.All.personUpdated(existingPerson);
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                Clients.Caller.raiseError("Unable to update the Person.");
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                using (var context = new PersonContext())
                {
                    var existingPerson = context.People.FirstOrDefault(x => x.Id == id);

                    if (existingPerson != null)
                    {
                        context.People.Remove(existingPerson);
                        context.SaveChanges();
                        Clients.All.personRemoved(id);
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                Clients.Caller.raiseError("Unable to update the Person.");
            }

            return result;
        }
    }
}