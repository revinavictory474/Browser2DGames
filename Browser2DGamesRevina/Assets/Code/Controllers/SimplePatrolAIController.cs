using UnityEngine;

namespace PlatformerMVC
{
    public class SimplePatrolAIController
    {
        private readonly LevelObjectView _view;
        private readonly SimplePatrolAIModel _model;

        public SimplePatrolAIController(LevelObjectView view, SimplePatrolAIModel model)
        {
            _view = view;
            _model = model;
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.transform.position) * Time.fixedDeltaTime;
            _view._rigidbody2D.velocity = newVelocity;
        }
    }
}