using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResultItemModel
{
    public ResultItemModel(int value, int stars)
    {
        this.value = value;
        this.stars = stars;
    }
    public int value; // множитель
    public int stars; // колличество звезд
    //  isBlocket считать буду по звездам если их 0 значит он заблокирован
}

[Serializable]
public class PlayerModel
{
    public ResultItemModel[] multiplications; // умножение
    public ResultItemModel[] mixmultiplications; // умножение
    public ResultItemModel[] divisions; // деление
    public ResultItemModel[] mixdivisions; // деление

    
    public ResultItemModel GetByValue(int value)
    {
        if (GameManager.instance.TypeCurrentGame == TypeGame.Multiplication)
            return multiplications.FirstOrDefault(model => model.value == value);
        if (GameManager.instance.TypeCurrentGame == TypeGame.Division)
            return divisions.FirstOrDefault(model => model.value == value);
        if (GameManager.instance.TypeCurrentGame == TypeGame.MixMultiplication)
            return mixmultiplications.FirstOrDefault(model => model.value == value);
        if (GameManager.instance.TypeCurrentGame == TypeGame.MixDivision)
            return mixdivisions.FirstOrDefault(model => model.value == value);
        throw new UnityException("not found type");
    }

    public void createSubModels(int length)
    {
        multiplications = new ResultItemModel[length];
        fill(ref multiplications);

        divisions = new ResultItemModel[length];
        fill(ref divisions);
    }

    private void fill(ref ResultItemModel[] _array)
    {
        for (int i = 0; i < _array.Length; i++)
            _array[i] = new ResultItemModel(i+2, 0);
    }
}