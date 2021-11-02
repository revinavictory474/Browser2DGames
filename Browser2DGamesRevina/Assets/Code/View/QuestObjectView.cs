using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        [SerializeField] private Color _completeColor;
        private Color _defaultColor;
        public int Id => _id;
        [SerializeField] private int _id;

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
        }

        public void ProcessComplete()
        {
            _spriteRenderer.color = _completeColor;
        }

        public void ProcessReset()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }       
}