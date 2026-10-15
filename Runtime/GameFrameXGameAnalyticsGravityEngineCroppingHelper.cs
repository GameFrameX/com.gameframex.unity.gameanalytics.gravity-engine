using UnityEngine;

namespace GameFrameX.GameAnalytics.GravityEngine.Runtime
{
    [UnityEngine.Scripting.Preserve]
    internal class GameFrameXGameAnalyticsGravityEngineCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _ = typeof(GameAnalyticsByGravityEngineManager);
            _ = typeof(GravityEngineAnalyticsBootstrap);
        }
    }
}