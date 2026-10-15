// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任。

using System.Collections.Generic;
using GameFrameX.GameAnalytics.Runtime;

namespace GameFrameX.GameAnalytics.GravityEngine.Runtime
{
    /// <summary>
    /// GravityEngine 统计渠道启动注册
    /// </summary>
    /// <remarks>
    /// 在 BeforeSceneLoad 阶段将渠道注册进 <see cref="GameAnalyticsRegistry"/>（注册即生效，事件广播到全部注册渠道）；
    /// 未安装本包时代码被裁剪，注册不会发生（引用即注册）。
    /// 渠道配置（accessToken / clientId / channel / debug / version）为启动期强类型配置：游戏侧在场景加载前
    /// 对 <see cref="Setting"/> 赋值即可；管理器在首次被注册表获取时才实例化，因此赋值时机宽松。
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    internal static class GravityEngineAnalyticsBootstrap
    {
        /// <summary>
        /// 获取或设置 GravityEngine 渠道配置（启动期由游戏侧赋值，默认值为空配置）
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static GameAnalyticsGravityEngineSetting Setting { get; set; } = new GameAnalyticsGravityEngineSetting();

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        [UnityEngine.Scripting.Preserve]
        private static void Register()
        {
            GameAnalyticsRegistry.Register(new GameAnalyticsProviderRegistration(
                GameAnalyticsByGravityEngineManager.ChannelId,
                "GravityEngine",
                () =>
                {
                    var manager = new GameAnalyticsByGravityEngineManager();
                    var setting = Setting ?? new GameAnalyticsGravityEngineSetting();
                    manager.Init(new Dictionary<string, string>
                    {
                        ["accessToken"] = setting.accessToken,
                        ["channel"] = setting.channel,
                        ["clientId"] = setting.clientId,
                        ["debug"] = setting.debug.ToString().ToLowerInvariant(),
                        ["version"] = setting.version.ToString(),
                    });
                    return manager;
                }));
        }
    }
}
