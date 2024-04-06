using HarmonyLib;
using UnityEngine;

namespace TrippingAndClumsiness.Components;

public class ReverseRepairController : MonoBehaviour
{
    public static ReverseRepairController Instance { get; private set; }
    public bool IsRepairReversed { get; private set; }

    private void Awake()
    {
        Instance = this;
        Harmony.CreateAndPatchAll(typeof(ReverseRepairController));
    }

    private void Update()
    {
        if (OWInput.IsNewlyPressed(InputLibrary.interact))
        {
            if (Config.ReverseRepairChance != 0f && Random.Range(0f, 1f) <= Config.ReverseRepairChance)
            {
                Main.Instance.WriteLine("Repair reversed!");
                IsRepairReversed = true;
            }
            else
            {
                IsRepairReversed = false;
            }
        }
    }
}