using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CreatureSpriteAdaptions : MonoBehaviour
{
    public ShrinkAndGrowSprite ShrinkAndGrow;

    public CreatureSpriteAdpation[] SpriteAdaptions;

    Evolution[] current_adaptions = new Evolution[3];

    Evolution current_adaption;

    bool carnivore = false;

    public void Set_Evolution_Active(Evolution _evolution, int Choice, bool Carnivore = false)
    {
        Evolution evolution = _evolution;

        carnivore = Carnivore;

        if (Carnivore)
        {
            if (evolution == Evolution.Wings) evolution = Evolution.Hump;
            if (evolution == Evolution.Paws) evolution = Evolution.Hooves;
        }

        if (evolution == Evolution.Grow || evolution == Evolution.Shrink)
        {
            if (evolution == Evolution.Grow) ShrinkAndGrow.Set_Size(1);
            else if (evolution == Evolution.Shrink) ShrinkAndGrow.Set_Size(-1);
        }
        else
        {
            for (var i = 0; i < SpriteAdaptions.Length; i++)
            {
                Evolution[] _evolutions = SpriteAdaptions[i].Get_Evolution();

                if (_evolutions.Contains(evolution))
                {
                    SpriteAdaptions[i].Set_Part_Active(false, Carnivore, 0);
                    break;
                }
            }
        }

        current_adaptions[Choice] = evolution;
    }

    public void Evolve_All_Parts()
    {
        for (var i = 0; i < SpriteAdaptions.Length; i++)
        {
            SpriteAdaptions[i].Set_Part_Active(true, carnivore, 0);

            Evolution[] _evolutions = SpriteAdaptions[i].Get_Evolution();

            for (var j = 0; j < _evolutions.Length; j++)
            {
                if (current_adaptions.Contains(_evolutions[j]))
                {
                    SpriteAdaptions[i].Set_Part_Active(false, carnivore, 1);
                    break;
                }
            }
        }
    }

    public Evolution[] Get_Adapations()
    {
        return current_adaptions;
    }
}
