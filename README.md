# CGJ-2022-Demo

China Game Jam 2022 参赛作品原型。基于 **GameFramework**（星渊 Star Force 范例改造）搭建的生存模式游戏框架。

## 内容

- **生存模式（Survival）**：按楼层推进，6 / 10 / 15 层对应三种结局
- **战斗 UI**：倒计时、连击、分数
- **剧情过场**：ProcedureStoryline + Timeline 播放
- **完整框架流程**：ProcedureLaunch → CheckVersion → UpdateResources → Menu → Storyline → Game → End
- **DataTable 配置管线**：Excel → 二进制配置的编辑器生成器（Scripts/Editor/DataTableGenerator）

> ⚠️ 这是 Jam 期间的原型：框架与流程完整，核心战斗玩法逻辑未完成（`SurvivalGame.Update` 为空、部分出场代码被注释），适合作为 GameFramework 的流程/配置参考。

## 环境要求

- Unity 6000.3.10f1（建议通过 Unity Hub 安装对应版本）

## 打开方式

1. 克隆本仓库
2. 用 Unity Hub 打开本仓库根目录
3. 等待资源导入完成后，打开 `Assets/GameEntry.unity` 启动框架流程

## License

[MIT](./LICENSE)
