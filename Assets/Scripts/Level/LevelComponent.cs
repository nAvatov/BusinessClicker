namespace BusinessECS
{
    [System.Serializable]
    public struct LevelComponent {
        [UnityEngine.SerializeField] private TMPro.TextMeshProUGUI _levelTMP;
        [UnityEngine.SerializeField] private TMPro.TextMeshProUGUI _priceTMP;
        public UnityEngine.UI.Button LevelUpButton;
        public LevelConfig LevelData;

        private int _level;
        private int _price;

        public int Level {
            set {
                _level = value;
                _levelTMP.SetText(_level.ToString());
            }

            get {
                return _level;
            }
        }

        public int Price {
            set {
                _price = value;
                _priceTMP.SetText(_price.ToString() + " $");
            }

            get {
                return _price;
            }
        }

        public LevelComponent(int _level = 0, int _price = 0) {
            this._level = _level;
            this._price = _price;

            _levelTMP = null;
            _priceTMP = null;
            LevelUpButton = null;
            LevelData = null;
        }
    }
}
