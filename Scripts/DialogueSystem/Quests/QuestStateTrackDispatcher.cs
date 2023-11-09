
namespace Quests
{

/// <summary>
/// Extended dialogue system class with QuestTracking callback. This component SHOULD be on the DialogueManager gameobject
/// if you use QustStateListener with QuestMark tracking.
/// </summary>
public class QuestStateTrackDispatcher : PixelCrushers.DialogueSystem.QuestStateDispatcher
{
    public void OnQuestTrackingEnabled(string questName)
    {
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                var listener = listeners[i];
                if (listener == null) continue;
                if (string.Equals(questName, listener.questName) && listener is QuestStateListenerWithQuestMarks l) // Custom listener, not DS.
                {
                    l.UpdateQuestTracking();
                }
            }
        }
    }

    public void OnQuestTrackingDisabled(string questName)
    {
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                var listener = listeners[i];
                if (listener == null) continue;
                if (string.Equals(questName, listener.questName) && listener is QuestStateListenerWithQuestMarks l) // Custom listener, not DS.
                {
                    l.UpdateQuestTracking();
                }
            }
        }
    }


}

}
