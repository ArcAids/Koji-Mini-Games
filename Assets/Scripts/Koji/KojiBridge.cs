using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

enum KojiValueType
{
    URL,
    Color,
    Number,
    String,
    Boolean
};

public class KojiBridge : MonoBehaviour
{
    public static KojiBridge Instance { get; private set; } 

    [DllImport("__Internal")]
    private static extern void Subscribe(string type, string key);

    // set from Javascript
    private static Dictionary<string, Observable<string>> urls = new Dictionary<string, Observable<string>>();
    private static Dictionary<string, Observable<Color>> colors = new Dictionary<string, Observable<Color>>();
    private static Dictionary<string, Observable<float>> numbers = new Dictionary<string, Observable<float>>();
    private static Dictionary<string, Observable<string>> strings = new Dictionary<string, Observable<string>>();
    private static Dictionary<string, Observable<bool>> booleans = new Dictionary<string, Observable<bool>>();

    // derived
    private static Dictionary<string, Observable<Texture2D>> textures = new Dictionary<string, Observable<Texture2D>>();
    private static Dictionary<string, Observable<Sprite>> sprites = new Dictionary<string, Observable<Sprite>>();
    private static Dictionary<string, Observable<Material>> materials = new Dictionary<string, Observable<Material>>();

#if UNITY_EDITOR
    string debugLine;
    [ContextMenu("Colors")]
    public void ListColorKeys()
    {
        debugLine = "Colors:";
        foreach (var key in colors.Keys)
        {
            debugLine += "\n"+key;
        }
            Debug.Log(debugLine);
    }
    [ContextMenu("Strings")]
    public void ListStringsKeys()
    {
        debugLine = "strings:";
        foreach (var key in strings.Keys)
        {
            debugLine += "\n" + key;
        }
        Debug.Log(debugLine);
    }
    [ContextMenu("Sprites")]
    public void ListSpritesKeys()
    {
        debugLine = "Sprites:";
        foreach (var key in sprites.Keys)
        {
            debugLine += "\n" + key;
        }
        Debug.Log(debugLine);
    }
    [ContextMenu("Textures")]
    public void ListTextruesKeys()
    {
        debugLine = "Textures:";
        foreach (var key in textures.Keys)
        {
            debugLine += "\n" + key;
        }
        Debug.Log(debugLine);
    }
#endif 

    public static Observable<string> ObservableURLOfKey(string key)
    {
        if (!urls.ContainsKey(key))
        {
            urls[key] = new Observable<string>();

            try
            {
                Subscribe(KojiValueType.URL.ToString(), key);
            } 
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        return urls[key];
    }

    public static Observable<Color> ObservableColorOfKey(string key)
    {
        if (!colors.ContainsKey(key))
        {
            colors[key] = new Observable<Color>();

            try
            {
                Subscribe(KojiValueType.Color.ToString(), key);
            } 
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        return colors[key];
    }

    public static Observable<float> ObservableNumberOfKey(string key)
    {
        if (!numbers.ContainsKey(key))
        {
            numbers[key] = new Observable<float>();

            try
            {
                Subscribe(KojiValueType.Number.ToString(), key);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        return numbers[key];
    }

    public static Observable<string> ObservableStringOfKey(string key)
    {
        if (!strings.ContainsKey(key))
        {
            strings[key] = new Observable<string>();

            try
            {
                Subscribe(KojiValueType.String.ToString(), key);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        return strings[key];
    }

    public static Observable<bool> ObservableBooleanOfKey(string key)
    {
        if (!booleans.ContainsKey(key))
        {
            booleans[key] = new Observable<bool>();

            try
            {
                Subscribe(KojiValueType.Boolean.ToString(), key);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }

        return booleans[key];
    }

    public static Observable<Texture2D> ObservableTextureOfKey(string key)
    {
        if (!textures.ContainsKey(key))
        {
            Observable<string> observableUrl = ObservableURLOfKey(key);
            textures[key] = new URLToTexture2D(observableUrl).Output;
        }

        return textures[key];
    }

    public static Observable<Sprite> ObservableSpriteOfKey(string key)
    {
        if (!sprites.ContainsKey(key))
        {
            Observable<Texture2D> observableTexture = ObservableTextureOfKey(key);
            sprites[key] = new Texture2DToSprite(observableTexture).Output;
        }

        return sprites[key];
    }

    public static Observable<Material> ObservableMaterialOfKey(string key)
    {
        if (!materials.ContainsKey(key))
        {
            Observable<Texture2D> observableTexture = ObservableTextureOfKey(key);
            materials[key] = new Texture2DToMaterial(observableTexture).Output;
        }

        return materials[key];
    }

    private void Start()
    {
        if (KojiBridge.Instance == null)
        {
            KojiBridge.Instance = this;
        }

        //ObservableURLOfKey("images.player").SetValue("https://images.koji-cdn.com/d4d2aab7-1847-481d-8aa6-6866d81d5e0f/xhoh4-player.png");
        //ObservableURLOfKey("images.ceiling").SetValue("https://images.koji-cdn.com/7b947fc3-5954-4d7f-aec5-9342b7a86700/iq4pu-14.png");
        //ObservableURLOfKey("images.cube").SetValue("https://images.koji-cdn.com/f703db95-b691-4454-8d2f-e1758d886bd5/userData/hx9td-r0yWP1.png");
        //ObservableColorOfKey("colors.sky").SetValue(new Color(0.5f, .5f, .65f));
        //ObservableNumberOfKey("general.health").SetValue(5);
    }

    public void ExternalURLDidChange(string json)
    {
        SpriteDefinition definition = SpriteDefinition.OfJson(json);

        ObservableURLOfKey(definition.FullKey).SetValue(definition.URL);
    }

    public void ExternalColorDidChange(string json)
    {
        ColorDefinition definition = ColorDefinition.OfJson(json);

        ObservableColorOfKey(definition.FullKey).SetValue(
            new Color(definition.R, definition.G, definition.B)
        );
    }

    public void ExternalNumberDidChange(string json)
    {
        NumberDefinition definition = NumberDefinition.OfJson(json);

        ObservableNumberOfKey(definition.FullKey).SetValue(definition.Value);
    }

    public void ExternalStringDidChange(string json)
    {
        StringDefinition definition = StringDefinition.OfJson(json);

        ObservableStringOfKey(definition.FullKey).SetValue(definition.Value);
    }

    public void ExternalBooleanDidChange(string json)
    {
        BooleanDefinition definition = BooleanDefinition.OfJson(json);

        ObservableBooleanOfKey(definition.FullKey).SetValue(definition.Value);
    }
}
