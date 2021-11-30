using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    // Singleton
    private static GameMode instance;
    public static GameMode Instance { get { return instance; } }

    [SerializeField] private CharacterSO characterSO = null;
    private Actor selfActor = null;
    //private List<Actor> actors = new List<Actor>();
    private Dictionary<int, Actor> actors = new Dictionary<int, Actor>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

#if UNITY_STANDALONE
            Screen.SetResolution(800, 640, false, 30);
#elif UNITY_WEBGL
            Application.targetFrameRate = 60;
#endif
            
            //selfActor = CreateActor("Player");
            //selfActor.SetIsMine(true);
        }
        else
        {
            Destroy(instance);
        }
    }

    private Actor tempActor = null;
    public Actor CreateActor(int id, string name)
    {
        tempActor = characterSO.GetActorPrefab(name);
        if (tempActor != null)
        {
            // Instansing
            tempActor = Instantiate(tempActor);
            tempActor.id = id;
            actors.Add(tempActor.id, tempActor);
        }
        return tempActor;
    }

    public void DestroyActor(int id)
    {
        Actor actor = null;
        if (id >= 0 && actors.ContainsKey(id))
        {
            actor = actors[id];
            actors.Remove(id);
            if (actor != null)
            {
                Destroy(actor.gameObject);
            }
        }
    }

    public Actor GetActorByID(int id)
    {
        tempActor = null;
        if (id >= 0 && actors.ContainsKey(id))
        {
            tempActor = actors[id];
        }
        return tempActor;
    }

    public int GetActorCount()
    {
        return actors.Count;
    }
}
