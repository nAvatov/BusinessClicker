using Leopotam.Ecs;
using UnityEngine;

namespace BusinessECS
{
    sealed class IncomeSystem: IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem, IEcsSaveSystem {
        private MainBalance _mainBalance;
        private readonly EcsFilter<LevelComponent, IncomeComponent> businessFilter = null;
        public void Init() {
            foreach(var businessEntityID in businessFilter) {
                ref var cachedIncomeComponent = ref businessFilter.Get2(businessEntityID);

                LoadData();
                
                RegisterSliderListeners(businessEntityID);
            } 
        }

        public void Run() {
            foreach(var businessEntity in  businessFilter) {
                if (businessFilter.Get1(businessEntity).Level > 0) {
                    ref var cachedIncomeComponent = ref businessFilter.Get2(businessEntity);
                    if (cachedIncomeComponent.incomeData != null) {
                        cachedIncomeComponent.incomeProgress.value += (1f / cachedIncomeComponent.incomeData.IncomeDelay) * UnityEngine.Time.deltaTime;
                    }
                }
            } 
        }

        public void Destroy() {
            SaveData();

            foreach(var entityID in businessFilter) {
                ref var cachedIncomeComponent = ref businessFilter.Get2(entityID);
                cachedIncomeComponent.incomeProgress.onValueChanged.RemoveAllListeners();
            } 
        }

        public void SaveData() {
            PlayerPrefs.SetFloat("balance", _mainBalance.Balance);

            foreach(var entityID in businessFilter) { 
                var cachedIncomeComponent = businessFilter.Get2(entityID);

                PlayerPrefs.SetFloat(entityID.ToString() + "income", cachedIncomeComponent.Income);
                PlayerPrefs.SetFloat(entityID.ToString() + "incomeProgress", cachedIncomeComponent.incomeProgress.value);
            }
        }

        public void LoadData() {
            _mainBalance.Balance = PlayerPrefs.GetFloat("balance", 0f);

            foreach(var entityID in businessFilter) { 
                ref var cachedIncomeComponent = ref businessFilter.Get2(entityID);

                cachedIncomeComponent.Income =  PlayerPrefs.GetFloat(entityID.ToString() + "income", cachedIncomeComponent.incomeData == null ? 0 : cachedIncomeComponent.incomeData.InitialIncome);
                cachedIncomeComponent.incomeProgress.value = PlayerPrefs.GetFloat(entityID.ToString() + "incomeProgress", 0f);
            }
        }

        private void RefreshAllIncome() {
            foreach(var incomeEntity in  businessFilter) {
                businessFilter.Get2(incomeEntity).incomeProgress.value = 0f;
            } 
        }

        private void SetInitialIncome(ref IncomeComponent incomeComponent) {
            if (incomeComponent.incomeData != null) {
                incomeComponent.Income = incomeComponent.incomeData.InitialIncome;
            }
        }

        private void RegisterSliderListeners(int entityID) {
            businessFilter.Get2(entityID).incomeProgress.onValueChanged.AddListener( (float currentValue) => {
                if (currentValue >= 1) {
                    AddIncomeToBalance(entityID);
                }
            });
        }

        private void AddIncomeToBalance(int id) {
            ref var incomeComponent = ref businessFilter.Get2(id);
            ref var levelComponent = ref businessFilter.Get1(id);

            
            _mainBalance.Balance += IncomeCalculator.UpdateIncome(ref incomeComponent, levelComponent);
            incomeComponent.incomeProgress.value = 0f;
        }
    }
}
