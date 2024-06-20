using System.Collections.Generic;
using GameFrameX.GameAnalytics.Runtime;
using GravityEngine;
using GravitySDK.PC.Constant;
using UnityEngine;

namespace GameFrameX.GameAnalytics.GravityEngine.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameAnalyticsByGravityEngineManager : BaseGameAnalyticsManager
    {
        private readonly Dictionary<string, object> m_publicProperties = new Dictionary<string, object>();

        public override void Init(string appid, string channel, string appKey, string secretKey)
        {
            var gravityEngineAPI = Object.FindObjectOfType<GravityEngineAPI>();
            if (gravityEngineAPI == null)
            {
                Debug.LogError("在场景中找不到GravityEngineAPI组件,请确保场景中存在一个GravityEngineAPI组件");
                return;
            }

            GravityEngineAPI.StartGravityEngine(appid, null, GravityEngineAPI.SDKRunMode.NORMAL, channel);
#if UNITY_WEBGL
#if ENABLE_WECHAT_MINI_GAME && GRAVITY_WECHAT_GAME_MODE
            GravityEngineAPI.EnableAutoTrack(AUTO_TRACK_EVENTS.WECHAT_GAME_ALL);
#elif ENABLE_DOUYIN_MINI_GAME && GRAVITY_BYTEDANCE_GAME_MODE
            GravityEngineAPI.EnableAutoTrack(AUTO_TRACK_EVENTS.BYTEDANCE_GAME_ALL);
#else
            GravityEngineAPI.EnableAutoTrack(AUTO_TRACK_EVENTS.APP_ALL);
#endif
#else
            GravityEngineAPI.EnableAutoTrack(AUTO_TRACK_EVENTS.APP_ALL);
#endif
        }

        public override void SetPublicProperties(string key, object value)
        {
            m_publicProperties[key] = value;
            GravityEngineAPI.SetSuperProperties(m_publicProperties);
        }

        public override void ClearPublicProperties()
        {
            m_publicProperties.Clear();
            GravityEngineAPI.ClearSuperProperties();
        }

        public override Dictionary<string, object> GetPublicProperties()
        {
            return GravityEngineAPI.GetSuperProperties();
        }

        public override void StartTimer(string eventName)
        {
            GravityEngineAPI.TimeEvent(eventName);
            GravityEngineAPI.Flush();
        }

        public override void PauseTimer(string eventName)
        {
            GravityEngineAPI.TimeEvent(eventName);
            GravityEngineAPI.Flush();
        }

        public override void ResumeTimer(string eventName)
        {
            GravityEngineAPI.TimeEvent(eventName);
            GravityEngineAPI.Flush();
        }

        public override void StopTimer(string eventName)
        {
            GravityEngineAPI.TimeEvent(eventName);
            GravityEngineAPI.Flush();
        }

        public override void Event(string eventName)
        {
            GravityEngineAPI.Track(eventName);
            GravityEngineAPI.Flush();
        }

        public override void Event(string eventName, float eventValue)
        {
            var customF = new Dictionary<string, object>
            {
                [NumberKey] = eventValue
            };
            GravityEngineAPI.Track(eventName, customF);
            GravityEngineAPI.Flush();
        }

        public override void Event(string eventName, Dictionary<string, object> customF)
        {
            GravityEngineAPI.Track(eventName, customF);
            GravityEngineAPI.Flush();
        }

        public override void Event(string eventName, float eventValue, Dictionary<string, object> customF)
        {
            customF[NumberKey] = eventValue;
            GravityEngineAPI.Track(eventName, customF);
            GravityEngineAPI.Flush();
        }

        const string NumberKey = "__event_value__";
    }
}