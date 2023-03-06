using Leopotam.Ecs;
using UnityEngine;

namespace BusinessECS
{
    sealed class IncomeSystem: IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsSaveSystem {
        private MainBalance _mainBalance;
        private readonly EcsFilter<LevelComponent, IncomeComponent> _businessFilter = null;
        public void Init() {
            foreach(var businessEntityID in _businessFilter) {
                ref var cachedIncomeComponent = ref _businessFilter.Get2(businessEntityID);

                LoadData();
                
                RegisterSliderListeners(businessEntityID);
            } 
        }

        public void Run() {
            foreach(var businessEntity in  _businessFilter) {
                if (_businessFilter.Get1(businessEntity).Level > 0) {
                    ref var cachedIncomeComponent = ref _businessFilter.Get2(businessEntity);
                    if (cachedIncomeComponent.IncomeData != null) {
                        cachedIncomeComponent.IncomeProgress.value += (1f / cachedIncomeComponent.IncomeData.IncomeDelay) * UnityEngine.Time.deltaTime;
                    }
                }
            } 
        }

        public void Destroy() {
            SaveData();

            foreach(var entityID in _businessFilter) {
                ref var cachedIncomeComponent = ref _businessFilter.Get2(entityID);
                cachedIncomeComponent.IncomeProgress.onValueChanged.RemoveAllListeners();
            } 
        }

        public void SaveData() {
            PlayerPrefs.SetFloat("balance", _mainBalance.Balance);

            foreach(var entityID in _businessFilter) { 
                var cachedIncomeComponent = _businessFilter.Get2(entityID);

                PlayerPrefs.SetFloat(entityID.ToString() + "income", cachedIncomeComponent.Income);
                PlayerPrefs.SetFloat(entityID.ToString() + "incomeProgress", cachedIncomeComponent.IncomeProgress.value);
            }
        }

        public void LoadData() {
            _mainBalance.Balance = PlayerPrefs.GetFloat("balance", 0f);

            foreach(var entityID in _businessFilter) { 
                ref var cachedIncomeComponent = ref _businessFilter.Get2(entityID);
                // Get saved income value from PlayerPrefs. Initial data from config varian returned by default.
                cachedIncomeComponent.Income =  PlayerPrefs.GetFloat(entityID.ToString() + "income", cachedIncomeComponent.IncomeData == null ? 0 : cachedIncomeComponent.IncomeData.InitialIncome);
                cachedIncomeComponent.IncomeProgress.value = PlayerPrefs.GetFloat(entityID.ToString() + "incomeProgress", 0f);
            }
        }

        private void RegisterSliderListeners(int entityID) {
            _businessFilter.Get2(entityID).IncomeProgress.onValueChanged.AddListener( (float currentValue) => {
                if (currentValue >= 1) {
                    AddIncomeToBalance(entityID);
                }
            });
        }

        private void AddIncomeToBalance(int id) {
            ref var incomeComponent = ref _businessFilter.Get2(id);
            ref var levelComponent = ref _businessFilter.Get1(id);

            
            _mainBalance.Balance += IncomeCalculator.UpdateIncome(ref incomeComponent, levelComponent);
            incomeComponent.IncomeProgress.value = 0f;
        }
    }
}
