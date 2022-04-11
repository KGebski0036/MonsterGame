using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    private ReaderJSON readerJSON = new ReaderJSON();
    private SkillsList allSkills;

    private void Start()
    {
        allSkills = readerJSON.ReadAllFromFile();
        UpdateAllSkills();
    }

    private void UpdateAllSkills()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.Find("TitleOfSkill").GetComponent<TextMeshProUGUI>().text = allSkills.listOfSkills[i].Name;

            byte[] imagineData = File.ReadAllBytes(Path.Combine(Application.dataPath + "/Icons/" + allSkills.listOfSkills[i].PathToIcon));
            Texture2D texture = new Texture2D(100, 100);
            texture.LoadImage(imagineData);

            transform.GetChild(i).transform.Find("IconOfSkill").GetComponent<RawImage>().texture = texture;

            string currentAndMaxLevelText = allSkills.listOfSkills[i].CurrentLevel + "/" + allSkills.listOfSkills[i].MaxLevel;
            transform.GetChild(i).transform.Find("LevelOfSkill").GetComponent<TextMeshProUGUI>().text = currentAndMaxLevelText;
        }
    }
}
