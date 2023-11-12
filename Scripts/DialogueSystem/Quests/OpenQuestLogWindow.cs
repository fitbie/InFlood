using PixelCrushers.DialogueSystem;
using UnityEngine;

public class OpenQuestLogWindow : MonoBehaviour
{
    public void OpenLogWindow() // Called from QuestTrackerHUD button.
    {
        FindObjectOfType<QuestLogWindow>().Open();
    }
}
