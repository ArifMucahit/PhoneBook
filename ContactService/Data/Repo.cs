using ContactService.Models;
using ContactService.Models.DTO;
using ContactService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public class Repo:IRepo
    {
        PhoneBookContext _dbContext;

        public Repo(PhoneBookContext phoneBookContext)
        {
            _dbContext = phoneBookContext;
        }

        public async Task<bool> CreatePerson(Person person)
        {
            var guid = new Guid();
            guid = Guid.NewGuid();
            person.UID = guid.ToString();

             _dbContext.Set<Person>().Add(person);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeletePerson(Person person)
        {

            var personToPurge = await _dbContext.Set<Person>().Where(x=> x.Name == person.Name && x.Surname == person.Surname).FirstOrDefaultAsync();
            if (personToPurge == null)
                return false;
          

            _dbContext.Set<Person>().Remove(personToPurge);
            return await _dbContext.SaveChangesAsync() > 0;
        }


        public async Task<bool> CreateContact(PersonWithContact contact)
        {
            var person = await _dbContext.Set<Person>().Where(x => x.Name == contact.Name && x.Surname == contact.Surname).FirstOrDefaultAsync();
            if (person == null)
                return false;


            _dbContext.Set<ContactInfo>().Add(new ContactInfo
            {
                InfoDetail = contact.ContactInfo.InfoDetail,
                InfoType = contact.ContactInfo.InfoType,
                PersonUID = person.UID
            });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveContact(PersonWithContact contact)
        {
            var person = await _dbContext.Set<Person>().Where(x => x.Name == contact.Name && x.Surname == contact.Surname).FirstOrDefaultAsync();
            if (person == null)
                return false;

            var contactToPurge = await _dbContext.Set<ContactInfo>().Where(x=> x.PersonUID == person.UID && x.InfoType == contact.ContactInfo.InfoType && x.InfoDetail == contact.ContactInfo.InfoDetail).FirstOrDefaultAsync();

            if (contactToPurge == null)
                return false;

            _dbContext.Set<ContactInfo>().Remove(contactToPurge);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<PersonDTO>> GetPersons()
        {
            var person = await _dbContext.Set<Person>().Select(x=> new PersonDTO { Company = x.Company,Name = x.Name,Surname = x.Surname}).ToListAsync();
            return person;
        }

        public async Task<List<PersonWithContacts>> GetPersonWithContact()
        {
            var personContacts = await _dbContext.Set<Person>().Include(x => x.ContactInfos).Select(x=> new PersonWithContacts 
            { 
                Name = x.Name,
                Surname = x.Surname,
                Company= x.Company,
                ContactInfos = x.ContactInfos.Select(x=> new ContactDTO { InfoDetail = x.InfoDetail,InfoType = x.InfoType }).ToList(),
            }).ToListAsync();

            return personContacts;
        }

        public async Task<List<LocationReportDTO>> GetLocationReport()
        {
            var report = (from contacts in _dbContext.ContactInfo
                    join c2 in _dbContext.ContactInfo.DefaultIfEmpty().Where(x => x.InfoType == EnumContactType.PhoneNumber) on contacts.PersonUID equals c2.PersonUID
                    select contacts
                    ).ToList();

            if (report == null)
                return null;

            var locReport = new List<LocationReportDTO>();
            foreach (var item in report.Distinct().Where(x=> x.InfoType == EnumContactType.Location))
            {
                locReport.Add(new LocationReportDTO
                {
                    InfoDetail = item.InfoDetail,
                    PersonOnLocation = report.Where(x=> x.InfoDetail == item.InfoDetail).Count(),
                    PersonPhoneNumber = report.Where(x=> x.InfoType == EnumContactType.PhoneNumber && x.InfoDetail != null).Count()
                });
            }
            return locReport;
        }
    }
}
