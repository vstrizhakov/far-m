public interface IWorkplace
{
    int WorkersCapacity { get; }

    bool CanCreateWorker(Context context);
    void AddWorker(Context context);
}
