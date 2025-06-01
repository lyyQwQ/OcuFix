using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using OcuFix.Configuration;
using OcuFix.Installers;
using SiraUtil.Zenject;
using UnityEngine;
using UnityEngine.XR;
using IPALogger = IPA.Logging.Logger;

namespace OcuFix
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private bool ShouldIgnore()
        {
            Log.Info($"XRSettings.loadedDeviceName: {XRSettings.loadedDeviceName}");
            Log.Info($"Environment.CommandLine: {Environment.CommandLine}");
            
            // if (!XRSettings.loadedDeviceName.ToLower().Contains("oculus") && PluginConfig.Instance.EnableChecks)
            if (!Environment.CommandLine.ToLower().Contains("oculus") && PluginConfig.Instance.EnableChecks)
            {
                Plugin.Log.Warn("Oculus vrmode not set, ignoring");
                return true;
            }

            if (Environment.CommandLine.ToLower().Contains("fpfc") && PluginConfig.Instance.EnableChecks)
            {
                Plugin.Log.Warn("FPFC mode enabled, ignoring");
                return true;
            }

            return false;
        }

        [Init]
        public void Init(Config config, IPALogger logger, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;

            PluginConfig.Instance = config.Generated<PluginConfig>();
            
            // Install our menu installer
            zenjector.Install<OcuFixMenuInstaller>(Location.Menu);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            if (ShouldIgnore())
                return;

            AswHelper.DisableAswWrapper();
            ProcessPriorityHelper.SwapPrioritiesWrapper();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            if (ShouldIgnore())
                return;

            if (!PluginConfig.Instance.Restore)
                return;

            AswHelper.RestoreAswWrapper();
            ProcessPriorityHelper.SwapPrioritiesWrapper();
        }
    }
}
