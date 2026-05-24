<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />

  # GameFrameX GameAnalytics Gravity Engine

  [![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine/releases)
  [![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)
  [![Documentation](https://img.shields.io/badge/docs-gameframex-blue.svg)](https://gameframex.doc.alianblank.com)

  獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

  [文檔](https://gameframex.doc.alianblank.com) | [快速開始](#快速開始)

  [English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)
</div>

---

## 項目簡介

GameFrameX GameAnalytics Gravity Engine 元件 - 提供遊戲開發者整合和使用基於 Gravity Engine 的遊戲資料分析功能的介面。該元件包含事件追蹤、計時功能和自訂事件報告。

## 特性

- **事件追蹤** - 上報簡單事件、數值事件和自訂欄位事件
- **計時功能** - 開始和停止計時器，用於測量事件持續時間
- **初始化保護** - 所有方法在執行前檢查初始化狀態
- **自訂欄位** - 支援基於字典的自訂事件資料

## 安裝

### 透過 Git URL 安裝（推薦）

1. 在 Unity 編輯器中開啟 Package Manager
2. 點擊 "+" 按鈕選擇 "Add package from git URL"
3. 輸入以下 URL：
   ```
   https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git
   ```

### 透過 manifest.json 安裝

在專案的 `Packages/manifest.json` 檔案中新增：

```json
{
  "dependencies": {
    "com.gameframex.unity.gameanalytics.gravity-engine": "https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git"
  }
}
```

### 手動安裝

1. 下載最新版本發佈包
2. 解壓縮到專案的 `Packages` 目錄下
3. Unity 會自動辨識並載入包

## 快速開始

### 初始化

在 Unity 的 `Awake` 方法中初始化 `GameAnalyticsComponent`。呼叫 `Init()` 完成初始化：

```csharp
using GameFrameX.GameAnalytics.Runtime;

public class GameAnalyticsExample : MonoBehaviour
{
    private void Awake()
    {
        // 取得 GameAnalyticsComponent 並初始化
        var gameAnalyticsComponent = GameEntry.GetComponent<GameAnalyticsComponent>();
        gameAnalyticsComponent.Init();
    }
}
```

### 計時功能

```csharp
// 開始計時
gameAnalyticsComponent.StartTimer("level_complete");

// 結束計時
gameAnalyticsComponent.StopTimer("level_complete");
```

### 事件上報

```csharp
// 簡單事件
gameAnalyticsComponent.Event("button_clicked");

// 帶數值的事件
gameAnalyticsComponent.Event("score_achieved", 100.0f);

// 帶自訂欄位的事件
var customFields = new Dictionary<string, string>
{
    { "level", "5" },
    { "character", "warrior" }
};
gameAnalyticsComponent.Event("level_started", customFields);

// 帶數值和自訂欄位的事件
gameAnalyticsComponent.Event("level_completed", 95.5f, customFields);
```

## 使用注意事項

- 請確保在使用元件的任何方法之前，元件已被正確初始化
- 若未初始化，則不進行任何操作，確保了只有在元件初始化後事件上報或計時才有效
- 上報的事件名稱應該具有代表性和唯一性，以確保資料分析的準確性

## 整合提示

- 專案中應引入命名空間 `GameFrameX.GameAnalytics.Runtime`
- 確保 `GameAnalyticsManager` 被正確實例化，並已透過 `GameFramework` 註冊
- 對於自訂欄位，使用 `Dictionary<string, string>` 類型保持鍵值對的資料結構

## 更新日誌

詳見 [CHANGELOG.md](CHANGELOG.md)。

## 開源協議

本專案基於 MIT 協議開源，詳見 [LICENSE.md](LICENSE.md)。
