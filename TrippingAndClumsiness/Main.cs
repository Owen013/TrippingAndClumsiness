using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace TrippingAndClumsiness;

public class Main : ModBehaviour
{
    public static Main Instance { get; private set; }
    public IHikersMod HikersModAPI { get; private set; }

    public override void Configure(IModConfig config)
    {
        Config.UpdateConfig(config);
    }

    public void WriteLine(string text, MessageType type = MessageType.Message)
    {
        Instance.ModHelper.Console.WriteLine(text, type);
    }

    private void Awake()
    {
        Instance = this;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }

    private void Start()
    {
        HikersModAPI = ModHelper.Interaction.TryGetModApi<IHikersMod>("Owen013.MovementMod");
    }
}