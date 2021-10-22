using UnityEngine;
using Pathfinding;
using System;

namespace PlatformerMVC
{
    public class StalkerAIController
    {
        private readonly LevelObjectView _view;
        private readonly StalkerAIModel _model;
        private readonly Seeker _seeker;
        private readonly Transform _target;

        public StalkerAIController(LevelObjectView view, StalkerAIModel model, Seeker seeker, Transform target)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));

            _model = model != null ? model : throw new ArgumentNullException(nameof(model));

            _seeker = seeker != null ? seeker : throw new ArgumentNullException(nameof(seeker));

            _target = target != null ? target : throw new ArgumentNullException(nameof(target));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.transform.position) * Time.fixedDeltaTime;
            _view._rigidbody2D.velocity = newVelocity;
        }

        public void RecalculatePath()
        {
            if(_seeker.IsDone())
            {
                _seeker.StartPath(_view._rigidbody2D.position, _target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path path)
        {
            if (path.error) return;
            _model.UpdatePath(path);
        }
    }
}