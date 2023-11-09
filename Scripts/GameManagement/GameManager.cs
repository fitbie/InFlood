using System;
using UnityEngine;


/// <summary>
/// This class stores references for the main components of the game and some game logic
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton Initialization

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) { throw new NullReferenceException("GameManager doest't exist!"); }
            return instance;
        }
        private set => instance = value;
    }


    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }

    #endregion


    #region Singleton fields & properties

    [field: SerializeField] public Player Player {get; private set; }

    #endregion
}