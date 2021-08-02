using System;

namespace TylerTracker.Common.Models
{
    public record RecordBase
    {
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

        public string DocumentType { get; set; }
        public DateTime Date { get; set;  }
    }
    public record Health(double Weight, int? Systolic = null, int? Diastolic = null, Measurements Measurements = null) : RecordBase;
    public record Measurements(double Neck, double Chest, double Arm, double Waist) : RecordBase;
}
