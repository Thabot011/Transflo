using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using Transflo.Domain.Entities;
using Transflo.Persistence.DBContext;

namespace Transflo.Test.Helper
{
    public class DBContextMockHelper
    {
        public static Mock<TransfloDBContext> GetDBContextMock()
        {
            var drivers = new List<Driver>
                {
                    new Driver
                    {
                        Id =1,
                        FirstName="Ahmed",
                        LastName ="Thabet",
                        PhoneNumber="01066345489"
                    },
                    new Driver
                    {
                        Id =2,
                        FirstName="Maged",
                        LastName ="Sayed",
                        PhoneNumber="01151970351"
                    },new Driver
                    {
                        Id =3,
                        FirstName="Omar",
                        LastName ="Ahmed",
                        PhoneNumber="01094147077"
                    },
                    new Driver
                    {
                        Id =4,
                        FirstName="Ahmed",
                        LastName ="Hossam",
                        PhoneNumber="01066345489"
                    },
                };
            var mock = drivers.BuildMock().BuildMockDbSet();
            var employeeContextMock = new Mock<TransfloDBContext>();
            employeeContextMock.Setup(x => x.Drivers).Returns(mock.Object);
            return employeeContextMock;
        }
    }
}
