using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject Display_1;
    [SerializeField] private GameObject Display_2;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject SettingsPanel;
    [Header("TXT Elements")]
    [SerializeField] private TextMeshProUGUI txtLevel;
    [Header("Link to AudioManager")]
    [SerializeField] private AudioManager audioManager;
    [Header("Link to script CellsTable")]
    [SerializeField] private CellsTable cellsTable;
    [Header("Link to script SlotsActivatior")]
    [SerializeField] private SlotsActivator slotsActivator;

    private int _levelGame;
    public int LevelGame => _levelGame;
    private float _timeWait = 0.8f;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        if (LoadSave.CheckFileAndLoad())
        {
            _levelGame = LoadSave.jsonData.Level;
            audioManager.VolumeSider.value = LoadSave.jsonData.Volume;
            cellsTable.IndexPositionsInTable = LoadSave.jsonData.IndexPosition;
        }
        else
            _levelGame = 1;
        
        Display_1.SetActive(true);
        Display_2.SetActive(false);

        txtLevel.text += $"{_levelGame}";
    }

    public void PlayGame()
    {
        Display_1.SetActive(false);
        Display_2.SetActive(true);
    }

    public void SettingsPopUp()
    {
        if (SettingsPanel.activeSelf)
            SettingsPanel.SetActive(false);
        else
            SettingsPanel.SetActive(true);
    }

    public void ReplayGame()
    {
        if (WinPanel.activeSelf)
        {
            _levelGame++;
            txtLevel.text = $"Level: {_levelGame}";

            WinPanel.SetActive(false);
            cellsTable.Sorting();
        }

        if (LosePanel.activeSelf)
        {
            LosePanel.SetActive(false);
            cellsTable.LoadPositionsCubs();
        }

        cellsTable.ResetPositonPlayer();
        slotsActivator.Active();
    }

    public void ResultGame(bool win)
    {
        StartCoroutine(Result(win));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator Result(bool win)
    {
        yield return new WaitForSeconds(_timeWait);

        if (win)
        {
            WinPanel.SetActive(true);
            audioManager.PlaySound(AudioManager.SoundsGame.Win);
        }
        else
        {
            LosePanel.SetActive(true);
            audioManager.PlaySound(AudioManager.SoundsGame.Lose);
        }

        yield break;
    }
    private void OnDisable()
    {
        LoadSave.SaveGame(_levelGame, audioManager.VolumeSider.value, cellsTable.IndexPositionsInTable);
    }
}
