using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Common;
using Core.Repository;
using PartyDomain.IRepositories;
using PartyDomain.Model;
using PartyDomain.Services;
using PartyDomain.Test.Mock;

namespace party
{
    [TestClass]
    public class PartyServiceTest
    {
        [TestMethod]
        public void AddParty_Nominal()
        {
            UnitOfWorkInMemory uow = new UnitOfWorkInMemory();
            uow.LoadDataFromCSV(".\\CsvFiles\\Country.csv");
            uow.LoadDataFromCSV(".\\CsvFiles\\Purpose.csv");

            PartyService serv = new PartyService(uow);
            var party = new Party{
                Name="Tony",
                Addresses=new HashSet<Address>(new []
                    {
                        new Address{
                            Ligne1="29 rue de velye",
                            country=new Country{Id=Guid.Parse("f4b2464f-d31c-4bca-b790-4dfe9ff87cf5")},//,
                            purposes=new HashSet<Purpose>(new[]
                                {
                                    new Purpose{Id=Guid.Parse("b40f2196-9cc1-470e-b82f-3dc2c06b01e8")}
                                })
                        }
                    })
            };
            serv.AddAsync(party).Wait();
            uow.ClearEntityCache(party);
            var result = serv.GetAllAsync().Result.FirstOrDefault(p=>p.Name=="Tony");
            Assert.IsFalse(result==null, "party not created");
        
        }

        [TestMethod]
        public void AddParty_BusinessException()
        {
            IUnitOfWork uow = new UnitOfWorkInMemory();
            PartyService serv = new PartyService(uow);
            Action insert = ()=>{
                serv.AddAsync(new Party{
                    Name=""
                }).Wait();
            };

            insert.Should().Throw<AggregateException>()
                .WithInnerException<BusinessException>();
        }
    }
}
