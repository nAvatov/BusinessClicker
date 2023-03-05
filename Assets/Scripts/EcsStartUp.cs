using System;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace BusinessECS
{
    public partial class EcsStartUp : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _balanceTMP;
        private MainBalance _mainBalance;
        /// <summary>
        /// ECS 
        /// </summary>
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start() {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _mainBalance = new MainBalance(_balanceTMP);

            InjectData();
            // AddOneFramedComponents();
            AddSystems();

            _systems.ConvertScene();

            _systems.Init();
        }

        private void AddOneFramedComponents()
        {
            throw new NotImplementedException();
        }

        private void InjectData()
        {
           _systems.Inject(_mainBalance);
        }

        private void AddSystems()
        {
            _systems
                .Add(new NamesSystem())
                .Add(new LevelSystem())
                .Add(new IncomeSystem())
                .Add(new UpgradeSystem());
        }

        private void Update() {
            _systems.Run();
        }

        private void OnDestroy() {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;

            _world.Destroy();
            _world = null;
        }
    } 

    class MainBalance {
        [SerializeField] private TMPro.TextMeshProUGUI _balanceTMP;
        private float _balance;

        public float Balance {
            get {
                return _balance;
            }

            set {
                _balance = value;
                _balanceTMP.SetText(_balance.ToString());
            }
        }

        public MainBalance(TMPro.TextMeshProUGUI tmpReference, int initialBalance = 0) {
            _balanceTMP = tmpReference;
            _balance = initialBalance;
        }
    } 

    public interface IEcsSaveSystem {
        public void LoadData();
        public void SaveData();
    } 
}
