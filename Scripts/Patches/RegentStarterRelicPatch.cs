
using HarmonyLib; // Harmony核心库，用于挂钩方法/属性
using MegaCrit.Sts2.Core.Models.Characters; // Regent类所在命名空间
using MegaCrit.Sts2.Core.Models.Relics; // RelicModel/ModelDb所在命名空间
using MegaCrit.Sts2.Core.Logging; // 日志调试
using System.Collections.Generic; // List/IReadOnlyList
using System.Linq; // ToList/Contains等扩展方法   // Log 所在
using System.Collections.Generic;           // IReadOnlyList、List 所在
using System.Linq;                          // ToList()、Contains() 所在
using ProRegent.Scripts.Relics; 
using MegaCrit.Sts2.Core.Models; 

// 2. 命名空间（和你的其他补丁保持一致）
namespace ProRegent.Scripts.Patches;

/// <summary>
/// 非侵入式修改摄政王初始遗物：保留神圣权利 + 新增星刃
/// </summary>
[HarmonyPatch(
    typeof(Regent), // 要挂钩的类：摄政王角色模型
    nameof(Regent.StartingRelics), // 要挂钩的属性：初始遗物列表
    MethodType.Getter // 挂钩属性的「读取器」（Getter）
)]
public static class RegentStarterRelicPatch
{
    /// <summary>
    /// Postfix：在属性返回结果后执行（核心逻辑）
    /// </summary>
    /// <param name="__result">原属性返回的初始遗物列表（引用传递，可修改）</param>
    public static void Postfix(ref IReadOnlyList<RelicModel> __result)
    {
        if (__result == null)
        {
            Log.Warn("摄政王初始遗物列表为空，无法添加星刃");
            return;
        }

        var mutableRelics = __result.ToList();

        var starBlade = ModelDb.Relic<starBlade>();

        if (starBlade == null)
        {
            Log.Error("星刃实例为空！请检查是否注册到RegentRelicPool");
            return;
        }
        if (mutableRelics.Contains(starBlade))
        {
            Log.Debug("摄政王已拥有星刃，无需重复添加");
            return;
        }

        mutableRelics.Add(starBlade);

        __result = mutableRelics.AsReadOnly();
    }
}