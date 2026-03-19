using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using Test.Scripts.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using Test.Scripts.Relics;
namespace Test.Scripts;
[ModInitializer("Init")]
public class Entry
{
    private static Harmony? _harmony;


    public static void Init()
    {
         ModHelper.AddModelToPool<ColorlessCardPool, TestCard>();
         ModHelper.AddModelToPool<SharedRelicPool, TestRelic>();
        _harmony = new Harmony("sts2.reme.testmod");
        _harmony.PatchAll();
        Log.Debug("Mod initialized!");
    }
}