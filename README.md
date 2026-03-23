# ProRegent

> 一个基于 **Godot 4.5 + C#** 的《Slay the Spire 2》Mod，围绕储君（Regent）补充卡牌、遗物与开局联动内容。

## 项目简介

`ProRegent` 的目标是强化储君的玩法联动，提升铸剑与星辉资源的协同体验。

当前版本主要包含：

- 新卡牌：`骇人仆从`（`TerrorMinion`）
- 新遗物：`铸星为刃`（`StarBlade`）
- 通过 Harmony Patch 扩展储君的初始遗物逻辑
- 配套的中英文本地化与图片资源

项目在 `mod_manifest.json` 中的展示名称为：`更强的储君`。

## 当前内容

### 卡牌：骇人仆从

- 类型：技能
- 费用：`X`
- 目标：单体敌人
- 效果：施加 `X / 5` 倍敌人当前生命值层数的 `Doom`，结果向上取整
- 升级效果：分母由 `5` 降为 `4`

本地化参考：

- 中文：`施加X/{Denominator:diff()}倍敌人当前生命值层数的灾厄。`
- 英文：`Apply Doom equal to X / {Denominator:diff()} * target's current Health.`

### 遗物：铸星为刃

- 稀有度：普通
- 效果：每次消耗星辉时，按 `消耗数量 * 当前阶段数` 获得锻造值
- 额外效果：若单次消耗不少于 `3` 个星辉，则升级本场战斗中的所有 `Sovereign Blade`

本地化参考：

- 中文：`消耗{singleStarIcon}时，获得消耗数量*[blue]{CurrentActIndex}[/blue]的锻造值（倍率为当前阶段数）。单次消耗大于等于[blue]3[/blue]个{singleStarIcon}时，本场战斗升级你的所有君主之剑。`

### 角色联动

项目通过 Harmony Patch 修改 `Regent.StartingRelics`，在不移除原有初始遗物的前提下，为储君额外加入 `铸星为刃`。

## 项目结构

```text
.
├─ Scripts/
│  ├─ Cards/          # 自定义卡牌
│  ├─ Relics/         # 自定义遗物
│  ├─ Patches/        # Harmony 补丁
│  └─ Entry.cs        # Mod 入口与注册逻辑
├─ ProRegent/
│  ├─ images/         # 卡牌与遗物资源
│  └─ localization/   # 本地化文本
│     ├─ eng/
│     └─ zhs/
├─ mod_manifest.json
├─ project.godot
└─ ProRegent.csproj
```

## 开发与构建

开始前请先确认本地环境：

- `Godot 4.5.1`
- `.NET 9`
- 《Slay the Spire 2》
- 可正确引用 `sts2.dll`、`0Harmony.dll`、`Steamworks.NET.dll`、`BaseLib.dll`

`ProRegent.csproj` 中需要配置你的游戏目录，例如：

```xml
<Sts2Dir>E:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
```

如果你的安装路径不同，请先修改为自己的目录。项目构建完成后，会自动将生成的 DLL 复制到游戏的 `mods/ProRegent/` 目录下。

## 本地化说明

本项目当前提供：

- `ProRegent/localization/zhs/`：简体中文
- `ProRegent/localization/eng/`：英文

README 中的卡牌名、遗物名和效果文本，均参考了上述本地化文件，并结合当前代码实现进行了整理。

## 适合作为参考的内容

如果你正在做以下方向，这个项目可以作为一个比较直接的示例：

- 《Slay the Spire 2》Mod 开发入门
- Godot + C# 的游戏模组工程组织
- 角色专属卡牌、遗物与 Patch 联动设计
- 本地化资源与图片资源的配套管理

## 说明

当前仓库已经具备一个较完整的 Mod 基础结构，包括：

- 模型注册
- 遗物与卡牌注入
- Harmony 补丁
- 本地化文本
- 资源目录组织

后续可以继续在此基础上扩展更多储君专属内容。
