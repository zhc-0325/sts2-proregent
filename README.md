# ProRegent

`ProRegent` 是一个基于 `Godot 4.5 + C#` 的《Slay the Spire 2》Mod，围绕储君（Regent）补充卡牌、遗物与开局联动内容。

当前仓库已经实现：

- 1 张卡牌：`骇人仆从`（`TerrorMinion`）
- 2 个遗物：`铸星为刃`（`StarBlade`）、`附魔忠诚`（`En Loyal`）
- 1 个 Harmony Patch：为储君追加起始遗物
- 简体中文与英文本地化资源

README 中的名称与描述文本，均以仓库内的本地化文件为准，并结合当前代码实现进行了整理。

## 项目简介

这个项目的目标是增强储君的玩法联动，重点放在“君主之剑”“星辉消耗”“锻造收益”这几个方向上。

从当前实现来看：

- `TerrorMinion` 会被注册到 `ColorlessCardPool`
- `StarBlade` 和 `EnLoyal` 会被注册到 `RegentRelicPool`
- 通过 Harmony Patch，储君开局会额外获得 `StarBlade`

## 当前内容

### 卡牌

#### 骇人仆从（TerrorMinion）

- 类型：技能
- 稀有度：非凡
- 费用：`X`
- 目标：单体敌人
- 中文描述：`施加X/{Denominator:diff()}倍敌人当前生命值层数的灾厄。`
- 英文描述：`Apply Doom equal to X / {Denominator:diff()} * target's current Health.`

代码中的实际结算方式为：

- 分母初始值为 `5`
- 升级后分母变为 `4`
- 先计算 `敌人当前生命 / 分母`，再向上取整
- 最终施加的 `Doom` 数值为“向上取整结果 × X”

### 遗物

#### 铸星为刃（StarBlade）

- 稀有度：普通
- 中文描述：`消耗{singleStarIcon}时，获得消耗数量*[blue]{CurrentActIndex}[/blue]的锻造值（倍率为当前阶段数）。单次消耗大于等于[blue]3[/blue]个{singleStarIcon}时，本场战斗升级你的所有君主之剑。`
- 英文描述：`When {singleStarIcon} is spent,gain Forging equal to the number spent * [blue]{CurrentActIndex}[/blue] (multiplier equals current act number).When 3 or more {singleStarIcon} are spent in a single use, upgrade all your Sovereign Blade cards for the rest of combat.`

当前实现对应的行为：

- 每次拥有者消耗星辉时，获得 `消耗数量 × 当前阶段数` 的锻造值
- 当前阶段数通过 `CurrentActIndex + 1` 计算，因此第一幕显示为 `1`
- 若单次消耗星辉数量不少于 `3`，则升级本场战斗中的所有 `SovereignBlade`

#### 附魔忠诚（En Loyal）

- 稀有度：稀有
- 中文描述：`当你打出[green]君主之剑[/green]时，不进入弃牌堆而是进入抽牌堆。`
- 英文描述：`When you play a [green]Sovereign Blade[/green], it goes into your draw pile instead of the discard pile.`

当前实现对应的行为：

- 当拥有者打出 `SovereignBlade` 时，遗物会触发
- 该牌不会按通常流程落入弃牌堆，而是被加入抽牌堆

### 储君联动

项目通过 Harmony Patch 修改 `Regent.StartingRelics` 的 getter，在不移除原有起始遗物的前提下，为储君额外添加 `铸星为刃`。

对应代码位置：

- [`Scripts/Entry.cs`](./Scripts/Entry.cs)
- [`Scripts/Patches/RegentStarterRelicPatch.cs`](./Scripts/Patches/RegentStarterRelicPatch.cs)

## 项目结构

```text
.
|-- Scripts/
|   |-- Cards/          # 自定义卡牌
|   |-- Relics/         # 自定义遗物
|   |-- Patches/        # Harmony Patch
|   `-- Entry.cs        # Mod 入口与注册逻辑
|-- ProRegent/
|   |-- images/         # 卡牌与遗物资源
|   `-- localization/   # 本地化文本
|       |-- eng/
|       `-- zhs/
|-- mod_manifest.json
|-- project.godot
`-- ProRegent.csproj
```

## 开发与构建

开始前请确认本地环境至少具备：

- `Godot 4.5.1`
- `.NET 9`
- 《Slay the Spire 2》
- 可正确引用 `sts2.dll`、`0Harmony.dll`、`Steamworks.NET.dll`、`BaseLib.dll`

在 [`ProRegent.csproj`](./ProRegent.csproj) 中，需要先把游戏目录改成你自己的安装路径，例如：

```xml
<Sts2Dir>E:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
```

项目中的构建与导出约定如下：

- 执行 `dotnet build ProRegent.sln` 后，会自动把生成的 DLL 复制到游戏的 `mods/ProRegent/` 目录
- `mod_manifest.json` 声明该 Mod 同时包含 `dll` 与 `pck`
- `export_presets.cfg` 已提供 Godot 导出配置，可用于生成对应的资源包

## 本地化

当前仓库提供以下本地化资源：

- `ProRegent/localization/zhs/`：简体中文
- `ProRegent/localization/eng/`：英文

对应文件：

- [`ProRegent/localization/zhs/cards.json`](./ProRegent/localization/zhs/cards.json)
- [`ProRegent/localization/eng/cards.json`](./ProRegent/localization/eng/cards.json)
- [`ProRegent/localization/zhs/relics.json`](./ProRegent/localization/zhs/relics.json)
- [`ProRegent/localization/eng/relics.json`](./ProRegent/localization/eng/relics.json)

如果后续继续扩展内容，建议优先维护本地化文本，再同步更新 README，避免说明文档与游戏内实际显示不一致。

## 适合作为参考的内容

这个仓库目前比较适合作为以下方向的参考示例：

- 《Slay the Spire 2》Mod 的基础工程搭建
- Godot + C# 的 Mod 入口注册流程
- 卡牌、遗物与角色起始遗物 Patch 的联动写法
- 中英文资源、本地化文本与图片目录的组织方式
