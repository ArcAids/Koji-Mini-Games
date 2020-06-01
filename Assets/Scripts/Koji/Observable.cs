public class Observable<T>
{
    public T Value { get; private set; }

    public readonly SimpleEvent<T> DidChange = new SimpleEvent<T>();
    public readonly SimpleEvent<bool> DidDestroy = new SimpleEvent<bool>();

    private bool isDestroyed = false;

    public void SetValue(T value)
    {
        this.Value = value;

        if (this.DidChange != null)
        {
            this.DidChange.Emit(value);
        }
    }

    public void Destroy()
    {
        if (isDestroyed)
        {
            return;
        }

        isDestroyed = true;

        if (DidDestroy != null)
        {
            DidDestroy.Emit(true);
        }
    }
}
