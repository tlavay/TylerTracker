using System;

namespace TylerTracker.Common.Models
{
    public record RecordBase
    {
        public RecordBase(DateTime date, string documentType)
        {
            this.DocumentType = documentType;
            this.Date = date;
        }

        private Guid guid;
        public Guid Id
        {
            get
            {
                if (this.guid == default)
                {
                    this.guid = Guid.NewGuid();
                }

                return this.guid;
            }

            set
            {
                this.guid = value;
            }
        }

        public string DocumentType;
        public DateTime Date { get; }
    }
    public record Health(DateTime date, double Weight, int? Systolic = null, int? Diastolic = null, Measurements Measurements = null) : RecordBase(date, "health");
    public record Measurements(DateTime date, double Neck, double Chest, double Arm, double Waist) : RecordBase(date, "measurements");
}
