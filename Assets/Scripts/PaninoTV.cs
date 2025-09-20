using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaninoTV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        exclamationSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        if (TestMode)
        {
            StartCoroutine(EventTime(TestValue));
            return;
        }
        if (((gc.mode == "story" || gc.mode == "pizza" || gc.mode == "triple" || gc.mode == "free") && Random.Range(1, 4) == 2) || gc.mode == "endless")
        {
            eventWillHappne = true;
            print("event happens");
            timmer = Random.Range(70, 280);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.spoopMode)
        {
            if (eventWillHappne)
            {
                timmer -= Time.deltaTime;
            }
            if (timmer <= 0 && eventWillHappne)
            {
                ItsTimeForARandomEventBrother();
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (TestMode)
            {
                StartCoroutine(EventTime(TestValue));
            }
        }
    }

    void ItsTimeForARandomEventBrother()
    {
        int eventee = Random.Range(3, paninoAnnounce.Length);
        if (gc.IsAprilFools() || Random.Range(1, 280) == 4)
        {
            eventee = 2;
        }
        StartCoroutine(EventTime(eventee));
        if (Random.Range(1, 4) != 3 && eventsDone[eventee] != 1)
        {
            timmer = Random.Range(40, 100);
            return;
        }
        eventsDone[eventee] = 1;
        eventWillHappne = false;
    }

    public IEnumerator EventTime(int thing)
    {
        if (!stillBlabbering)
        {
            anim.SetTrigger("neverGoDown");
        }
        if (thing != 0)
        {
            exclamationSound.Play();
            if (stillBlabbering)
            {
                queued = true;
                yield return new WaitUntil(() => !stillBlabbering);
            }
            exclamation.SetActive(true);
            yield return new WaitForSeconds(2.5f);
        }
        else
        {
            yield return new WaitForSeconds(1); 
            if (stillBlabbering)
            {
                queued = true;
                yield return new WaitUntil(() => !stillBlabbering);
            }
        }
        panino.GetComponent<AudioSource>().clip = paninoAnnounce[thing];
        queued = false;
        panino.SetActive(false);
        exclamation.SetActive(false);
        tvStatic.SetActive(true);
        stillBlabbering = true;
        yield return new WaitForSeconds(0.25f);
        tvStatic.SetActive(false);
        panino.SetActive(true);
        switch (thing)
        {
            case 0: FindObjectOfType<SubtitleManager>().AddChained2DSubtitle(blabber0, duration0, colore0); break;
            case 1: FindObjectOfType<SubtitleManager>().AddChained2DSubtitle(blabber1, duration1, colore1); break;
            case 2: FindObjectOfType<SubtitleManager>().Add2DSubtitle("How do I get him off", paninoAnnounce[2].length, Color.white); break;
            case 3: FindObjectOfType<SubtitleManager>().AddChained2DSubtitle(blabber2, duration2, colore0); break;
            case 4: FindObjectOfType<SubtitleManager>().Add2DSubtitle("I agree, let's release the angry bees", paninoAnnounce[4].length, Color.white); break;
        }
        switch (thing)
        {
            case 2: washeewashee.SetActive(true); break;
            case 3: prisonDoor.ItemsAreNowGoingToJail(); break;
        }
        yield return new WaitForSeconds(paninoAnnounce[thing].length + 0.5f);
        tvStatic.SetActive(true);
        panino.SetActive(false);
        stillBlabbering = false;
        yield return new WaitForSeconds(0.25f);
        tvStatic.SetActive(false);
        if (!queued)
        {
            anim.SetTrigger("alwaysGoUp");
        }
    }

    AudioSource exclamationSound;
    public GameObject exclamation;
    public GameObject panino;
    public GameObject tvStatic;
    Animator anim;

    public GameControllerScript gc;
    [SerializeField]
    float timmer;
    [SerializeField]
    bool eventWillHappne;

    public GameObject washeewashee;

    public AudioClip[] paninoAnnounce;

    [SerializeField]
    int TestValue;
    public bool TestMode;

    int[] eventsDone = new int[4];

    public PrisonDoor prisonDoor;

    bool stillBlabbering;
    bool queued;

    string[] blabber0 = { "Congrattation!", "You found all 7 Dwaynes,", "now all you need to do is...", "GET OUT." };
    float[] duration0 = { 1.8f, 3f, 2.8f, 1.935f };
    Color[] colore0 = { Color.white, Color.white, Color.white, Color.white };
    string[] blabber1 = { "The Pillar John door in the first cafeteria has just been unlocked,", "and we don't know why.", "You should totes stay out of there bro, please" };
    float[] duration1 = { 4.609f, 2.05f,  2.192f};
    Color[] colore1 = { Color.white, Color.white, Color.white };
    string[] blabber2 = { "I have decided that any items that you have are a big meanie", "and I will put them into the.", "Jail.", "Good luck breaking them out, assuming you have any." };
    float[] duration2 = { 3.61f, 2.64f, 1f, 2.5f };
}
