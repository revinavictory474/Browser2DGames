using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs/ Quest Cfg", order = 1)]
public class QuestConfig : ScriptableObject
{
    public int id;
    public QuestType questType;
}

public enum QuestType
{
    Coin,
    Switch,
    Button
}
