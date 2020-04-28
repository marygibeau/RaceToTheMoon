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
        Debug.Log("Target star = " + target);
    }

    public string GetTarget()
    {
        return target;
    }

    public void UpdateTarget()
    {
        if (starsToFind.Count == 0)
        {
            target = "done";
        }
        else
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
                randomIndex = random.Next(starsToFind.Count);
                target = starsToFind[randomIndex];
            }
        }
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
