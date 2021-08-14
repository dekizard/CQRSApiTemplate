using System;

namespace CQRSApiTemplate.Domain.Abstraction
{
    public abstract class AuditableEntity
    {
        public long Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
