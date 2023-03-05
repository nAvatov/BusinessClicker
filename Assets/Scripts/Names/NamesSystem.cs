using Leopotam.Ecs;

namespace BusinessECS
{
    sealed class NamesSystem: IEcsInitSystem {
        private readonly EcsFilter<NamesComponent> _namesEntityFilter = null;

        public void Init() {
            
            foreach(var namesEntity in _namesEntityFilter) {
                ref var cachedNamesComponent = ref _namesEntityFilter.Get1(namesEntity);

                SetNames(cachedNamesComponent);
            }
        }

        private void SetNames(NamesComponent namesComponent) {
            namesComponent.BusinessName.SetText(namesComponent.NamesData?.BussinessName);
            namesComponent.FirstUpgradeName.SetText(namesComponent.NamesData?.FirstUpgradeName);
            namesComponent.SecondUpgradeName.SetText(namesComponent.NamesData?.SecondUpgradeName);
        }
    }
}
