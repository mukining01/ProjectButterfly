using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CreatureSpriteAdaptions : MonoBehaviour
{
    public ShrinkAndGrowSprite ShrinkAndGrow;
    public CreatureSpriteAdpation[] SpriteAdaptions;

    List<Evolution> current_adaptions = new List<Evolution>();

    public void Set_Evolution_Active(Evolution _evolution, bool Clear, bool Carnivore, int Part)
    {
        Evolution evolution = _evolution;

        if(!Carnivore)
        {
            if(evolution == Evolution.Wings) evolution = Evolution.Hump;
            if (evolution == Evolution.Paws) evolution = Evolution.Hooves;
        }

        if(evolution == Evolution.Grow || evolution == Evolution.Shrink)
        {
            if (Clear) ShrinkAndGrow.Set_Size(0);
            else if (evolution == Evolution.Grow) ShrinkAndGrow.Set_Size(1);
            else if (evolution == Evolution.Shrink) ShrinkAndGrow.Set_Size(-1);
        } else
        {
            for (var i = 0; i < SpriteAdaptions.Length; i++)
            {
                Evolution[] _evolutions = SpriteAdaptions[i].Get_Evolution();

                if (_evolutions.Contains(evolution)) SpriteAdaptions[i].Set_Part_Active(false, Carnivore, Part);
            }
        }

        if (!Clear) current_adaptions.Add(evolution);
        else current_adaptions.Remove(evolution);
    }
}
