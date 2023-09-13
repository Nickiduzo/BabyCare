using Quest;
using System;
using UnityEngine;

public class ShopperController : MonoBehaviour
{
    private Shopper _currentShopper;
    public event Action OnShopperArrived;
    public bool IsShopperExist => _currentShopper != null;

    // monitor whether the shopper has arrived
    public void SetShopper(Shopper shopper)
    {
        _currentShopper = shopper;
        shopper.OnArrived += OnShopperArrived;
        shopper.transform.SetParent(transform);
    }

    // return current Shopper in SpawnFinishShopper
    public Shopper GetShopper()
        => _currentShopper;
}
