using System.Threading.Tasks;

public interface IAttachable
{
    public string KindOfObject { get; }
    public bool IsAttachable { get; }
    public DragAndDrop dragAndDrop { get; }
    public Task OnAnimStart();
    public void OnAnimEnd();
}
