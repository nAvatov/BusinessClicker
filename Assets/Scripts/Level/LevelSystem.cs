using Leopotam.Ecs;
using UnityEngine;

namespace BusinessECS
{
    sealed class LevelSystem: IEcsInitSystem, IEcsDestroySystem, IEcsSaveSystem {
        private MainBalance _mainBalance;
        private readonly EcsFilter<LevelComponent, IncomeComponent> businessFilter = null;

        public void Init() {
            foreach(var businessEntityID in businessFilter) {
                ref var cachedLevelComponent = ref businessFilter.Get1(businessEntityID);
                ref var cachedIncomeComponent = ref businessFilter.Get2(businessEntityID);
                
                LoadData();
                
                RegisterLevelUpButtonListener(businessEntityID);
            }
        }

        public void Destroy() {
            SaveData();
        }

        public void LoadData() {
            foreach(var businessEntityID in businessFilter) {
                ref var cachedLevelComponent = ref businessFilter.Get1(businessEntityID);
                
                cachedLevelComponent.Level = PlayerPrefs.GetInt(businessEntityID.ToString() + "level", cachedLevelComponent.levelData == null ? 0 : cachedLevelComponent.levelData.InitialLevel);
                cachedLevelComponent.Price = PlayerPrefs.GetInt(businessEntityID.ToString() + "price", cachedLevelComponent.levelData == null ? 0 : cachedLevelComponent.levelData.InitialLevelPrice);
            }
        }

        public void SaveData() {
            foreach(var businessEntityID in businessFilter) {
                var cachedLevelComponent = businessFilter.Get1(businessEntityID);

                PlayerPrefs.SetInt(businessEntityID.ToString() + "level", cachedLevelComponent.Level);
                PlayerPrefs.SetInt(businessEntityID.ToString() + "price", cachedLevelComponent.Price);
            }
        }

        private void RegisterLevelUpButtonListener(int entityID) {
            businessFilter.Get1(entityID).levelUpButton.onClick.AddListener(() => {
                if (_mainBalance.Balance >= businessFilter.Get1(entityID).Price) {
                    IncreaseLevel(entityID); 
                }
            });
            
        }

        private void IncreaseLevel(int id) {
            ref var levelComponent = ref businessFilter.Get1(id);
            ref var incomeComponent = ref businessFilter.Get2(id);

            _mainBalance.Balance -= levelComponent.Price;

            levelComponent.Level++;

            IncomeCalculator.UpdateIncome(ref incomeComponent, levelComponent);
            levelComponent.Price = (levelComponent.Level + 1) * levelComponent.levelData.InitialLevelPrice;
        }
    }
}
