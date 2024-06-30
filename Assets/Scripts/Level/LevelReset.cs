using UnityEngine;

public class LevelReset : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector2[] initialPosition;

    private void Awake()
    {
        //Saving the initial position of the enemies
        initialPosition = new Vector2[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i]= enemies[i].transform.position;
        }
    }

    public void ActivateLevel(bool _status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }

    }   }
}
