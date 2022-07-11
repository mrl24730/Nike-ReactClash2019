using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Nike_ReactClash2019.Models
{
    public class Response
    {
        public ResponseCode Code = ResponseCode.Success;
        public string Message { get; set; }
        public Object Data { get; set; }
    }

    public class Timeslot
    {
        public int Id { get; set; }
        public string Name_tc { get; set; }
        public int Qty { get; set; }
        public string Slot_group { get; set; }
    }

    public class Record
    {

        [Required]
        public string Name { get; set; }

        [Required, Range(10000000, 99999999)]
        public int Phone { get; set; }
        public string EncryptedPhone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string EncryptedEmail { get; set; }

        public DateTime? Birth { get; set; } = null;
        public bool? IsMale { get; set; } = null;

        [Required]
        public int? Tid { get; set; }

        [Required]
        public bool? Lang { get; set; }

        [Required]
        public bool? Subscribe { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Report
    {
        public Timeslot timeslot;
        public Record record;
    }
}
