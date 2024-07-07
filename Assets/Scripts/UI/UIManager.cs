using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Start")]
    [SerializeField] private GameObject startScreen;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    //Scene saving parameters
    private int sceneToContinue;
    private int currentSceneIndex;

    private Transform currentPlayerPosition;
    private PlayerMovement playerMovement;
    private Health playerHealth;
    private PlayerRespawn playerRespawn;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }


    private void Update()
    {
        UnPause();
    }



    #region Starting Game

    public void StartGame()
    {
        //
        SceneManager.LoadScene(1);
        startScreen = null;
    }

    public void ContinueGame()
    {
        sceneToContinue = PlayerPrefs.GetInt("SavedGame");

        if (sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
            Time.timeScale = 1f;
        }
        else
            return;
    }

    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //if status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        //When pause is true change time scale to 0(stoping the game ) and if pause in false - game is on
        if (status) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }
    }

    private void UnPause()
    {
        //If pause screen already active unpause

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }

    }
    public void SoundValue()
    {
        SoundManager.instance.ChangeSoundVolume(0.25f);
    }
    public void MusicValue()
    {
        SoundManager.instance.ChangeMusicVolume(0.25f);
    }
    #endregion

    #region Game Over
    //Activate Game Over Screen
    public void GameOver()
    {
         gameOverScreen.SetActive(true);
         SoundManager.instance.PlaySound(gameOverSound);
         pauseScreen = null;    
    }
    //Game over functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueFromTheLastCheckpoint()
    {
        playerRespawn.RespawnMade();
    }

    public void MainMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedGame", currentSceneIndex);
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
       Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//executed only inside of an editor
#endif
    }
    #endregion

}
