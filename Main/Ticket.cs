using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class Ticket
    {
        private static int _nextId = 1;
        private int _ticketId;
        private string _summary;
        private Status _status;
        private Priority _priority;
        private string _submitter;
        private string _assigned;
        private List<string> _watching;

        public Ticket(int ticketId, string summary, Status status, Priority priority, string submitter, string assigned,
            List<string> watching)
        {
            _ticketId = ticketId;
            _summary = summary;
            _status = status;
            _priority = priority;
            _submitter = submitter;
            _assigned = assigned;
            _watching = watching;

            if (ticketId >= _nextId)
            {
                _nextId++;
            }
        }

        public Ticket(string summary, Status status, Priority priority, string submitter, string assigned,
            List<string> watching)
        {
            _ticketId = _nextId;
            _nextId++;
            _summary = summary;
            _status = status;
            _priority = priority;
            _submitter = submitter;
            _assigned = assigned;
            _watching = watching;
        }

        public Ticket(string str)
        {
            string[] arr = str.Split(',');

            _ticketId = Int32.Parse(arr[0]);
            _summary = arr[1];
            Enum.TryParse(arr[2], out _status);
            Enum.TryParse(arr[3], out _priority);
            _submitter = arr[4];
            _assigned = arr[5];
            _watching = arr[6].Split("|").ToList();
        }

        public override string ToString()
        {
            string watchingList = "\t" + string.Join("\n\t", _watching);
            return string.Join(
                "\n",
                $"Ticket {_ticketId}",
                $"Summary: {_summary}",
                $"Status: {_status}",
                $"Priority: {_priority}",
                $"Submitter: {_submitter},",
                $"Assigned: {_assigned}",
                "Watching:",
                watchingList
            );
        }

        public string ToPrintString()
        {
            String watchingString = String.Join("|", _watching);
            return String.Join(",", _ticketId, _summary, _status, _priority, _submitter, _assigned, watchingString);
        }
    }
}