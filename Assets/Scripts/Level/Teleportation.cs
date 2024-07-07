using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Teleportation : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;
    [SerializeField] private GameObject teleportNotification;
    [SerializeField] private float teleportationTime;

    private void Awake()
    {
        teleportNotification.SetActive(false);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        teleportNotification.SetActive(true);
        yield return new WaitForSeconds(teleportationTime);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
