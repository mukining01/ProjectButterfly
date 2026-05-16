using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CreatureSpriteAdaptions : MonoBehaviour
{
    public ShrinkAndGrowSprite ShrinkAndGrow;

    public CreatureSpriteAdpation[] SpriteAdaptions;

    List<Evolution> current_adaptions = new List<Evolution>();

    Evolution current_adaption;

    public void Set_Evolution_Active(Evolution _evolution, bool Clear, bool Carnivore = false, int Part = 0)
    {
        Evolution evolution = _evolution;

        if (evolution == Evolution.Null)
        {
            if (Clear) Evolution_Empty();
            return;
        }

        if (!Carnivore)
        {
            if (evolution == Evolution.Wings) evolution = Evolution.Hump;
            if (evolution == Evolution.Paws) evolution = Evolution.Hooves;
        }

        if (evolution == Evolution.Grow || evolution == Evolution.Shrink)
        {
            if (Clear) ShrinkAndGrow.Set_Size(0);
            else if (evolution == Evolution.Grow) ShrinkAndGrow.Set_Size(1);
            else if (evolution == Evolution.Shrink) ShrinkAndGrow.Set_Size(-1);
        }
        else
        {
            for (var i = 0; i < SpriteAdaptions.Length; i++)
            {
                Evolution[] _evolutions = SpriteAdaptions[i].Get_Evolution();

                if (_evolutions.Contains(evolution))
                {
                    SpriteAdaptions[i].Set_Part_Active(false, Carnivore, Part);
                    break;
                }
            }

            if (!Clear)
            {
                if (current_adaption != Evolution.Null) Evolution_Empty();

                current_adaptions.Add(evolution);
                current_adaption = evolution;

            }
            else Evolution_Empty();
        }

        void Evolution_Empty()
        {
            if (current_adaption == Evolution.Null) return;

            current_adaptions.Remove(current_adaption);
            current_adaption = Evolution.Null;
        }
    }
}
