<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />

  # GameFrameX GameAnalytics Gravity Engine

  [![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine/releases)
  [![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)
  [![Documentation](https://img.shields.io/badge/docs-gameframex-blue.svg)](https://gameframex.doc.alianblank.com)

  All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

  [Documentation](https://gameframex.doc.alianblank.com) | [Quick Start](#quick-start)

  **English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)
</div>

---

## Project Overview

GameFrameX GameAnalytics Gravity Engine Component - Provides game developers with an interface for integrating and using game analytics functionality powered by Gravity Engine. This component includes event tracking, timer functionality, and custom event reporting.

## Features

- **Event Tracking** - Report simple events, value-based events, and custom field events
- **Timer Functionality** - Start and stop timers for measuring event durations
- **Initialization Guard** - All methods check initialization status before executing
- **Custom Fields** - Support for dictionary-based custom event data

## Installation

### Via Git URL (Recommended)

1. Open Package Manager in Unity Editor
2. Click the "+" button and select "Add package from git URL"
3. Enter the following URL:
   ```
   https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git
   ```

### Via manifest.json

Add the following to your project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.gameframex.unity.gameanalytics.gravity-engine": "https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git"
  }
}
```

### Manual Installation

1. Download the latest release package
2. Extract it to your project's `Packages` directory
3. Unity will automatically recognize and load the package

## Quick Start

### Initialization

Initialize the `GameAnalyticsComponent` in Unity's `Awake` method. Call `Init()` to complete initialization:

```csharp
using GameFrameX.GameAnalytics.Runtime;

public class GameAnalyticsExample : MonoBehaviour
{
    private void Awake()
    {
        // Get the GameAnalyticsComponent and initialize it
        var gameAnalyticsComponent = GameEntry.GetComponent<GameAnalyticsComponent>();
        gameAnalyticsComponent.Init();
    }
}
```

### Timer Usage

```csharp
// Start a timer for an event
gameAnalyticsComponent.StartTimer("level_complete");

// Stop the timer when the event ends
gameAnalyticsComponent.StopTimer("level_complete");
```

### Event Reporting

```csharp
// Simple event
gameAnalyticsComponent.Event("button_clicked");

// Event with value
gameAnalyticsComponent.Event("score_achieved", 100.0f);

// Event with custom fields
var customFields = new Dictionary<string, string>
{
    { "level", "5" },
    { "character", "warrior" }
};
gameAnalyticsComponent.Event("level_started", customFields);

// Event with value and custom fields
gameAnalyticsComponent.Event("level_completed", 95.5f, customFields);
```

## Usage Notes

- Ensure the component is properly initialized before calling any methods
- If not initialized, no operations will be performed, ensuring events are only reported after initialization
- Event names should be representative and unique to ensure accurate data analysis

## Integration Tips

- Import the namespace `GameFrameX.GameAnalytics.Runtime` in your project
- Ensure `GameAnalyticsManager` is properly instantiated and registered with `GameFramework`
- Use `Dictionary<string, string>` for custom fields

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for details.

## License

This project is licensed under the MIT License - see [LICENSE.md](LICENSE.md) for details.
