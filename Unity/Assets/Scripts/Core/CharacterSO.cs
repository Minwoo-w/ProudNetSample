using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character")]
public class CharacterSO : ScriptableObject
{
    public Actor[] characters; // ĳ���� ����

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
