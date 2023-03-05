using Leopotam.Ecs;
using UnityEngine;

namespace BusinessECS
{
    sealed class LevelSystem: IEcsInitSystem, IEcsDestroySystem, IEcsSaveSystem {
        private MainBalance _mainBalance;
        private readonly EcsFilter<LevelComponent, IncomeComponent> _businessFilter = null;

        public void Init() {
            foreach(var businessEntityID in _businessFilter) {
                ref var cachedLevelComponent = ref _businessFilter.Get1(businessEntityID);
                ref var cachedIncomeComponent = ref _businessFilter.Get2(businessEntityID);
                
                LoadData();
                
                RegisterLevelUpButtonListener(businessEntityID);
            }
        }

        public void Destroy() {
            SaveData();
        }

        public void LoadData() {
            foreach(var businessEntityID in _businessFilter) {
                ref var cachedLevelComponent = ref _businessFilter.Get1(businessEntityID);
                
                cachedLevelComponent.Level = PlayerPrefs.GetInt(businessEntityID.ToString() + "level", cachedLevelComponent.LevelData == null ? 0 : cachedLevelComponent.LevelData.InitialLevel);
                cachedLevelComponent.Price = PlayerPrefs.GetInt(businessEntityID.ToString() + "price", cachedLevelComponent.LevelData == null ? 0 : cachedLevelComponent.LevelData.InitialLevelPrice);
            }
        }

        public void SaveData() {
            foreach(var businessEntityID in _businessFilter) {
                var cachedLevelComponent = _businessFilter.Get1(businessEntityID);

                PlayerPrefs.SetInt(businessEntityID.ToString() + "level", cachedLevelComponent.Level);
                PlayerPrefs.SetInt(businessEntityID.ToString() + "price", cachedLevelComponent.Price);
            }
        }

        private void RegisterLevelUpButtonListener(int entityID) {
            _businessFilter.Get1(entityID).LevelUpButton.onClick.AddListener(() => {
                if (_mainBalance.Balance >= _businessFilter.Get1(entityID).Price) {
                    IncreaseLevel(entityID); 
                }
            });
            
        }

        private void IncreaseLevel(int id) {
            ref var levelComponent = ref _businessFilter.Get1(id);
            ref var incomeComponent = ref _businessFilter.Get2(id);

            _mainBalance.Balance -= levelComponent.Price;

            levelComponent.Level++;

            IncomeCalculator.UpdateIncome(ref incomeComponent, levelComponent);
            levelComponent.Price = (levelComponent.Level + 1) * levelComponent.LevelData.InitialLevelPrice;
        }
    }
}
