using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Quests
{
/// <summary>
/// Extended Dialogue System class with quest marks tracking option.
/// </summary>
[AddComponentMenu("Quests/QuestStateListenerWithQuestMarks")]
public class QuestStateListenerWithQuestMarks : QuestStateListener
{
//     [System.Serializable]
//     public class TrackingQuestEvent : UnityEvent<string> {} // String - quest name.

//     [Header("Quest Tracking Events")]
//     [Tooltip("String - quest name")]
//    public TrackingQuestEvent onTrackingQuest;
//    [Tooltip("String - quest name")]
//    public TrackingQuestEvent onUnTrackingQuest;

    [System.Serializable]
   public class QuestStateTrackMark
   {
    [Tooltip("Quest entry state to listen for.")]
    public QuestState questState;

    [Tooltip("Conditions that must also be true.")]
    public Condition condition;
    [Tooltip("Quest mark to track")]
    public QuestMark questMark;
   }

   public QuestStateTrackMark[] questStateTrackMarks = new QuestStateTrackMark[0];


    [System.Serializable]
   public class QuestEntryStateTrackMark
   {
    [Tooltip("Quest entry number.")]
    public int entryNumber;

    [Tooltip("Quest entry state to listen for.")]
    public QuestState questState;

    [Tooltip("Conditions that must also be true.")]
    public Condition condition;
    [Tooltip("Quest mark to track")]
    public QuestMark questMark;
   }

    public QuestEntryStateTrackMark[] questEntryStateTrackMarks = new QuestEntryStateTrackMark[0];



   public void UpdateQuestTracking()
   {
    // Check quest state:
    var questState = QuestLog.GetQuestState(questName);
    for (int i = 0; i < questStateTrackMarks.Length; i++)
    {
        var questStateTrackMark = questStateTrackMarks[i];
        if (((questState & questStateTrackMark.questState) != 0) && questStateTrackMark.condition.IsTrue(null))
        {
            if (!m_suppressOnEnterStateEvent)
            {
                var tracking = QuestLog.IsQuestTrackingEnabled(questName);
                if (tracking) { questStateTrackMark.questMark.StartTracking(); }
                else { questStateTrackMark.questMark.StopTracking(); }
                
            }
        }
    }

    // Check quest entry states:
    for (int i = 0; i < questEntryStateTrackMarks.Length; i++)
    {
        var quesEntrytStateTrackMark = questEntryStateTrackMarks[i];
        var questEntryState = QuestLog.GetQuestEntryState(questName, quesEntrytStateTrackMark.entryNumber);
        if (((questEntryState & quesEntrytStateTrackMark.questState) != 0) && quesEntrytStateTrackMark.condition.IsTrue(null))
        {
            if (!m_suppressOnEnterStateEvent)
            {
                var tracking = QuestLog.IsQuestTrackingEnabled(questName);
                if (tracking) { quesEntrytStateTrackMark.questMark.StartTracking(); }
                else { quesEntrytStateTrackMark.questMark.StopTracking(); }
            }
        }
    }
    
   }


    public override void UpdateIndicator()
    {
        base.UpdateIndicator();

        UpdateQuestTracking();
    }

}

}
