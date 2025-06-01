using OcuFix.UI;
using Zenject;

namespace OcuFix.Installers
{
    public class OcuFixMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OcuFixSettingsViewController>()
                .FromNewComponentAsViewController()
                .AsSingle();
        }
    }
}