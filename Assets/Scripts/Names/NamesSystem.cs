using Leopotam.Ecs;

namespace BusinessECS
{
    sealed class NamesSystem: IEcsInitSystem, IEcsRunSystem {
        private readonly EcsFilter<NamesComponent> namesEntityFilter = null;

        public void Init() {
            
            foreach(var namesEntity in namesEntityFilter) {
                ref var cachedNamesComponent = ref namesEntityFilter.Get1(namesEntity);

                SetNames(cachedNamesComponent);
            }
        }

        public void Run() {
            
        }

        private void SetNames(NamesComponent namesComponent) {
            namesComponent.businessName.SetText(namesComponent.namesData?.BussinessName);
            namesComponent.firstUpgradeName.SetText(namesComponent.namesData?.FirstUpgradeName);
            namesComponent.secondUpgradeName.SetText(namesComponent.namesData?.SecondUpgradeName);
            // if (namesComponent.namesData != null) {
            //     namesComponent.businessName.SetText(namesComponent.namesData.BussinessName);
                
            //     if (namesComponent.firstUpgradeName != null) {
            //         namesComponent.firstUpgradeName.SetText(namesComponent.namesData.FirstUpgradeName);
            //     }

            //     if (namesComponent.secondUpgradeName != null) {
            //         namesComponent.secondUpgradeName.SetText(namesComponent.namesData.SecondUpgradeName);
            //     }
            // }
        }
    }
}
