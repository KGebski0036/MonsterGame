using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ReaderJSON
{
    public SkillsList ReadAllFromFile()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/Resources/SkillsConfig.json");
        return JsonUtility.FromJson<SkillsList>(jsonData);
    }
    public void SaveNewSkill(SkillsList skill)
    {
        string jsonData = JsonUtility.ToJson(skill, true);
        File.WriteAllText(Application.dataPath + "/Resources/SkillsConfig.json", jsonData);
    }
}
