using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlatformerMVC
{
    public class QuestConfiguratorController : MonoBehaviour
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuest;

        public QuestConfiguratorController(QuestObjectView questView)
        {
            _singleQuestView = questView;
        }

        public void Init()
        {
            _singleQuest = new QuestController(_singleQuestView, new QuestModel());
            _singleQuest.Reset();
        }
    }
}