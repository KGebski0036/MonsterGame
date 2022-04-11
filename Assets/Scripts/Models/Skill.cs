using System.Collections.Generic;

[System.Serializable]
public class Skill
{
    public int Id;
    public string Name;
    public string PathToIcon;
    public int MaxLevel;
    public int CurrentLevel;
    public List<int> ParentsId;
}

