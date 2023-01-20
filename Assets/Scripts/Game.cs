using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Question
{
    public bool IsTrue = false; // верный или нет ответ
    public int value;// какое значение было 
    public string question; // вопрос
}
public enum GameState
{
    Result=0    
}

public class Game : WindowsBase
{
    
    [Header("Панель с вопросами")]
    [SerializeField] private TextMeshProUGUI panelQuestion;

    [Header("Кноупки для ответов")]
    [SerializeField] private Answer[] _answers;

    [Header("количество не верных ответов")]
    [SerializeField] private int counterNotTrueAnswer = 0;
    private int preValue = -1; // значение на которое отвечали
    private int trueAnswer = -1; // правильное значение на вопрос
    private int SelectedValue;
    private Question currentQuestion;

    protected override void onShow()
    {
        counterNotTrueAnswer = 0;
        GameManager.instance.NewGame();
        SelectedValue = GameManager.instance.SelectedValue;
        setNewNumberTile(SelectedValue);
    }

    public void SetState(int value)
    {
        if (trueAnswer == value)
        {
            currentQuestion.IsTrue = true;
        }
        else
        {
            counterNotTrueAnswer++;
        }

        currentQuestion.value = value;
        GameManager.instance.questions.Add(currentQuestion);
        if (GameManager.instance.questions.Count == GameManager.instance.CountForGame)
        {
            // конец игре нужно обсчитать
            GameManager.instance.UpdateStats(SelectedValue, counterNotTrueAnswer);
            GameManager.instance.SetNextState();
        }
        else
            setNewNumberTile(SelectedValue);    
        
    }

    private void setNewNumberTile(int mainValue)
    {
        currentQuestion = new Question();
        // тут ответы которые мы покажем
        List<int> answers_int = new List<int>();
        int value = 0;

        if (GameManager.instance.TypeCurrentGame == TypeGame.Multiplication)
        {
            while (true)
            {
                value = Random.Range(2, 9);
                if (preValue > 0 && value == preValue)
                    continue;
                break;
            }
            trueAnswer = value * mainValue;
            currentQuestion.question = mainValue + " * " + value;
        }
        if (GameManager.instance.TypeCurrentGame == TypeGame.Division)
        {
            while (true)
            {
                value = Random.Range(2, 9);
                if (preValue > 0 && value == preValue)
                    continue;
                break;
            }
            // 12 = 6 / 2
            int multi = value * mainValue;
            trueAnswer = value;
            currentQuestion.question = multi + " / " + mainValue;
        }
        preValue = value;

        panelQuestion.text = currentQuestion.question;

        for (int i = 0; i < _answers.Length; i++)
        {
            int v1 = Random.Range(2, 9);
            int v = Random.Range(2, 9);//mainValue * v1;
            if (!isDuplicate(answers_int, v))
                answers_int.Add(v);
            else
                i--;
        }
        // проверяем если нет верного ответа, то добавляем
        if (!isTrueAnserPresents(answers_int))
        {
            int index = Random.Range(0, answers_int.Count);
            answers_int[index] = trueAnswer;
        }
        // Назначаем кнопки
        for (int i = 0; i < _answers.Length; i++)
        {
            _answers[i].Number = answers_int[i];
        }

        Debug.Log("Начало верный ответ: " + trueAnswer);
    }

    private bool isTrueAnserPresents(List<int> answers)
    {
        foreach (int answer in answers)
        {
            if (answer == trueAnswer)
                return true;
        }
        return false;
    }
    private bool isDuplicate(List<int> answers_int, int value)
    {
        // проверяем на дубликат
        foreach (var value_int in answers_int)
        {
            if (value_int == value)
                return true;
        }
        return false;
    }
}
