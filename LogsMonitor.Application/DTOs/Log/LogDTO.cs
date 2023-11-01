﻿namespace LogsMonitor.Application.DTOs
{
    public class LogDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = null!;
        public DateTime OccurrenceDate { get; set; }
    }
}
