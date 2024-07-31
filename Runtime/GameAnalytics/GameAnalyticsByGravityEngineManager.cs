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
        private readonly Dictionary<string, object> m_publicProperties = new Dictionary<string, object>();

        private readonly Dictionary<string, string> m_Args = new Dictionary<string, string>();

        public override void Init(Dictionary<string, string> args)
        {
            Log.Info("GameAnalyticsByGravityEngineManager Init, args:" + Utility.Json.ToJson(args));

            m_Args["accessToken"] = args["accessToken"];
            m_Args["channel"] = args["channel"];
            m_Args["debug"] = args["debug"];
        }

        public override void ManualInit(Dictionary<string, string> args)
        {
            Log.Info("GameAnalyticsByGravityEngineManager ManualInit, args:" + Utility.Json.ToJson(args));

            var gravityEngineAPI = Object.FindObjectOfType<GravityEngineAPI>();
            if (gravityEngineAPI == null)
            {
                Debug.LogError("在场景中找不到GravityEngineAPI组件,请确保场景中存在一个GravityEngineAPI组件");
                return;
            }

            m_Args["clientId"] = args["clientId"];

            bool debug = false;
            if (m_Args.ContainsKey("debug"))
            {
                debug = m_Args["debug"] == "true";
            }

            Log.Info("GameAnalyticsByGravityEngineManager ManualInit with accessToken:" + m_Args["accessToken"] + ", clientId:" + m_Args["clientId"] + ", channel:" + m_Args["channel"]);

            GravityEngineAPI.StartGravityEngine(m_Args["accessToken"], m_Args["clientId"], debug ? GravityEngineAPI.SDKRunMode.DEBUG : GravityEngineAPI.SDKRunMode.NORMAL, m_Args["channel"]);
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

            int version = 1;
            if (m_Args.ContainsKey("version"))
            {
                version = int.Parse(m_Args["version"]);
            }
            GravityEngineAPI.Initialize(m_Args["clientId"], "default", version, m_Args["clientId"], false, new InitializeCallbackImpl());
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