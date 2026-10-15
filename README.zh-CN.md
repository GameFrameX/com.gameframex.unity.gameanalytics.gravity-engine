<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />

  # GameFrameX GameAnalytics Gravity Engine

  [![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine/releases)
  [![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)
  [![Documentation](https://img.shields.io/badge/docs-gameframex-blue.svg)](https://gameframex.doc.alianblank.com)

  独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

  [文档](https://gameframex.doc.alianblank.com) | [快速开始](#快速开始)

  [English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)
</div>

---

## 项目简介

GameFrameX GameAnalytics Gravity Engine 组件 - 提供游戏开发者集成和使用基于 Gravity Engine 的游戏数据分析功能的接口。该组件包含事件追踪、计时功能和自定义事件报告。

## 特性

- **事件追踪** - 上报简单事件、数值事件和自定义字段事件
- **计时功能** - 开始和停止计时器，用于测量事件持续时间
- **初始化保护** - 所有方法在执行前检查初始化状态
- **自定义字段** - 支持基于字典的自定义事件数据

## 安装

### 通过 Git URL 安装（推荐）

1. 在 Unity 编辑器中打开 Package Manager
2. 点击 "+" 按钮选择 "Add package from git URL"
3. 输入以下 URL：
   ```
   https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git
   ```

### 通过 manifest.json 安装

在项目的 `Packages/manifest.json` 文件中添加：

```json
{
  "dependencies": {
    "com.gameframex.unity.gameanalytics.gravity-engine": "https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git"
  }
}
```

### 手动安装

1. 下载最新版本发布包
2. 解压到项目的 `Packages` 目录下
3. Unity 会自动识别并加载包

## 快速开始

### 初始化

在 Unity 的 `Awake` 方法中初始化 `GameAnalyticsComponent`。调用 `Init()` 完成初始化：

```csharp
using GameFrameX.GameAnalytics.Runtime;

public class GameAnalyticsExample : MonoBehaviour
{
    private void Awake()
    {
        // 获取 GameAnalyticsComponent 并初始化
        var gameAnalyticsComponent = GameEntry.GetComponent<GameAnalyticsComponent>();
        gameAnalyticsComponent.Init();
    }
}
```

### 计时功能

```csharp
// 开始计时
gameAnalyticsComponent.StartTimer("level_complete");

// 结束计时
gameAnalyticsComponent.StopTimer("level_complete");
```

### 事件上报

```csharp
// 简单事件
gameAnalyticsComponent.Event("button_clicked");

// 带数值的事件
gameAnalyticsComponent.Event("score_achieved", 100.0f);

// 带自定义字段的事件
var customFields = new Dictionary<string, string>
{
    { "level", "5" },
    { "character", "warrior" }
};
gameAnalyticsComponent.Event("level_started", customFields);

// 带数值和自定义字段的事件
gameAnalyticsComponent.Event("level_completed", 95.5f, customFields);
```

## 使用注意事项

- 请确保在使用组件的任何方法之前，组件已被正确初始化
- 若未初始化，则不进行任何操作，确保了只有在组件初始化后事件上报或计时才有效
- 上报的事件名称应该具有代表性和唯一性，以确保数据分析的准确性

## 集成提示

- 工程中应引入命名空间 `GameFrameX.GameAnalytics.Runtime`
- 确保 `GameAnalyticsManager` 被正确实例化，并已经通过 `GameFramework` 注册
- 对于自定义字段，使用 `Dictionary<string, string>` 类型保持键值对的数据结构

## Bootstrap 自注册（渠道注册表）

本包通过 `GravityEngineAnalyticsBootstrap` 在 `BeforeSceneLoad` 阶段向核心包（`com.gameframex.unity.gameanalytics`）的 `GameAnalyticsRegistry` 自注册，注册即生效：所有统计事件广播到全部注册渠道。未安装本包时代码被裁剪，注册不会发生。

渠道配置改为启动期强类型赋值（管理器懒实例化，场景加载前完成赋值即可生效）：

```csharp
GameFrameX.GameAnalytics.GravityEngine.Runtime.GravityEngineAnalyticsBootstrap.Setting =
    new GameFrameX.GameAnalytics.GravityEngine.Runtime.GameAnalyticsGravityEngineSetting
    {
        accessToken = "你的 AccessToken",
        clientId = "你的客户端ID",
        channel = "渠道",
        debug = false,
        version = 1,
    };
```

**破坏性变更 / 迁移说明：** 原先在 `GameAnalyticsComponent` Inspector 上以键值对配置 GravityEngine 参数的方式已移除、不再生效，请在游戏启动代码中改用上述强类型配置。

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。

## 开源协议

本项目基于 MIT 协议开源，详见 [LICENSE.md](LICENSE.md)。
