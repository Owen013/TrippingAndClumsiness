using HarmonyLib;
using TrippingAndClumsiness.Components;
using UnityEngine;

namespace TrippingAndClumsiness;

[HarmonyPatch]
public static class Patches
{
    private static bool _isBoostReversed;

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerCharacterController), nameof(PlayerCharacterController.Awake))]
    private static void OnCharacterControllerAwake(PlayerCharacterController __instance)
    {
        __instance.gameObject.AddComponent<TrippingController>();
        __instance.gameObject.AddComponent<ScoutMisfireController>();
        //__instance.gameObject.AddComponent<ReverseRepairController>();
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerCharacterController), nameof(PlayerCharacterController.OnInstantDamage))]
    private static void OnCharacterControllerDamaged(float instantDamage)
    {
        if (Random.Range(0f, 1f) <= Config.DamagedTripChance * instantDamage)
        {
            TrippingController.Instance.StartTripping();
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(JetpackThrusterModel), nameof(JetpackThrusterModel.ActivateBoost))]
    private static void OnStartBoosting(JetpackThrusterModel __instance)
    {
        if (Config.ReverseBoostChance != 0f && Random.Range(0f, 1f) <= Config.ReverseBoostChance)
        {
            _isBoostReversed = true;
            __instance._boostThrust = -__instance._boostThrust;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(JetpackThrusterModel), nameof(JetpackThrusterModel.DeactivateBoost))]
    private static void OnStopBoosting(JetpackThrusterModel __instance)
    {
        if (_isBoostReversed)
        {
            _isBoostReversed = false;
            __instance._boostThrust = -__instance._boostThrust;
        }
    }

    //[HarmonyPrefix]
    //[HarmonyPatch(typeof(ShipHull), nameof(ShipHull.RepairTick))]
    //public static bool RepairTick(ShipHull __instance)
    //{
    //    if (!ReverseRepairController.Instance.IsRepairReversed)
    //    {
    //        return true;
    //    }

    //    if (!__instance._damaged)
    //    {
    //        return false;
    //    }

    //    __instance._integrity = Mathf.Min(__instance._integrity - Time.deltaTime / __instance._repairTime, 1f);
    //    if (__instance._integrity <= 0f)
    //    {
    //        __instance.GetComponent<ShipDetachableModule>().Detach();
    //    }
    //    if (__instance._damageEffect != null)
    //    {
    //        __instance._damageEffect.SetEffectBlend(1f - __instance._integrity);
    //    }

    //    return false;
    //}
}
