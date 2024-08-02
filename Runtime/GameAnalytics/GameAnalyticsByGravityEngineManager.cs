using System.Collections.Generic;
using GameFrameX.GameAnalytics.Runtime;
using GameFrameX.Runtime;
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
        private readonly Dictionary<string, object>        m_publicProperties = new Dictionary<string, object>();
        private readonly Dictionary<string, string>        Args               = new Dictionary<string, string>();
        private          GameAnalyticsGravityEngineSetting m_GameAnalyticsSetting;

        public override void Init(Dictionary<string, string> args)
        {
            Log.Info("GameAnalyticsByGravityEngineManager Init, args:" + Utility.Json.ToJson(args));
            foreach (var arg in args)
            {
                Args[arg.Key] = arg.Value;
            }

            var gravityEngineAPI = Object.FindObjectOfType<GravityEngineAPI>();
            if (gravityEngineAPI == null)
            {
                Debug.LogError("在场景中找不到GravityEngineAPI组件,请确保场景中存在一个GravityEngineAPI组件");
                return;
            }

            m_GameAnalyticsSetting = Utility.Json.ToObject<GameAnalyticsGravityEngineSetting>(Utility.Json.ToJson(Args));

            if (m_GameAnalyticsSetting == null)
            {
                Debug.LogError("GameAnalyticsByGravityEngineManager ManualInit: GameAnalyticsGravityEngineSetting is null");
                return;
            }
        }

        public override void ManualInit(Dictionary<string, string> args)
        {
            Init(args);
            Log.Info("GameAnalyticsByGravityEngineManager ManualInit with accessToken:" + m_GameAnalyticsSetting.accessToken + ", clientId:" + m_GameAnalyticsSetting.clientId + ", channel:" + m_GameAnalyticsSetting.channel);

            GravityEngineAPI.StartGravityEngine(m_GameAnalyticsSetting.accessToken, m_GameAnalyticsSetting.clientId, m_GameAnalyticsSetting.debug ? GravityEngineAPI.SDKRunMode.DEBUG : GravityEngineAPI.SDKRunMode.NORMAL, m_GameAnalyticsSetting.channel);
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

            m_IsInit = true;
            Log.Info("GameAnalyticsByGravityEngineManager ManualInit Success");

            GravityEngineAPI.Initialize(m_GameAnalyticsSetting.clientId, "default", m_GameAnalyticsSetting.version, m_GameAnalyticsSetting.clientId, false, new InitializeCallbackImpl());
        }

        public override bool IsManualInit()
        {
            return true;
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