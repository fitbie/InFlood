using UnityEngine;

namespace Quests
{

    /// <summary>
    /// QuestMark.cs using this class as tracking target. This class should be on gameobject we want to track and added to
    /// QuestMark.cs List<Target> Targets 
    /// </summary>
    public class QuestTarget : MonoBehaviour
    {
        private QuestMark questMark; // Quest Mark this target related to.


        public void Initialize(QuestMark mark) // Inject from QuestMark
        {
            questMark = mark;
        }


        public void RemoveTargetFromMark() // Remove this target from its QuestMark. E.g. object destroying, etc..
        {
            if (questMark != null) { questMark.RemoveTarget(this); }
        }
    }

}
