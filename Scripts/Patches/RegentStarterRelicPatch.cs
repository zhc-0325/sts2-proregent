
using HarmonyLib;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using ProRegent.Scripts.Relics; 
using MegaCrit.Sts2.Core.Models; 

namespace ProRegent.Scripts.Patches;

[HarmonyPatch(
    typeof(Regent),
    nameof(Regent.StartingRelics),
    MethodType.Getter
)]
public static class RegentStarterRelicPatch
{
    public static void Postfix(ref IReadOnlyList<RelicModel> __result)
    {
        if (__result == null)
        {
            Log.Warn("储君初始遗物列表为空，无法添加");
            return;
        }

        var mutableRelics = __result.ToList();

        var starBlade = ModelDb.Relic<StarBlade>();

        if (starBlade == null)
        {
            Log.Error("实例为空！请检查是否注册到RegentRelicPool");
            return;
        }
        if (mutableRelics.Contains(starBlade))
        {
            Log.Debug("储君已拥有星刃，无需重复添加");
            return;
        }

        mutableRelics.Add(starBlade);

        __result = mutableRelics.AsReadOnly();
    }
}