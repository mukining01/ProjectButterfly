using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSprite : MonoBehaviour
{
    List<GameObject> all_creature_images = new List<GameObject>();

    GameObject currently_active_creature;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Get_All_Creature_Images();
        Set_Creature_Sprite();
    }

    public void Get_All_Creature_Images()
    {
        for(var i = 0; i < transform.childCount; i++)
        {
            GameObject _sprite = transform.GetChild(i).gameObject;
            all_creature_images.Add(_sprite);
        }
    }

    List<int> stored_evolutions = new List<int> { 0, 0, 0};
    int current_evolution_stage;

    int store_evolution_type_for_evolution = 0;

    public void Set_Creature_Moving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void Set_Creature_Evolving(int evolution_type)
    {
        store_evolution_type_for_evolution = evolution_type;

        animator.SetTrigger("Evolve");

        
    }

    public void Set_Evolution()
    {
        stored_evolutions[current_evolution_stage] = store_evolution_type_for_evolution;
        current_evolution_stage++;

        Set_Creature_Sprite();
    }

    public void Set_Creature_Sprite()
    {
        if(currently_active_creature != null) currently_active_creature.SetActive(false);

        for (var i = 0; i < all_creature_images.Count; i++)
        {
            Creature_Sprites _sprite = all_creature_images[i].GetComponent<CreatureSpriteInformation>().creature_sprite_information;

            if ((int)_sprite.evolution_one != stored_evolutions[0]) continue;
            if ((int)_sprite.evolution_two != stored_evolutions[1]) continue;
            if ((int)_sprite.evolution_three != stored_evolutions[2]) continue;

            currently_active_creature = all_creature_images[i];
            currently_active_creature.SetActive(true);
            break;
        }

        for (var i = 0; i < all_creature_images.Count; i++)
        {
            if (currently_active_creature == all_creature_images[i]) continue;
            all_creature_images[i].SetActive(false);
        }

        //Debug.LogError("No evolution found");
    }

    public void Reset_CreatureSprite()
    {
        for(var i = 0; i < all_creature_images.Count;i++)
        {
            stored_evolutions[i] = 0;
        }

        current_evolution_stage = 0;

        Set_Creature_Sprite();
    }

    public void Post_Evolution_Animation()
    {
        EvolutionManager.Post_Creature_Evolution();
    }
}

[System.Serializable]
public class Creature_Sprites
{
    public Evolution1 evolution_one;
    public Evolution1 evolution_two;
    public Evolution1 evolution_three;
}

public enum Evolution1
{
    Null, Choice_1, Choice_2, Choice_3
}

public enum Evolution2
{
    Null, Choice_1, Choice_2, Choice_3
}

public enum Evolution3
{
    Null, Choice_1, Choice_2, Choice_3
}

