using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public static int language = 1;
    private static Dictionary<string, string[]> fields;

    private void Awake()
    {
        fields = new Dictionary<string, string[]>();
        
        // Map
        
        fields.Add("<mapUnlocked>", new [] {
            "Unlocked",
            "Открыто" });
        
        fields.Add("<mapLock>", new [] {
            "Lock",
            "Замок" });
        
        fields.Add("<mapCleared>", new [] {
            "(Cleared)",
            "(Пройдено)" });
        
        // Rings
        
        fields.Add("<rinEffect>", new [] {
            "Effect: ",
            "Эффект: " });
        
        // Messages

        fields.Add("<mesInCombat>", new [] {
            "You can't do that in combat",
            "Вы не можете сделать это в бою" });

        fields.Add("<mesLocked>", new [] {
            "Locked",
            "Закрыто" });

        fields.Add("<>", new [] {
            "",
            "" });
    }

    public static string Translate(string key)
    {
        if (fields.ContainsKey(key))
            return fields[key][language];
        return key;
    }

}
