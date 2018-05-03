using System;
using System.Collections.Generic;
using System.Linq;
using ETT.Logic.Interfaces;
using ETT.Logic.DTO;
using ETT.Storage;
using ETT.Storage.Entities;

namespace ETT.Logic.Services
{
    public class RecordService : IRecordService
    {
        private UnitOfWork Database { get; set; }

        public RecordService()
        {
            Database = new UnitOfWork();
        }

        public void CreateRecord(RecordDTO record)
        {
            Database.Records.Create(
                new Record()
                {
                    UserId = record.UserId,
                    TaskId = record.TaskId,
                    Hours = record.Hours,
                    Note=record.Note,
                    RecordDateTime = record.RecordDateTime,
                });
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public RecordDTO GetRecord(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            Record record = Database.Records.Get((int)id);
            if (record == null) throw new KeyNotFoundException();

            return new RecordDTO()
            {
                Id = record.Id,
                UserId = record.UserId,
                TaskId = record.TaskId,
                Hours = record.Hours,
                Note = record.Note,
                RecordDateTime = record.RecordDateTime,
                ProjectId = record.Task.ProjectId,
            };

        }

        public IEnumerable<RecordDTO> GetRecords()
        {
            IEnumerable<RecordDTO> records = Database.Records.GetAll()
                .Select(record =>new RecordDTO()
            {
                Id = record.Id,
                UserId = record.UserId,
                TaskId = record.TaskId,
                Hours = record.Hours,
                Note=record.Note,
                RecordDateTime = record.RecordDateTime,
                ProjectId = record.Task.ProjectId,
            });
            return records;
        }

        public void UpdateRecord(RecordDTO record)
        {
            Database.Records.Update(
                 new Record()
                 {
                     Id = record.Id,
                     UserId = record.UserId,
                     TaskId = record.TaskId,
                     Hours = record.Hours,
                     Note = record.Note,
                     RecordDateTime = record.RecordDateTime,
                 }
             );
            Database.Save();
        }

        public void DeleteRecord(int? id)
        {
           
                if (id == null) throw new ArgumentNullException(nameof(id));
                if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

                Database.Records.Delete((int)id);
                Database.Save();
        }
    }
}
