using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LVT.Models.Entities
{
    [Table("vehicle_alarms")]
    public class VehicleAlarm
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Alarm Type is required")]
        [StringLength(60, ErrorMessage = "Alarm Type can't be longer than 60 characters.")]
        [Column("registration_no")]
        public string RegistrationNo { get; set; }

        [Column("color")]
        public string Color{ get; set; }

        [Column("engine_no")]
        public string EngineNo { get; set; }

        [Column("chassis_no")]
        public string ChassisNo { get; set; }

        [Column("owner_name")]
        public string OwnerName { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("reported_no")]
        public string ReportedOn { get; set; }

        [Column("fir_police_station")]
        public string FirPoliceStation { get; set; }

        [Column("fir_no")]
        public string FirNO { get; set; }

        [Column("investigator_name")]
        public string InvestigatorName { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("alarm_cam_id")]
        public string AlarmCameraId { get; set; }

        [Column("alarm_location")]
        public string AlarmLocation { get; set; }

        [Column("alarm_generation_time")]
        public string AlarmGenerationTime { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

    }
}
