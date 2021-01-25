public interface IHandlerItem<TID>
{
    TID ID { get; }
    void InitHandlerItem();
    void RemoveHandlerItem();
    void Enable();
    void Disable();
}
