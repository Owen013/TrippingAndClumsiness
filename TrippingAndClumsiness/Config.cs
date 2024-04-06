using OWML.Common;

namespace TrippingAndClumsiness;

public static class Config
{
    public static float TripDuration { get; private set; }
    public static float TripChance { get; private set; }
    public static float SprintingTripChance { get; private set; }
    public static float DamagedTripChance { get; private set; }
    public static float ReverseBoostChance { get; private set; }
    public static float EmergencyBoostMisfireChance { get; private set; }
    public static float ScoutMisfireChance { get; private set; }
    //public static float ReverseRepairChance { get; private set; }

    public static void UpdateConfig(IModConfig config)
    {
        TripDuration = config.GetSettingsValue<float>("Trip Duration");
        TripChance = config.GetSettingsValue<float>("Chance of Tripping Randomly");
        SprintingTripChance = config.GetSettingsValue<float>("Chance of Tripping while Sprinting");
        DamagedTripChance = config.GetSettingsValue<float>("Chance of Tripping per Point of Damage");
        ReverseBoostChance = config.GetSettingsValue<float>("Reverse Boost Chance");
        EmergencyBoostMisfireChance = config.GetSettingsValue<float>("Emergency Boost Misfire Chance");
        ScoutMisfireChance = config.GetSettingsValue<float>("Scout Misfire Chance");
        //ReverseRepairChance = config.GetSettingsValue<float>("Reverse Repair Chance");
    }
}