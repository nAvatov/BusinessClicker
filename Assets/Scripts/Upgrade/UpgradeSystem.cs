using Leopotam.Ecs;
using UnityEngine;

public enum Upgrades {
    firstUpgrade,
    secondUpgrade
}

namespace BusinessECS
{
    sealed class UpgradeSystem: IEcsInitSystem, IEcsDestroySystem, IEcsSaveSystem {
        private MainBalance _mainBalance;
        private readonly EcsFilter<UpgradeComponent> upgradesFilter = null;
        private readonly EcsFilter<LevelComponent, IncomeComponent> businessFilter = null;
        
        public void Init() {
            foreach(var upgradeEntity in upgradesFilter) {
                ref var cachedUpgradeComponent = ref upgradesFilter.Get1(upgradeEntity);

                SetTextValues(ref cachedUpgradeComponent);
                LoadData();

                RegisterUpgradeButtonListener(upgradeEntity);
            }
        }

        public void Destroy() {
            SaveData();
        }

        public void LoadData() {
            foreach(var entityID in upgradesFilter) {
                ref var cachedUpgradeComponent = ref upgradesFilter.Get1(entityID);
                ref var cachedIncomeComponent = ref businessFilter.Get2(entityID); // Because upgrade providers and income providers relates to same converted entity

                if (PlayerPrefs.GetString(entityID.ToString() + "upgrade1", "not bought") == "bought") {
                    DeactivateUpgradeButton(cachedUpgradeComponent.upgradeButton1, cachedUpgradeComponent.price1TMP);
                    cachedIncomeComponent.incomeMultiplier1 = cachedUpgradeComponent.upgradeData1 == null ? 0 : cachedUpgradeComponent.upgradeData1.IncomeMultiplier;
                }

                if (PlayerPrefs.GetString(entityID.ToString() + "upgrade2", "not bought") == "bought") {
                    DeactivateUpgradeButton(cachedUpgradeComponent.upgradeButton2, cachedUpgradeComponent.price2TMP);
                    cachedIncomeComponent.incomeMultiplier2 = cachedUpgradeComponent.upgradeData2 == null ? 0 : cachedUpgradeComponent.upgradeData2.IncomeMultiplier;
                }
            }
        }

        public void SaveData() {
            foreach(var entityID in upgradesFilter) {
                var cachedUpgradeComponent = upgradesFilter.Get1(entityID);

                if (!cachedUpgradeComponent.upgradeButton1.interactable) {
                    PlayerPrefs.SetString(entityID.ToString() + "upgrade1", "bought");
                }

                if (!cachedUpgradeComponent.upgradeButton2.interactable) {
                    PlayerPrefs.SetString(entityID.ToString() + "upgrade2", "bought");
                }
            }
        }

        private void SetTextValues(ref UpgradeComponent upgradeComponent) {
            upgradeComponent.multiplier1TMP.SetText(upgradeComponent.upgradeData1.IncomeMultiplier.ToString() + " %");
            upgradeComponent.price1TMP.SetText(upgradeComponent.upgradeData1.Price.ToString() + " $");

            upgradeComponent.multiplier2TMP.SetText(upgradeComponent.upgradeData2.IncomeMultiplier.ToString() + " %");
            upgradeComponent.price2TMP.SetText(upgradeComponent.upgradeData2.Price.ToString() + " $");
        }

        private void RegisterUpgradeButtonListener(int upgradeEntityID) {
            upgradesFilter.Get1(upgradeEntityID).upgradeButton1?.onClick.AddListener(() => {
                if (_mainBalance.Balance >= upgradesFilter.Get1(upgradeEntityID).upgradeData1.Price && businessFilter.Get1(upgradeEntityID).Level > 0){ // if balance is enough and business bought
                    ApplyUpgrade(upgradeEntityID, Upgrades.firstUpgrade);
                }
            });

            upgradesFilter.Get1(upgradeEntityID).upgradeButton2?.onClick.AddListener(() => {
                if (_mainBalance.Balance >= upgradesFilter.Get1(upgradeEntityID).upgradeData2.Price && businessFilter.Get1(upgradeEntityID).Level > 0) { // if balance is enough and business bought
                    ApplyUpgrade(upgradeEntityID, Upgrades.secondUpgrade);
                }
            });
        }

        private void ApplyUpgrade(int id, Upgrades upgrade) {
            ref var cachedIncomeComponent = ref businessFilter.Get2(id);
            ref var cachedLevelComponent = ref businessFilter.Get1(id);
            ref var cachedUpgradeConponent = ref upgradesFilter.Get1(id);

            switch(upgrade) {
                case Upgrades.firstUpgrade: {
                    if (upgradesFilter.Get1(id).upgradeData1 != null) {
                        cachedIncomeComponent.incomeMultiplier1 = upgradesFilter.Get1(id).upgradeData1.IncomeMultiplier;

                        _mainBalance.Balance -= cachedUpgradeConponent.upgradeData1.Price;

                        DeactivateUpgradeButton(cachedUpgradeConponent.upgradeButton1, cachedUpgradeConponent.price1TMP);
                    }
                    break;
                }
                case Upgrades.secondUpgrade: {
                    if (upgradesFilter.Get1(id).upgradeData2 != null) {
                        cachedIncomeComponent.incomeMultiplier2 = upgradesFilter.Get1(id).upgradeData2.IncomeMultiplier;

                        _mainBalance.Balance -= cachedUpgradeConponent.upgradeData2.Price;

                        DeactivateUpgradeButton(cachedUpgradeConponent.upgradeButton2, cachedUpgradeConponent.price2TMP);
                    }
                    break;
                }
            }

            IncomeCalculator.UpdateIncome(ref cachedIncomeComponent, cachedLevelComponent);
            
        }

        private void DeactivateUpgradeButton(UnityEngine.UI.Button upgradeButton, TMPro.TextMeshProUGUI upgradeTextTMP) {
            upgradeButton.interactable = false;
            upgradeTextTMP.SetText("Куплено");
        }
    }
}
