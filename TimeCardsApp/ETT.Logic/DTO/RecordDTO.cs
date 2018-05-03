using System;
namespace ETT.Logic.DTO
{
    public class RecordDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public double Hours { get; set; }
        public string Note { get; set; }
        public DateTime RecordDateTime { get; set; }
        public int ProjectId { get; set; }
    }
}
