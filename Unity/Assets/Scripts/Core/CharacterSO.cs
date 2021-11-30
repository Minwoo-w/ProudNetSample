using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character")]
public class CharacterSO : ScriptableObject
{
    public Actor[] characters; // 캐릭터 정보

    public Actor GetActorPrefab(string name)
    {
        Actor actor = null;
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].name == name)
            {
                actor = characters[i];
            }
        }
        return actor;
    }
}
