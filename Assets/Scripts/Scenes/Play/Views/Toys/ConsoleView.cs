using Scene;
using System.Threading.Tasks;
using Sound;
using UnityEngine;

public class ConsoleView : BaseToyView, IAttachable
{
    [SerializeField] private SoundSystem _soundSystem;
    [SerializeField] private string _sound;
    public  SceneLoader _sceneLoader { get; set; }

    public string KindOfObject => KindOfToy.ToString();
    public bool IsAttachable => false;
    public DragAndDrop dragAndDrop => DragAndDrop;

    private new void Start()
    {
        base.Start();
        _soundSystem.StopSound(_sound);
        dragAndDrop.OnDragStart += () => _soundSystem.PlaySound(_sound);
        Child.OnTriggerEnter += GoToMiniGames;
    }

    private void GoToMiniGames(IAttachable obj)
    {
        if(obj.KindOfObject == Toys.Console.ToString())
            _sceneLoader.LoadScene(Quest.SceneType.MiniGames);
    }

    public override Task OnAnimStart() => throw new System.NotImplementedException();
    public override void OnAnimEnd() => throw new System.NotImplementedException();
}
