using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        public event EventHandler<IQuest> Completed;
        public bool IsCompleted { get; private set; }

        private QuestObjectView _view;
        private IQuestModel _model;
        private bool _active;

        public QuestController(QuestObjectView view, IQuestModel model)
        {
            _view = view;
            _model = model;
        }

        private void OnContact(LevelObjectView arg)
        {
            bool completed = _model.TryComplete(arg.gameObject);

            if(completed)
                Complete();
        }

        private void Complete()
        {
            if (!_active)
                return;

            _active = false;
            _view.OnLevelObjectContact -= OnContact;
            _view.ProcessComplete();
            Completed?.Invoke(this, this);
        }

        public void Reset()
        {
            if (_active)
                return;

            _active = true;
            _view.OnLevelObjectContact += OnContact;
            _view.ProcessReset();
        }

        public void Dispose()
        {
            _view.OnLevelObjectContact -= OnContact;
        }
    }
}