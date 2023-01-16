using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum WindowsName
{
    SELECT=0,
    SELECT_LEVEL,
    GAME,
    SHOW_RESULT,
    NONE=-1
}

[System.Serializable]
public class Panel
{
    public WindowsName name;
    public WindowsBase panel;

    public bool Equals(WindowsName name)
    {
        return this.name == name;
    }
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    [Header("Основная игра")]
    [SerializeField]
    private Game mainGame;

    [Header("Все панели")]
    [SerializeField] private Panel[] _panels;

    [Header("Игровые состояния")]
    [SerializeField] private Panel[] _statePanels;

    [SerializeField] private WindowsName CurrentState;

    // предыдушее состояние
    [SerializeField] private WindowsName PreviousState;

    [Header("звездочки голд/серая")]
    [SerializeField] public Sprite[] starsImage;

    public List<Question> questions;
    public static GameManager instance = null;
    public PlayerModel PlayerModel => _playerModel;
    
    // число на которое будет умножение -1 это рандом
    public int MultiplicationValue = -1;
    // счетчик для игры
    public int CountForGame = 8;
    void Awake () {
        if (instance == null) {
            instance = this;
        } else if(instance == this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }

    private void InitializeManager()
    {
        if (!PlayerPrefs.HasKey("stats"))
        {
            CreateNewStats();
        }
        else
        {
            LoadStats();
        }
        ShowCurrentPanel();
    }

    public void CreateNewStats()
    {
        _playerModel = new PlayerModel();
        _playerModel.multiplications = new MultiplicationItemModel[8];
        for (int i = 0; i < _playerModel.multiplications.Length; i++)
            _playerModel.multiplications[i] = new MultiplicationItemModel(i+2, 0);
        SaveCurrentStats();
        
    }
    public void SaveCurrentStats()
    {
        PlayerPrefs.SetString("stats", JsonUtility.ToJson(_playerModel));
        Debug.Log("save current stats");
    }

    public void LoadStats()
    {
        _playerModel = JsonUtility.FromJson<PlayerModel>(PlayerPrefs.GetString("stats"));
    }
    public Game MainGame => mainGame;

    public void NewGame()
    {
        questions = new List<Question>();
    }

    public void UpdateStats(int multiplication, int notTrueAnswer)
    {
        int stars = 0;
        switch (notTrueAnswer)
        {
            case 0:
                stars = 3;
                break;
            case 1:
            case 2:
                stars = 2;
                break;
            case 3:
            case 4:
                stars = 1;
                break;
        }
        MultiplicationItemModel model = PlayerModel.GetByValue(multiplication);
        Debug.Log("stars: " + stars);
        Debug.Log("notTrueAnswer: " + notTrueAnswer);

        if (stars > model.stars)
        {
            model.stars = stars;
            SaveCurrentStats();
        }    
    }
    public void SetState(WindowsName name)
    {
        PreviousState = CurrentState;
        CurrentState = name;
        EnterToNextState();
    }
    public void SetNextState()
    {
        if (CurrentState == WindowsName.SHOW_RESULT)
            SetState(WindowsName.SELECT_LEVEL);
        else
            SetState(CurrentState + 1);
        EnterToNextState();
    }
    // закрываем старую панель и открываем новую
    private void EnterToNextState()
    {
        HidePreviousPanel();
        ShowCurrentPanel();
    }
    
    private void HidePreviousPanel()
    {
        if (PreviousState != WindowsName.NONE)
            getPanelByName(PreviousState).panel.HidePanel();
    }
    private void ShowCurrentPanel()
    {
        getPanelByName(CurrentState).panel.ShowPanel();
    }
    private Panel getPanelByName(WindowsName name)
    {
        foreach (Panel panel in _panels)
        {
            if (panel.Equals(name))
                return panel;
        }

        return null;
    }
}
