using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webservice.API.DataAccess.Extensions
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
