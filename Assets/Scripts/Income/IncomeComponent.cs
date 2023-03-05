namespace BusinessECS
{
    [System.Serializable]
    public struct IncomeComponent {
        [UnityEngine.SerializeField] private  TMPro.TextMeshProUGUI _incomeTMP;
        public UnityEngine.UI.Slider IncomeProgress;
        public IncomeConfig IncomeData;
        private float _income;
        private int _incomeMultiplier1;
        private int _incomeMultiplier2;

        public float Income {
            set {
                _income = value;
                _incomeTMP.SetText(_income.ToString());
            }

            get {
                return _income;
            }
        }

        public int IncomeMultiplier1 {
            get {
                return _incomeMultiplier1;
            }

            set {
                _incomeMultiplier1 = value;
            }
        }

        public int IncomeMultiplier2 {
            get {
                return _incomeMultiplier2;
            }

            set {
                _incomeMultiplier2 = value;
            }
        }

        public IncomeComponent(int income = 0) {
            IncomeProgress = null;
            _incomeTMP = null;
            IncomeData = null;

            _income = income;

            _incomeMultiplier1 = 0;
            _incomeMultiplier2 = 0;
        }
    }
}
