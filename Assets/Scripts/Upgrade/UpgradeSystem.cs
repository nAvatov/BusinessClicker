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
        private readonly EcsFilter<UpgradeComponent> _upgradesFilter = null;
        private readonly EcsFilter<LevelComponent, IncomeComponent> _businessFilter = null;
        
        public void Init() {
            foreach(var upgradeEntity in _upgradesFilter) {
                ref var cachedUpgradeComponent = ref _upgradesFilter.Get1(upgradeEntity);

                SetTextValues(ref cachedUpgradeComponent);
                LoadData();

                RegisterUpgradeButtonListener(upgradeEntity);
            }
        }

        public void Destroy() {
            SaveData();
        }

        public void LoadData() {
            foreach(var entityID in _upgradesFilter) {
                ref var cachedUpgradeComponent = ref _upgradesFilter.Get1(entityID);
                // Because upgrade providers and income providers relates to same converted entity
                ref var cachedIncomeComponent = ref _businessFilter.Get2(entityID);

                if (PlayerPrefs.GetString(entityID.ToString() + "upgrade1", "not bought") == "bought") {
                    DeactivateUpgradeButton(cachedUpgradeComponent.UpgradeButton1, cachedUpgradeComponent.Price1TMP);
                    cachedIncomeComponent.IncomeMultiplier1 = cachedUpgradeComponent.Upgrade1Data == null ? 0 : cachedUpgradeComponent.Upgrade1Data.IncomeMultiplier;
                }

                if (PlayerPrefs.GetString(entityID.ToString() + "upgrade2", "not bought") == "bought") {
                    DeactivateUpgradeButton(cachedUpgradeComponent.UpgradeButton2, cachedUpgradeComponent.Price2TMP);
                    cachedIncomeComponent.IncomeMultiplier2 = cachedUpgradeComponent.Upgrade2Data == null ? 0 : cachedUpgradeComponent.Upgrade2Data.IncomeMultiplier;
                }
            }
        }

        public void SaveData() {
            foreach(var entityID in _upgradesFilter) {
                var cachedUpgradeComponent = _upgradesFilter.Get1(entityID);

                if (!cachedUpgradeComponent.UpgradeButton1.interactable) {
                    PlayerPrefs.SetString(entityID.ToString() + "upgrade1", "bought");
                }

                if (!cachedUpgradeComponent.UpgradeButton2.interactable) {
                    PlayerPrefs.SetString(entityID.ToString() + "upgrade2", "bought");
                }
            }
        }

        private void SetTextValues(ref UpgradeComponent upgradeComponent) {
            upgradeComponent.Multiplier1TMP.SetText(upgradeComponent.Upgrade1Data.IncomeMultiplier.ToString() + " %");
            upgradeComponent.Price1TMP.SetText(upgradeComponent.Upgrade1Data.Price.ToString() + " $");

            upgradeComponent.Multiplier2TMP.SetText(upgradeComponent.Upgrade2Data.IncomeMultiplier.ToString() + " %");
            upgradeComponent.Price2TMP.SetText(upgradeComponent.Upgrade2Data.Price.ToString() + " $");
        }

        private void RegisterUpgradeButtonListener(int upgradeEntityID) {
            _upgradesFilter.Get1(upgradeEntityID).UpgradeButton1?.onClick.AddListener(() => {
                // If balance is enought and business level more than 0
                if (_mainBalance.Balance >= _upgradesFilter.Get1(upgradeEntityID).Upgrade1Data.Price && _businessFilter.Get1(upgradeEntityID).Level > 0){ 
                    ApplyUpgrade(upgradeEntityID, Upgrades.firstUpgrade);
                }
            });

            _upgradesFilter.Get1(upgradeEntityID).UpgradeButton2?.onClick.AddListener(() => {
                // If balance is enought and business level more than 0
                if (_mainBalance.Balance >= _upgradesFilter.Get1(upgradeEntityID).Upgrade2Data.Price && _businessFilter.Get1(upgradeEntityID).Level > 0) {
                    ApplyUpgrade(upgradeEntityID, Upgrades.secondUpgrade);
                }
            });
        }

        private void ApplyUpgrade(int id, Upgrades upgrade) {
            ref var cachedIncomeComponent = ref _businessFilter.Get2(id);
            ref var cachedUpgradeConponent = ref _upgradesFilter.Get1(id);

            switch(upgrade) {
                case Upgrades.firstUpgrade: {
                    if (_upgradesFilter.Get1(id).Upgrade1Data != null) {
                        cachedIncomeComponent.IncomeMultiplier1 = _upgradesFilter.Get1(id).Upgrade1Data.IncomeMultiplier;

                        _mainBalance.Balance -= cachedUpgradeConponent.Upgrade1Data.Price;

                        DeactivateUpgradeButton(cachedUpgradeConponent.UpgradeButton1, cachedUpgradeConponent.Price1TMP);
                    }
                    break;
                }
                case Upgrades.secondUpgrade: {
                    if (_upgradesFilter.Get1(id).Upgrade2Data != null) {
                        cachedIncomeComponent.IncomeMultiplier2 = _upgradesFilter.Get1(id).Upgrade2Data.IncomeMultiplier;

                        _mainBalance.Balance -= cachedUpgradeConponent.Upgrade2Data.Price;

                        DeactivateUpgradeButton(cachedUpgradeConponent.UpgradeButton2, cachedUpgradeConponent.Price2TMP);
                    }
                    break;
                }
            }

            IncomeCalculator.UpdateIncome(ref cachedIncomeComponent, _businessFilter.Get1(id));
            
        }

        private void DeactivateUpgradeButton(UnityEngine.UI.Button upgradeButton, TMPro.TextMeshProUGUI upgradeTextTMP) {
            upgradeButton.interactable = false;
            upgradeTextTMP.SetText("Куплено");
        }
    }
}
