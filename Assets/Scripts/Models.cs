using System;
using System.Linq;

[Serializable]
public class MultiplicationItemModel
{
    public MultiplicationItemModel(int value, int stars)
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
    public MultiplicationItemModel[] multiplications;

    public MultiplicationItemModel GetByValue(int value)
    {
        return multiplications.FirstOrDefault(model => model.value == value);
    }
    
}