using System;
using GameFrameX.GameAnalytics.Runtime;
using UnityEngine;

namespace GameFrameX.GameAnalytics.GravityEngine.Runtime
{
    [Serializable]
    public sealed class GameAnalyticsGravityEngineSetting : BaseGameAnalyticsSetting
    {
        [Header("AccessToken")] public string accessToken;

        [Header("渠道")] public string channel;

        [Header("是否开启调试模式")] public bool   debug;
        [Header("客户端ID")]    public string clientId;
        [Header("游戏版本号")]    public int    version;
    }
}