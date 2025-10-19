using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevinRoomManager : MonoBehaviour
{
    private bool on;
    public RawImage[] itemSlots;
    public TMP_Text health;

    public void Initialize()
    {
        SetPause(true);
        for (int i = 0; i < 4; i++)
        {
            if (GameControllerScript.Instance.item[i] != 0)
            {
                itemSlots[i].gameObject.SetActive(true);
                itemSlots[i].texture = GameControllerScript.Instance.itemSlot[i].texture;
            }
            else itemSlots[i].gameObject.SetActive(false);
        }
        health.text = PlayerScript.Instance.health.ToString() + "%";
    }

    public void Update()
    {
        if (on && Input.GetKeyDown(KeyCode.Escape)) Finish();
    }

    public void Finish()
    {
        SetPause(false);
        PlayerScript.Instance.transform.position += Vector3.left * 5;
    }

    private void SetPause(bool state)
    {
        GameControllerScript.Instance.disablePausing = state;
        if (state) GameControllerScript.Instance.PauseGame();
        else GameControllerScript.Instance.UnpauseGame();
        on = state;
        transform.parent.gameObject.SetActive(state);
    }
}
