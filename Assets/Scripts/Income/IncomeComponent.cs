namespace BusinessECS
{
    [System.Serializable]
    public struct IncomeComponent {
        public UnityEngine.UI.Slider incomeProgress;
        public TMPro.TextMeshProUGUI incomeTMP;
        public IncomeConfig incomeData;
        private float income;
        public int incomeMultiplier1;
        public int incomeMultiplier2;

        public float Income {
            set {
                income = value;
                incomeTMP.SetText(income.ToString());
            }

            get {
                return income;
            }
        }

        public IncomeComponent(int _income = 0) {
            incomeProgress = null;
            incomeTMP = null;
            incomeData = null;

            income = _income;

            incomeMultiplier1 = 0;
            incomeMultiplier2 = 0;
        }
    }
}
