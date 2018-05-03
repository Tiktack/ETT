using ETT.Logic.DTO;
using ETT.Logic.Services;
using System;
using System.Collections.Generic;
using Xunit;


namespace ETT.Tests.Timecards
{
    public class RecordServiceTest
    {
        [Fact]
        public void CreateRecord()
        {   //Arrange
            using (RecordService service = new RecordService())
            {
                //Act
                Exception error = null;

                try
                {
                    int randomNumber = new Random().Next(1, 1000);

                    service.CreateRecord(new RecordDTO()
                    {
                        UserId = randomNumber,
                        TaskId = randomNumber,
                        Hours = 2.36,
                        RecordDateTime = DateTime.Now,
                        ProjectId = randomNumber
                    });
                }
                catch (Exception E)
                {
                    error = E;
                }

                //Assert
                Assert.Null(error);
            }
        }

        //set Record Id
        private const int id= 1;

        [Fact]
        public void Get()
        {
            //Arrange
            using (RecordService service = new RecordService())
            {
                //Act
                RecordDTO record = service.GetRecord(id);
                //Assert
                Assert.NotNull(record);
            }
        }


        [Fact]
        public void Update()
        {
            //Arrange
            using (RecordService service = new RecordService())
            {
                //Act
                Exception error = null;
                try
                {

                    service.UpdateRecord(
                        new RecordDTO()
                        {
                            Id = id,
                            UserId = id+1,
                            TaskId=id+2,
                            Hours=2.55,
                            ProjectId=id+3,
                            RecordDateTime=DateTime.Now
                        }
                    );
                }
                catch (Exception E)
                {
                    error = E;
                }

                // Assert
                Assert.Null(error);
            }
        }

        //User id
        private const int UserId = 1;

        [Fact]
        public void GetAll()
        {
            using (RecordService service = new RecordService())
            {
                // Act
                IEnumerable<RecordDTO> records = service.GetRecords();

                // Assert
                Assert.NotEmpty(records);
            }
        }

    }
}
