using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{


    [SerializeField] private Transform playerTransform;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("playerStarted"))
        {
            playerTransform.gameObject.SetActive(false);
            playerTransform.position = new Vector3(PlayerPrefs.GetFloat("playerPositionX"), PlayerPrefs.GetFloat("playerPositionY"), PlayerPrefs.GetFloat("playerPositionZ"));
            playerTransform.gameObject.SetActive(true);
        }
        if (!PlayerPrefs.HasKey("playerStarted"))
        {
            PlayerPrefs.SetInt("playerStarted", 1);
            PlayerPrefs.Save();
        }

    }

    // Update is called once per frame
    void Update()
    {
        SavedPosition();
    }

    private void SavedPosition()
    {
        playerPosition = playerTransform.position;
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("playerPositionZ", playerPosition.z);
        PlayerPrefs.Save();
        Debug.Log("X:" + PlayerPrefs.GetFloat("playerPositionX") + "Y:" + PlayerPrefs.GetFloat("playerPositionY") + "Z:" + PlayerPrefs.GetFloat("playerPositionZ"));
    }
}
