using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private CannonView _cannonView;

        private SpriteAnimatorController _playerAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private SpriteParalaxController _paralaxController;
        private CannonController _cannon;
        private BulletEmitterController _bulletEmitterController;

        [SerializeField] private Transform[] _backgroundsTransform;
        [SerializeField] private Transform _cameraTransform;
        
        
        void Start()
        {
            _paralaxController = new SpriteParalaxController(_cameraTransform, _backgroundsTransform);
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerController = new PlayerController(_playerView, _playerAnimator);
            }
            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);

            _cannon = new CannonController(_cannonView._muzzleTransform, _playerView.transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
        }

        void Update()
        {
            _paralaxController.Update();
            _cameraController.Update();
            _playerController.Update();
            _cannon.Update();
            _bulletEmitterController.Update();
        }

        void LateUpdate()
        {
            _paralaxController.LateUpdate();
        }
    }
}