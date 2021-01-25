using System.Collections.Generic;
public interface IHandler<TID, TItem> where TItem : IHandlerItem<TID>
{
    Dictionary<TID, TItem> Handler { get; }
    void Add(TItem _item);
    void Remove(TItem _item);
    TItem Get(TID _id);
    bool Exist(TID _id);
    void Enable(TID _id);
    void Disable(TID _id);
}
