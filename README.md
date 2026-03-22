# 更强的储君

> 一个基于 **Godot 4.5 + C#** 的《Slay the Spire 2》Mod，围绕 **储君** 角色扩展卡牌、遗物与开局联动。

---

## 项目简介

ProRegent 是一个偏向玩法强化的角色向 Mod，当前内容主要聚焦于：

- 新增卡牌：`模糊扁狗`
- 新增遗物：`铸星为刃`
- 通过 Harmony Patch 修改 `Regent` 的初始遗物逻辑
- 提供对应的中文本地化与资源文件

## 当前内容

### 卡牌

`模糊扁狗`
- 费用：2
- 类型：技能
- 稀有度：普通
- 效果：对一名敌人施加等同其当前生命值的 `灾厄`
- 升级后：费用 `-1`

### 遗物

`铸星为刃`

- 每次消耗辉星时，按当前阶段数获得锻造值
- 单次消耗不少于 3 个辉星时，升级本场战斗中的所有 `君王之剑`

### 角色联动

项目通过 Harmony Patch 扩展了 `Regent.StartingRelics`，让角色开局即可获得新的遗物联动。

## 项目结构

```text
.
├─ Scripts/
│  ├─ Cards/        # 自定义卡牌
│  ├─ Relics/       # 自定义遗物
│  ├─ Patches/      # Harmony 补丁
│  └─ Entry.cs      # Mod 入口与注册逻辑
├─ ProRegent/
│  ├─ images/       # 卡牌与遗物资源
│  └─ localization/ # 本地化文本
├─ mod_manifest.json
├─ project.godot
└─ ProRegent.csproj
```

## 开发说明

开始前请先确认本地环境：

- `Godot 4.5.1`
- `.NET 9`
- `Slay the Spire 2`
- `Harmony / BaseLib / sts2.dll` 可被项目正确引用

`ProRegent.csproj` 中配置了本地游戏目录：

```xml
<Sts2Dir>E:\Steam\steamapps\common\Slay the Spire 2</Sts2Dir>
```

如果你的安装位置不同，先改成自己的路径，再进行构建。项目在构建后会自动将生成的 DLL 复制到游戏的 `mods` 目录。

## 适合什么人

如果你正在做：

- 《Slay the Spire 2》Mod 开发练习
- Godot + C# 的游戏模组实验
- 角色专属卡牌 / 遗物 / Patch 联动设计

这个项目会是一个比较直接、清晰的参考骨架。

---

**ProRegent** 目前已经具备一套完整的 Mod 基础结构：入口注册、内容注入、资源组织、本地化与补丁逻辑，适合继续扩展更多 Regent 专属内容。
