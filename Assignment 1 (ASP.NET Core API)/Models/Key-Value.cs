using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment_1__ASP.NET_Core_API_.Models
{
    public class Key_Value
    {
        [Key]
        public int Id { get; set; }
        public int? Key { get; set; }
        public string? Value { get; set; }
    }

}
