namespace Dungeons.Core

{
    public interface IStatusQueue
    {
        void Add(string message);
        void Show();
    }
}