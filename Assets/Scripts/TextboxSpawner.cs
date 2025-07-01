using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public void Spawn()
    {
        if (!susieIdea)
        {
            Transform canvas = transform.GetComponentInParent<Canvas>().transform;
            GameObject a = Instantiate(textbox, canvas);
            a.SetActive(true);
            a.GetComponent<UtTextboxScript>().dialogue = dialogue;
            a.GetComponent<UtTextboxScript>().specialEvent = special;
        }
        else
        {
            textbox.SetActive(true);
        }
    }
    public GameObject textbox;
    public Dialogue dialogue;
    public int special;

    public Dialogue[] dialogues;
    // 1 - met
    // 2 - ready?
    public bool susieIdea;
}
