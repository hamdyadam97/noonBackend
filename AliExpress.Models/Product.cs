using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AliExpress.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Product: BaseEntity , IDeletedEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int quantity {  get; set; }
        public string ShippingMethod { get; set; }
        public string? PlaceOfOrigin { get; set; }
        public string? ModelNumber { get; set; }
        public string? LensWidth { get; set; }
        public string? LensHeight { get; set; }
        public string? LensMaterial { get; set; }
        public string? Department { get; set; }
        public Gender Gender { get; set; }
        public string? FrameMaterial { get; set; }
        public string? LensFeatures { get; set; }
        public string? Style { get; set; }
        public string? BrandName { get; set; }
        public string? Origin { get; set; }
        public string? ItemType { get; set; }
        public string? EyewearType { get; set; }
        public string? Certification { get; set; }
        public string? FrameColor { get; set; }
        public string? LensColor { get; set; }
        public string? LensAttributes { get; set; }
        public string? UsageScope { get; set; }
        public string? SuitableFaceShapes { get; set; }
        public string? SunglassesStyle { get; set; }
        public string? CompanyHistory { get; set; }
        public string? Brand { get; set; }

        public bool? PackageIncluded { get; set; }
        public string? CommunicationType { get; set; }
        public string? SetType { get; set; }
        public string? Transmitter { get; set; }
        public string? Use { get; set; }

        public string? ChargingTime { get; set; }
        public string? WorkingHours { get; set; }
        public string? TransmissionDistance { get; set; }
        public string? Features { get; set; }
        public string? Accessories { get; set; }
        public string? BandLength { get; set; }
        public string? BandWidth { get; set; }
        public string? Weight { get; set; }
        public string? SimCardSlot { get; set; }
        public string? BuiltInVoiceAssistant { get; set; }
        public string? BluetoothVersion { get; set; }
        public string? WiFi { get; set; }
        public string? MeasurementScales { get; set; }
        public string? ActivityTracking { get; set; }
        public string? Touchscreen { get; set; }
        public string? Category { get; set; }
        public string? AppName { get; set; }
        public string? ScreenMaterial { get; set; }
        public string? CPUModel { get; set; }
        public string? Detachable { get; set; }
        public string? AppDownloadAvailable { get; set; }
        public string? RemovableBattery { get; set; }
        public string? CPUManufacturer { get; set; }
        public string? Resolution { get; set; }
        public string? ScreenSize { get; set; }
        public string? MovementType { get; set; }

        public string? AgeCategoryApp { get; set; }
        public string? ScreenShape { get; set; }
        public string? CaseMaterial { get; set; }
        public string? StrapMaterial { get; set; }
        public string? WaterproofRating { get; set; }
        public string? AvailableSimCard { get; set; }
        public string? Mechanism { get; set; }
        public string? MultiFace { get; set; }
        public string? GPS { get; set; }
        public string? NetworkMode { get; set; }
        public string? BatteryCapacity { get; set; }
        public string? RearCamera { get; set; }
        public string? RAM { get; set; }
        public string? ROM { get; set; }
        public string? System { get; set; }
        public string? Type { get; set; }
        public string? Compatibility { get; set; }
        public string? Language { get; set; }
        public string? Functionality { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<Images> Images { get; set; }
    }
}
