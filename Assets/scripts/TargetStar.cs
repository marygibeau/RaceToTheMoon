using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TargetStar : MonoBehaviour
{

    string target = "";
    System.Random random = new System.Random();
    int randomIndex;
    public List<string> starsToFind;
    public List<string> starsFound = new List<string>();
    TargetTextScript targetText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Target star script started");
        starsToFind = new List<string> {
            "Menkar",
            "Rigel",
            "Alpheratz",
            "Diphda",
            "Achernar",
            "Acamar",
            "Fomalhaut",
            "Aldebaran",
            "Canopus",
            "Sirius",
            "Capella",
            "Mirfak"
        };
        target = starsToFind[0];
        targetText = GameObject.Find("TargetText").GetComponent<TargetTextScript>();
        UpdateTargetText();
        // Debug.Log("Target star = " + target);
    }

    public string GetTarget()
    {
        return target;
    }

    public void UpdateTarget()
    {
        if (starsFound.Count == 0)
        {
            starsFound.Add(target);
            starsToFind.Remove(target);
            target = starsToFind[0];
        }
        else
        {
            starsFound.Add(target);
            starsToFind.Remove(target);
            // Debug.Log(starsToFind.Count);
            if (starsToFind.Count == 0) // no more stars to find
            {
                target = "done";
            }
            else
            {
                randomIndex = random.Next(starsToFind.Count);
                target = starsToFind[randomIndex];
            }
        }
        UpdateTargetText();
    }

    public void UpdateTargetText()
    {
        targetText.GenerateTargetText(target);
    }
    public int GetNumberOfStarsFound()
    {
        return starsFound.Count;
    }

    public List<string> GetNamesOfStarsFound()
    {
        return starsFound;
    }
}
