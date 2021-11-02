using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace PlatformerMVC
{
    public class QuestConfiguratorController : MonoBehaviour
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuest;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjectViews;

        private List<IQuestStory> _questStories;

        private Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>(1);
        private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);

        public QuestConfiguratorController(QuestView questView)
        {
            _singleQuestView = questView._singleQuestView;

            _questStoryConfigs = questView._questStoryConfigs;

            _questObjectViews = questView._questObjectViews;
        }

        public void Init()
        {
            _singleQuest = new QuestController(_singleQuestView, new QuestModel());
            _singleQuest.Reset();

            _questStoryFactories.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));
            //_questStoryFactories.Add(QuestStoryType.Resettable, questCollection => new )

            _questFactories.Add(QuestType.Coin, () => new QuestModel());
            _questStories = new List<IQuestStory>();

            foreach (QuestStoryConfig questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig questConfig in config.quests)
            {
                IQuest quest = CreateQuest(questConfig);

                if (quest == null) continue;
                quests.Add(quest);

                Debug.Log("Quest add");
            }

            return _questStoryFactories[config.questStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            int questId = config.id;
            QuestObjectView questView = _questObjectViews.FirstOrDefault(value => value.Id == config.id);

            if(questView == null)
            {
                Debug.Log("Cant find View");
                return null;
            }

            if(_questFactories.TryGetValue(config.questType, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(questView, questModel);
            }
            Debug.Log("Cant create a model");
            return null;
        }
    }
}