using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private int _animationSpeed = 10;

        private SpriteAnimatorController _playerAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private SpriteParalaxController _paralaxController;

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
        }

        void Update()
        {
            _paralaxController.Update();
            _cameraController.Update();
            _playerController.Update();
        }

        void LateUpdate()
        {
            _paralaxController.LateUpdate();
        }
    }
}