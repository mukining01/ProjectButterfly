using UnityEngine;

public class CreatureSpriteAdpation : MonoBehaviour
{
    [Header("Evolution")]
    public Evolution[] evolutions;

    [Header("Sprite Parts")]
    public GameObject Herbivore_Part_1;
    public GameObject Herbivore_Part_2;
    public GameObject Carnivore_Part_1;
    public GameObject Carnivore_Part_2;

    public Evolution[] Get_Evolution()
    {
        return evolutions;
    }

    public void Set_Part_Active(bool none = false, bool Carnivore = false, int Part = 0)
    {
        if(none)
        {
            Herbivore_Part_1.SetActive(false);
            Herbivore_Part_2.SetActive(false);
            Carnivore_Part_1.SetActive(false);
            Carnivore_Part_2.SetActive(false);
            return;
        }

        if(Carnivore)
        {
            if (Part == 0) Carnivore_Part_1.SetActive(true);
            else Carnivore_Part_2.SetActive(true);
        } else
        {
            if (Part == 0) Herbivore_Part_1.SetActive(true);
            else Herbivore_Part_2.SetActive(true);
        }
    }
}
