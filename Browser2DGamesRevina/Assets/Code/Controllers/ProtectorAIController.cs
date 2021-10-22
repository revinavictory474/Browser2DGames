using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class ProtectorAIController : IProtector
    {
        private readonly LevelObjectView _view;
        private readonly PatrolAIModel _model;
        private readonly AIDestinationSetter _destinationSetter;
        private readonly AIPatrolPath _patrolPath;

        private bool _isPatrolling;

        public ProtectorAIController(LevelObjectView view, PatrolAIModel model, AIDestinationSetter destinationSetter, AIPatrolPath patrolPath)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            _destinationSetter = destinationSetter != null ? destinationSetter : throw new ArgumentNullException(nameof(patrolPath));
            _patrolPath = patrolPath != null ? patrolPath : throw new ArgumentNullException(nameof(model));
        }

        public void Init()
        {
            _destinationSetter.target = _model.GetNextTarget();
            _isPatrolling = true;
            _patrolPath.TargetReached += OnTargetReached;
        }

        public void Deinit()
        {
            _patrolPath.TargetReached -= OnTargetReached;
        }


        private void OnTargetReached(object sender, EventArgs e)
        {
            _destinationSetter.target = _isPatrolling
                ? _model.GetNextTarget()
                : _model.GetClosestTarget(_view.transform.position);
        }


        public void FinishProtection(GameObject invader)
        {
            _isPatrolling = true;
            _destinationSetter.target = _model.GetClosestTarget(_view.transform.position);
        }

        public void StartProtection(GameObject invader)
        {
            _isPatrolling = false;
            _destinationSetter.target = invader.transform;
        }
    }
}