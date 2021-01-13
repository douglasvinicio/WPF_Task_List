using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session5
{
    class Task
    {
        private string _taskDescription;
        private string _difficulty;
        string _dueDate;
        string _status;

        public string TaskDescription { get; set; }

        public string Difficulty { get; set; }

        public string DueDate{ get; set; }

        public string Status { get; set; }
        
        public Task(string tskDesc, string difficulty, string dueDate, string status)
        {
            TaskDescription = tskDesc;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }

        public override string ToString()
        {
            return $"{TaskDescription} - {Difficulty} - {DueDate} - {Status}";
        }

        public string ToDataString()
        {
            return $"{TaskDescription};{Difficulty};{DueDate};{Status}";
        }

    }
}
