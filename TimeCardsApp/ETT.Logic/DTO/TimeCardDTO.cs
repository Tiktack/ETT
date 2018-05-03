using System;
using System.Collections.Generic;
using System.Text;
using ETT.Storage.Entities;

namespace ETT.Logic.DTO
{
   public class TimeсardDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}
