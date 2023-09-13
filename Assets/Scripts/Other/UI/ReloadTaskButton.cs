using Finish;
using UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReloadTaskButton : HudElement
{
    private const float RELOAD_BUTTON_INTERNAL_TIME = 0f;

    [SerializeField] private FinishActor _finishActor;
    [SerializeField] private ShopperController _shopperController;

    private Button _reloadButton;


    // appear reload button
    private new void Start() 
    {
        base.Start();
        Appear(); 
        _reloadButton = GetComponent<Button>();
        _shopperController.OnShopperArrived += SetInteractable;
    }
    
    // uses to change task (invoke when UI button is tapped)
    public void ReloadQuest()
    {
        Click();
        _finishActor.InitFinishShopper(RELOAD_BUTTON_INTERNAL_TIME);
        _reloadButton.interactable = false;
    }

    // if shopper has arrived make reload button available for tapping
    private void SetInteractable()
    {
        _reloadButton.interactable = true;
    }
}
