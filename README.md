# 更强的储君
一个基于 `Godot 4.5 + C#` 的《Slay the Spire 2》（杀戮尖塔2）模组，核心目标是强化储君（Regent）的玩法深度，围绕「铸剑/星辉」核心机制扩展新遗物、新卡牌，并通过 Harmony Patch 整合到储君初始配置中。

## 项目简介
| 项⽬ | 内容 |
|------|------|
| 模组 ID | `test` |
| 模组名称 | 更强的储君 |
| 作者 | 硬币落地无声 |
| 当前版本 | `0.0.1` |
| 玩法影响 | 是（修改储君初始遗物/新增卡牌/遗物） |

### 核心设计理念
> 强化储君的「铸剑」与「星辉」机制联动性，补充玩法多样性，提升角色体验的同时保持数值平衡。

## 功能清单
### 1. 新遗物：铸星为刃
- 稀有度：`Common`
- 核心效果：
  - 消耗 {singleStarIcon}（星辉）时，获得「消耗数量 × 当前章节数」的锻造值
  - 单次消耗 ≥ 3 个 {singleStarIcon} 时，本场战斗升级所有「君主之剑」
- 实现文件：`Scripts/Relics/TestRelic.cs`

### 2. 新卡牌：模糊扁狗
- 基础属性：
  - 类型：`Skill`（技能）
  - 稀有度：`Common`
  - 费用：2（升级后费用 -1）
- 效果：对单个敌人施加等于其当前生命值的 `Doom`（末日）
- 实现文件：`Scripts/Cards/TestCard.cs`

### 3. 储君初始遗物扩展
通过 Harmony Patch 修改 `Regent.StartingRelics`，在原有初始遗物基础上，为储君额外添加「铸星为刃」。
- 实现文件：`Scripts/Patches/RegentStarterRelicPatch.cs`

### 4. 模组初始化逻辑
模组启动时自动完成以下操作：
- 注册卡牌 `TestCard` 到 `ColorlessCardPool`（无颜色卡牌池）
- 注册遗物 `TestRelic` 到 `SharedRelicPool`（通用遗物池）
- 自动应用所有 Harmony Patch
- 实现文件：`Scripts/Entry.cs`

## 开发环境
### 必装依赖
| 软件/框架 | 版本要求 |
|-----------|----------|
| Godot Engine | 4.5.1（需带 .NET/C# 支持） |
| .NET SDK | 9.0+ |
| 游戏本体 | Slay the Spire 2（Steam 版本） |

### 游戏依赖 DLL（位于游戏安装目录）
- `sts2.dll`
- `0Harmony.dll`
- `Steamworks.NET.dll`
- `BaseLib.dll`

### 核心项目文件
- `project.godot`：Godot 项目配置
- `test.csproj`：C# 项目配置（含依赖引用）
- `mod_manifest.json`：模组清单（游戏识别用）

## 本地配置指南
### 1. 路径配置（必做）
编译前需修改 `test.csproj` 中的游戏安装路径，替换为你本机的实际路径：
```xml
<PropertyGroup>
  <!-- 替换为你的 Steam 游戏目录 -->
  <Sts2Dir>E:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
  <Sts2DataDir>$(Sts2Dir)\data_sts2_windows_x86_64</Sts2DataDir>
</PropertyGroup>
