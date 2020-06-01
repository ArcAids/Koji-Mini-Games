using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public interface TransformerOutput<T>
{
    Observable<T> Output { get; }
}

public abstract class Transformer<TI, TV> : TransformerOutput<TV>
{
    public Observable<TV> Output { get; private set; }
    protected Observable<TI> input;

    private List<Handle> handles = new List<Handle>();

    public Transformer(Observable<TI> input)
    {
        Output = new Observable<TV>();

        this.input = input;

        if (this.input != null)
        {
            handles.Add(this.input.DidChange.Subscribe(Input_DidChange, true));
            handles.Add(this.input.DidDestroy.Subscribe(Input_DidDestroy));
        }
    }

    private void Input_DidDestroy(bool ignored)
    {
        Destroy();
    }

    private void Input_DidChange(TI value)
    {
        Process();
    }

    public void Destroy()
    {
        handles.ForEach(handle =>
        {
            handle.Release();
        });
        handles.Clear();

        Output.Destroy();
    }

    protected abstract void Process();
}

public abstract class AsyncTransformer<TI, TV> : Transformer<TI, TV>
{
    public AsyncTransformer(Observable<TI> input) : base(input) { }

    protected override void Process()
    {
        KojiBridge.Instance.StartCoroutine(ProcessAsync());
    }

    protected abstract IEnumerator ProcessAsync();
}

public class URLToTexture2D : AsyncTransformer<string, Texture2D>
{
    public URLToTexture2D(Observable<string> observableUrl) : base(observableUrl) { }

    protected override IEnumerator ProcessAsync()
    {
        string url = this.input.Value;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            this.Output.SetValue(texture);
        }
    }
}

public class Texture2DToSprite : Transformer<Texture2D, Sprite>
{
    public Texture2DToSprite(Observable<Texture2D> observableTexture2D) : base(observableTexture2D) { }

    protected override void Process()
    {
        Texture2D texture = this.input.Value;

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100, 1, SpriteMeshType.FullRect);
        Output.SetValue(sprite);
    }
}

public class Texture2DToMaterial : Transformer<Texture2D, Material>
{
    public Texture2DToMaterial(Observable<Texture2D> observableTexture2D) : base(observableTexture2D) { }

    protected override void Process()
    {
        Texture2D texture = this.input.Value;

        Shader shader = Shader.Find("Unlit/Texture");
        if (shader == null)
        {
            throw new Exception("Could not find shader 'Unlit/Texture'");
        }

        Material material = new Material(shader);
        material.SetTexture("_MainTex", texture);

        Output.SetValue(material);
    }
}
