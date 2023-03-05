using UnityEngine;

namespace BusinessECS
{
    public static class IncomeCalculator {

        public static float UpdateIncome(ref IncomeComponent incomeComponent, LevelComponent levelComponent) {
            return incomeComponent.Income = levelComponent.Level * incomeComponent.IncomeData.InitialIncome * UpgradesMultiplierEffect(incomeComponent);
        }
        public static float UpgradesMultiplierEffect(IncomeComponent incomeComponent) {
            return (1 + (float)incomeComponent.IncomeMultiplier1/100 + (float)incomeComponent.IncomeMultiplier2/100);
        }
    }
}
