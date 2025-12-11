using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPersistentData : MonoBehaviour
{
    public static PlayerPersistentData Instance;

    [Header("Persistent Aether & Abilities")]
    public int  aetherEnergy = 0;
    public bool hasPulseStep   = false;
    public bool hasIgnisCore   = false;
    public bool hasTitanStrike = false;
    public bool hasAetherBurst = false;

    void Awake()
    {
        // Simple singleton + keep this object when scenes change
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Call this ONLY when starting a completely new game from the main menu
    public void ResetForNewGame()
    {
        aetherEnergy   = 0;
        hasPulseStep   = false;
        hasIgnisCore   = false;
        hasTitanStrike = false;
        hasAetherBurst = false;
    }
}
