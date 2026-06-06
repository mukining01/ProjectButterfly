using UnityEngine;

public class FinalExtinctionText : MonoBehaviour
{
    public ExtinctionText[] EvolutionText;

    public ExtinctionText[] EvolutionText_Drought_Herb;

    public ExtinctionText[] EvolutionText_IceAge_Carnivore;

    public ExtinctionText[] EvolutionText_IceAge_Herbivore;
}

[System.Serializable]
public class ExtinctionText
{
    public Evolution[] Evolutions = new Evolution[] { new Evolution(), new Evolution(), new Evolution(), new Evolution(), new Evolution() };
    [TextArea(2, 2)]
    public string GoodText;
    [TextArea(2, 2)]
    public string BadText;
    [Range(0, 10)]
    public float percentage;
}

