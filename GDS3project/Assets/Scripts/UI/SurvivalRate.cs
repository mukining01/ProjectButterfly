using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SurvivalRate : MonoBehaviour
{
    public GameObject CreatureSprite_Object;

    public List<CreatureSprite> creature_sprites = new List<CreatureSprite>();

    public int darken_count = 0;

    [ContextMenu("CreatureShow")]
    public void CreateSurvivalObjects()
    {
        StartCoroutine(SurvialObjects());
    }

    IEnumerator SurvialObjects()
    {
        for(var i = 0; i < 10; i++)
        {
            yield return new WaitForEndOfFrame();

            int _pos = 5 - i;
            Vector2 _new_pos = new Vector2(5 * _pos, 0);
            GameObject _new_creatue_sprite = Instantiate(CreatureSprite_Object, Vector3.zero, Quaternion.identity, transform);

            _new_creatue_sprite.transform.position = _new_pos;
            _new_creatue_sprite.transform.localScale = Vector3.one;

            creature_sprites.Add(_new_creatue_sprite.transform.GetChild(0).GetComponent<CreatureSprite>());
            Destroy(_new_creatue_sprite.transform.GetChild(0).gameObject.GetComponent<Animator>());
        }

        Darken_Images(darken_count);
    }

    public void Darken_Images(int _darken_count)
    {
        for(var i = 0; i < _darken_count; i++)
        {
            CreatureSprite _sprite = creature_sprites[i];

            for(var j = 0; j < _sprite.all_creature_sprites.Length; j++)
            {
                _sprite.all_creature_sprites[j].color = new Color32(0, 0, 0, 255);
            }
        }
    }
}
