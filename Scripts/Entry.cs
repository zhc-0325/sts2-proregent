using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using ProRegent.Scripts.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using ProRegent.Scripts.Relics;
namespace ProRegent.Scripts;
[ModInitializer("Init")]
public class Entry
{
    private static Harmony? _harmony;


    public static void Init()
    {
         ModHelper.AddModelToPool<ColorlessCardPool, TerrorMinion>();
         ModHelper.AddModelToPool<RegentRelicPool, StarBlade>();
         ModHelper.AddModelToPool<RegentRelicPool, EnLoyal>();
        _harmony = new Harmony("sts2.reme.ProRegentmod");
        _harmony.PatchAll();
        Log.Debug("Mod initialized!");
    }
}