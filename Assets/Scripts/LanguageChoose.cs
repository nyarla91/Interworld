using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageChoose : MonoBehaviour
{
    public void Choose(int language)
    {
        Saves.Config.language = language;
        Saves.Config.loaded = true;
        Saves.SaveConfig();
        SceneManager.LoadScene("Location");
    }
    
}
