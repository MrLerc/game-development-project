using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playMenu;

    [SerializeField] private TMP_Dropdown difficultySelection;
    private int difficulty = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
        playMenu.SetActive(false);

        if (difficultySelection != null)
        {
            difficulty = difficultySelection.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Main Menu Buttons
    public void OpenPlayMenu()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void Options()
    {
        //add Later if able
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Play Menu Buttons

    public void StartGame()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        SceneManager.LoadScene("TestScene");
    }

    public void DropdownSelect()
    {
        if(difficultySelection != null)
        {
            difficulty = difficultySelection.value;
            Debug.Log("Difficulty: " + difficultySelection.options[difficulty].text);
        }
    }

    public void LevelSelect()
    {
        //Add later
    }

    public void BackToMain()
    {
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
    }
}
