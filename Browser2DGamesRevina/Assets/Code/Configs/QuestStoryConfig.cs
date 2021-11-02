using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs/ Quest Story Cfg", order = 1)]
public class QuestStoryConfig : ScriptableObject
{
    public QuestConfig[] quests;
    public QuestStoryType questStoryType;
}

public enum QuestStoryType
{
    Common,
    Resettable
}
