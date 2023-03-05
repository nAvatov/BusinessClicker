using UnityEngine;

namespace BusinessECS
{
    public static class IncomeCalculator {

        public static float UpdateIncome(ref IncomeComponent incomeComponent, LevelComponent levelComponent) {
            return incomeComponent.Income = levelComponent.Level * incomeComponent.incomeData.InitialIncome * UpgradesMultiplierEffect(incomeComponent);
        }
        public static float UpgradesMultiplierEffect(IncomeComponent incomeComponent) {
            return (1 + (float)incomeComponent.incomeMultiplier1/100 + (float)incomeComponent.incomeMultiplier2/100);
        }
    }
}
