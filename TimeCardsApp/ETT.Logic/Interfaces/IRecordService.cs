using System;
using System.Collections.Generic;
using System.Text;
using ETT.Logic.DTO;

namespace ETT.Logic.Interfaces
{
    public interface IRecordService : IDisposable
    {
        void CreateRecord(RecordDTO record);
        void UpdateRecord(RecordDTO record);
        RecordDTO GetRecord(int? id);
        IEnumerable<RecordDTO> GetRecords();
        void DeleteRecord(int? id);
    }
}
