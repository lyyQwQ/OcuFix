using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using OcuFix.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Zenject;

namespace OcuFix.UI
{
    [HotReload]
    public class OcuFixSettingsViewController : BSMLAutomaticViewController, INotifyPropertyChanged, IInitializable
    {
        public string ResourceName => "OcuFix.UI.Views.Settings.bsml";
        [UIValue("DisableASW")]
        public bool DisableASW
        {
            get => PluginConfig.Instance.DisableASW;
            set 
            { 
                PluginConfig.Instance.DisableASW = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("SetPriority")]
        public bool SetPriority
        {
            get => PluginConfig.Instance.SetPriority;
            set 
            { 
                PluginConfig.Instance.SetPriority = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("OculusPriorityHigh")]
        public bool OculusPriorityHigh
        {
            get => PluginConfig.Instance.OculusPriorityHigh;
            set 
            { 
                PluginConfig.Instance.OculusPriorityHigh = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("GamePriority")]
        public bool GamePriority
        {
            get => PluginConfig.Instance.GamePriority;
            set 
            { 
                PluginConfig.Instance.GamePriority = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("Restore")]
        public bool Restore
        {
            get => PluginConfig.Instance.Restore;
            set 
            { 
                PluginConfig.Instance.Restore = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("EnableChecks")]
        public bool EnableChecks
        {
            get => PluginConfig.Instance.EnableChecks;
            set 
            { 
                PluginConfig.Instance.EnableChecks = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            BSMLSettings.Instance.AddSettingsMenu("OcuFix", ResourceName, this);
            Plugin.Log.Info("OcuFix settings menu registered via Zenject");
        }
    }
}