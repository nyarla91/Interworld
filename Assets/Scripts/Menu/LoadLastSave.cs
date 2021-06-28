using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLastSave : MonoBehaviour
{
    public void Load()
    {
        if (!CombatArea.combat)
            Saves.Load();
        else
            UIMessage.ShowMessage(Localization.Translate("<mesInCombat>"));
            
    }
}
