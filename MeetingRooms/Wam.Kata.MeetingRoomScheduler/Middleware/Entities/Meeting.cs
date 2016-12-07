using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Wam.Kata.MeetingRoomScheduler.Validation;

namespace Wam.Kata.MeetingRoomScheduler.Middleware.Entities
{
    public class Meeting
    {
        [JsonIgnore]
        public string Room { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        [Range(0, 23, ErrorMessage = "The start hour of the meeting must be between 0 and 23")]
        public int StartsAt { get; set; }

        [Required]
        [Range(1, 24, ErrorMessage = "The end hour of the meeting must be between 1 and 24")]
        [GreaterThanInt("StartsAt", ErrorMessage = "The end hour must be greater than the start hour")]
        public int EndsAt { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }
    }
}