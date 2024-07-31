using System.Collections.Generic;
using GravityEngine;
using UnityEngine;

namespace GameFrameX.GameAnalytics.GravityEngine.Runtime
{
    public class InitializeCallbackImpl : IInitializeCallback
    {
        // 初始化失败之后回调，errorMsg为报错信息
        public void onFailed(string errorMsg)
        {
            Debug.Log("GravityEngine initialize failed! Error: " + errorMsg);
        }

        // 初始化成功之后回调
        public void onSuccess(Dictionary<string, object> responseJson)
        {
            Debug.Log("GravityEngine initialize success!");
            GravityEngineAPI.Flush();
        }
    }
}