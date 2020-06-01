using UnityEngine;

[System.Serializable]
public class SpriteDefinition
{
    public string FullKey;
    public string URL;

    public static SpriteDefinition OfJson(string json)
    {
        return JsonUtility.FromJson<SpriteDefinition>(json);
    }
}

[System.Serializable]
public class ColorDefinition
{
    public string FullKey;
    public float R;
    public float G;
    public float B;

    public static ColorDefinition OfJson(string json)
    {
        return JsonUtility.FromJson<ColorDefinition>(json);
    }
}

[System.Serializable]
public class NumberDefinition
{
    public string FullKey;
    public float Value;

    public static NumberDefinition OfJson(string json)
    {
        return JsonUtility.FromJson<NumberDefinition>(json);
    }
}

[System.Serializable]
public class StringDefinition
{
    public string FullKey;
    public string Value;

    public static StringDefinition OfJson(string json)
    {
        return JsonUtility.FromJson<StringDefinition>(json);
    }
}

[System.Serializable]
public class BooleanDefinition
{
    public string FullKey;
    public bool Value;

    public static BooleanDefinition OfJson(string json)
    {
        return JsonUtility.FromJson<BooleanDefinition>(json);
    }
}

