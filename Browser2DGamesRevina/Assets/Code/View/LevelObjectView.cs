using UnityEngine;


namespace PlatformerMVC
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelObjectView : MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer _spriteRenderer;
        public Collider2D _collider;
        public Rigidbody2D _rigidbody2D;
        public TrailRenderer[] _trailRenderer;
        
        void Start()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentsInChildren<TrailRenderer>();
        }

    }
}