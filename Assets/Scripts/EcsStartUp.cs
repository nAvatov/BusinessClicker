using System;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace BusinessECS
{
    public partial class EcsStartUp : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI balanceTMP;
        MainBalance mainBalance;
        /// <summary>
        /// ECS 
        /// </summary>
        private EcsWorld world;
        private EcsSystems systems;

        private void Start() {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            mainBalance = new MainBalance(balanceTMP);

            InjectData();
            // AddOneFramedComponents();
            AddSystems();

            systems.ConvertScene();

            systems.Init();
        }

        private void AddOneFramedComponents()
        {
            throw new NotImplementedException();
        }

        private void InjectData()
        {
           systems.Inject(mainBalance);
        }

        private void AddSystems()
        {
            systems
                .Add(new NamesSystem())
                .Add(new LevelSystem())
                .Add(new IncomeSystem())
                .Add(new UpgradeSystem());
        }

        private void Update() {
            systems.Run();
        }

        private void OnDestroy() {
            if (systems == null) return;

            systems.Destroy();
            systems = null;

            world.Destroy();
            world = null;
        }
    } 

    class MainBalance {
        [SerializeField] TMPro.TextMeshProUGUI balanceTMP;
        private float balance;

        public float Balance {
            get {
                return balance;
            }

            set {
                balance = value;
                balanceTMP.SetText(balance.ToString());
            }
        }

        public MainBalance(TMPro.TextMeshProUGUI tmpReference, int initialBalance = 0) {
            balanceTMP = tmpReference;
            balance = initialBalance;
        }
    } 

    public interface IEcsSaveSystem {
        public void LoadData();
        public void SaveData();
    } 
}
