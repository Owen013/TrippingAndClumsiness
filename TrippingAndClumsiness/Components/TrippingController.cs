using HarmonyLib;
using UnityEngine;

namespace TrippingAndClumsiness.Components;

public class TrippingController : MonoBehaviour
{
    public static TrippingController Instance { get; private set; }
    public bool IsTripping { get; private set; }

    private PlayerCharacterController _characterController;
    private PlayerAudioController _audioController;
    private float _tripTimeLeft;

    private void Start()
    {
        Instance = this;
        _characterController = GetComponent<PlayerCharacterController>();
        _audioController = Locator.GetPlayerAudioController();
    }

    private void FixedUpdate()
    {
        if (_tripTimeLeft > 0f)
        {
            _tripTimeLeft -= Time.deltaTime;
        }
        else if (IsTripping && !PlayerState.IsDead())
        {
            StopTripping();
        }

        float trippingChance = Main.Instance.HikersModAPI != null && Main.Instance.HikersModAPI.IsSprinting() ? Config.SprintingTripChance : Config.TripChance;
        bool canTrip = _tripTimeLeft <= 0 && _characterController.IsGrounded();
        if (trippingChance != 0f && canTrip && Random.Range(0f, 1f) <= 1f - Mathf.Pow(1f - trippingChance, Time.deltaTime))
        {
            StartTripping();
        }

    }

    public void StartTripping()
    {
        IsTripping = true;
        _tripTimeLeft = Config.TripDuration;
        _characterController.MakeUngrounded();
        _characterController._owRigidbody.UnfreezeRotation();
        _characterController.GetComponent<AlignPlayerWithForce>().enabled = false;
        _characterController.SetPhysicsMaterial(_characterController._standingPhysicMaterial);
        _audioController._oneShotSleepingAtCampfireSource.PlayOneShot(AudioType.PlayerGasp_Medium, 1f);
    }

    public void StopTripping()
    {
        IsTripping = false;
        _characterController._owRigidbody.FreezeRotation();
        _characterController.GetComponent<AlignPlayerWithForce>().enabled = true;
    }
}
