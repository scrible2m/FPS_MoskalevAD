public interface ISaveStuff
{
    void Save(DroppedStuff[] _stuff, int count);
    DroppedStuff[] Load();
}
