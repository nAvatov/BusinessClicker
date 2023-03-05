namespace BusinessECS
{
    [System.Serializable]
    public struct LevelComponent {
        public TMPro.TextMeshProUGUI levelTMP;
        public TMPro.TextMeshProUGUI priceTMP;
        public UnityEngine.UI.Button levelUpButton;
        public LevelConfig levelData;

        private int level;
        private int price;

        public int Level {
            set {
                level = value;
                levelTMP.SetText(level.ToString());
            }

            get {
                return level;
            }
        }

        public int Price {
            set {
                price = value;
                priceTMP.SetText(price.ToString() + " $");
            }

            get {
                return price;
            }
        }

        public LevelComponent(int _level = 0, int _price = 0) {
            level = _level;
            price = _price;

            levelTMP = null;
            priceTMP = null;
            levelUpButton = null;
            levelData = null;
        }
    }
}
