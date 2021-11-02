using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private SpriteAnimatorConfig _coinsAnimCfg;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private List<LevelObjectView> _coinsViews;
        [SerializeField] private QuestObjectView _singleQuest;
        [SerializeField] private Transform[] _backgroundsTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private GeneratorLevelView _generatorLevelView;

        [Header("Simple AI")]
        [SerializeField] private AIConfig _simplePatrolAIConfig;
        [SerializeField] private LevelObjectView _simplePatrolAIView;

        [Header("Stalker AI")]
        [SerializeField] private AIConfig _stalkerAIConfig;
        [SerializeField] private LevelObjectView _stalkerAIView;
        [SerializeField] private Seeker _stalkerAISeeker;
        [SerializeField] private Transform _stalkerAITarget;

        [Header("Protector AI")]
        [SerializeField] private LevelObjectView _protectorAIView;
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private Transform[] _protectorWaypoints;


        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private SpriteParalaxController _paralaxController;
        private CannonController _cannon;
        private BulletEmitterController _bulletEmitterController;
        private CoinsController _coinsController;
        private SimplePatrolAIController _simplePatrolAIController;
        private StalkerAIController _stalkerAIController;
        private ProtectorAIController _protectorAI;
        private ProtectedZone _protectedZone;
        private GeneratorController _levelGeneratorController;
        private QuestConfiguratorController _questConfigurator;



        void Start()
        {
            _paralaxController = new SpriteParalaxController(_cameraTransform, _backgroundsTransform);
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _coinsAnimCfg = Resources.Load<SpriteAnimatorConfig>("CoinAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerController = new PlayerController(_playerView, _playerAnimator);
            }

            if(_coinsAnimCfg)
            {
                _coinAnimator = new SpriteAnimatorController(_coinsAnimCfg);
            }
            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);

            _cannon = new CannonController(_cannonView._muzzleTransform, _playerView.transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
            _coinsController = new CoinsController(_playerView, _coinsViews, _coinAnimator);
            _simplePatrolAIController = new SimplePatrolAIController(_simplePatrolAIView, new SimplePatrolAIModel(_simplePatrolAIConfig));
            _stalkerAIController = new StalkerAIController(_stalkerAIView, new StalkerAIModel(_stalkerAIConfig), _stalkerAISeeker, _stalkerAITarget);
            InvokeRepeating(nameof(RecalculateAIPath), 0.0f, 1.0f);
            _protectorAI = new ProtectorAIController(_protectorAIView, new PatrolAIModel(_protectorWaypoints), _protectorAIDestinationSetter, _protectorAIPatrolPath);
            _protectorAI.Init();
            _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector> { _protectorAI });
            _protectedZone.Init();

            _levelGeneratorController = new GeneratorController(_generatorLevelView);
            _levelGeneratorController.Init();

            _questConfigurator = new QuestConfiguratorController(_singleQuest);
            _questConfigurator.Init();
        }

        void Update()
        {
            _paralaxController.Update();
            _cameraController.Update();
            _playerController.Update();
            _cannon.Update();
            _bulletEmitterController.Update();
            _coinAnimator.Update();
        }

        void LateUpdate()
        {
            _paralaxController.LateUpdate();
        }

        private void FixedUpdate()
        {
            _simplePatrolAIController.FixedUpdate();
            _stalkerAIController.FixedUpdate();
        }

        private void OnDestroy()
        {
            _protectorAI.Deinit();
            _protectedZone.Deinit();
        }

        private void RecalculateAIPath()
        {
            _stalkerAIController.RecalculatePath();
        }


    }
}