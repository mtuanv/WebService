using System;
using System.ComponentModel.DataAnnotations;

namespace WebService.DataAccess.Extensions
{
    public interface IAuditedEntity<T>
        where T : struct
    {
        T Id { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
    public class AuditedEntity<T> : IAuditedEntity<T>
        where T : struct
    {
        [Key]
        public T Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
