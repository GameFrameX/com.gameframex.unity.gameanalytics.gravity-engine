<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />

  # GameFrameX GameAnalytics Gravity Engine

  [![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine)](https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine/releases)
  [![License](https://img.shields.io/badge/license-MIT-orange.svg)](LICENSE.md)
  [![Documentation](https://img.shields.io/badge/docs-gameframex-blue.svg)](https://gameframex.doc.alianblank.com)

  인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

  [문서](https://gameframex.doc.alianblank.com) | [빠른 시작](#빠른-시작)

  [English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**
</div>

---

## 프로젝트 개요

GameFrameX GameAnalytics Gravity Engine 컴포넌트 - Gravity Engine 기반 게임 분석 기능 통합 및 사용을 위한 인터페이스를 제공합니다. 이벤트 추적, 타이머 기능 및 사용자 정의 이벤트 보고를 포함합니다.

## 특징

- **이벤트 추적** - 단순 이벤트, 값 기반 이벤트 및 사용자 정의 필드 이벤트 보고
- **타이머 기능** - 이벤트 지속 시간 측정을 위한 타이머 시작 및 중지
- **초기화 가드** - 모든 메서드는 실행 전 초기화 상태를 확인
- **사용자 정의 필드** - 딕셔너리 기반 사용자 정의 이벤트 데이터 지원

## 설치

### Git URL을 통해 설치 (권장)

1. Unity 에디터에서 Package Manager 열기
2. "+" 버튼을 클릭하고 "Add package from git URL" 선택
3. 다음 URL 입력:
   ```
   https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git
   ```

### manifest.json을 통해 설치

프로젝트의 `Packages/manifest.json`에 다음을 추가:

```json
{
  "dependencies": {
    "com.gameframex.unity.gameanalytics.gravity-engine": "https://github.com/GameFrameX/com.gameframex.unity.gameanalytics.gravity-engine.git"
  }
}
```

### 수동 설치

1. 최신 릴리스 패키지 다운로드
2. 프로젝트의 `Packages` 디렉토리에 압축 해제
3. Unity가 자동으로 패키지를 인식하고 로드합니다

## 빠른 시작

### 초기화

Unity의 `Awake` 메서드에서 `GameAnalyticsComponent`를 초기화합니다. `Init()`을 호출하여 초기화를 완료합니다:

```csharp
using GameFrameX.GameAnalytics.Runtime;

public class GameAnalyticsExample : MonoBehaviour
{
    private void Awake()
    {
        // GameAnalyticsComponent 가져오기 및 초기화
        var gameAnalyticsComponent = GameEntry.GetComponent<GameAnalyticsComponent>();
        gameAnalyticsComponent.Init();
    }
}
```

### 타이머 기능

```csharp
// 타이머 시작
gameAnalyticsComponent.StartTimer("level_complete");

// 타이머 중지
gameAnalyticsComponent.StopTimer("level_complete");
```

### 이벤트 보고

```csharp
// 단순 이벤트
gameAnalyticsComponent.Event("button_clicked");

// 값이 포함된 이벤트
gameAnalyticsComponent.Event("score_achieved", 100.0f);

// 사용자 정의 필드가 포함된 이벤트
var customFields = new Dictionary<string, string>
{
    { "level", "5" },
    { "character", "warrior" }
};
gameAnalyticsComponent.Event("level_started", customFields);

// 값과 사용자 정의 필드가 포함된 이벤트
gameAnalyticsComponent.Event("level_completed", 95.5f, customFields);
```

## 사용 시 주의사항

- 컴포넌트의 메서드를 호출하기 전에 올바르게 초기화되었는지 확인하세요
- 초기화되지 않은 경우 작업이 수행되지 않으며, 초기화 후에만 이벤트 보고 또는 타이머가 유효합니다
- 데이터 분석의 정확성을 보장하기 위해 이벤트 이름은 대표성 있고 고유해야 합니다

## 통합 팁

- 프로젝트에서 네임스페이스 `GameFrameX.GameAnalytics.Runtime`을 임포트하세요
- `GameAnalyticsManager`가 올바르게 인스턴스화되고 `GameFramework`에 등록되었는지 확인하세요
- 사용자 정의 필드에는 `Dictionary<string, string>` 유형을 사용하세요

## 변경 로그

자세한 내용은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다. 자세한 내용은 [LICENSE.md](LICENSE.md)를 참조하세요.
