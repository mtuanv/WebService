using System;

namespace Webservice.API.DataAccess.Extensions
{
    public interface IDeleteable
    {
        int? DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
    }
    public class Deleteable : IDeleteable
    {
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
