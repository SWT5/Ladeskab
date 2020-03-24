using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab.Implementation
{
    public class LogFile : ILogFile
    {
        private List<LogFileData> listOfData;
        private string _date;
        private string _id;
        private string _LockedData = "Door locked";
        private string _UnlockedData = "Door Unlocked";
        private string _ErrorData;

        public LogFile() {listOfData = new List<LogFileData>();}

        public void LogDoorLocked(string id)
        {
            _date = DateTime.Now.ToString("dd/MM/yyy");
            _id = id;
            listOfData.Add(new LogFileData(_id, _date, _LockedData));
        }

        public void LogDoorUnlocked(string id)
        {
            _date = DateTime.Now.ToString("dd/MM/yyy");
            _id = id;
            listOfData.Add(new LogFileData(_id, _date, _UnlockedData));
        }

        public void LogError(string s)
        {
            _date = DateTime.Now.ToString("dd/MM/yyy");
            _id = "Error";
            _ErrorData = s;
            listOfData.Add(new LogFileData(_id, _date, _ErrorData));
        }
    }

}
