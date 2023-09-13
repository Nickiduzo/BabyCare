using UnityEngine;

namespace Main
{
    [CreateAssetMenu(fileName = "MainLevel", menuName = "Configs/MainLevel")]
    public class MainSceneConfig : ScriptableObject
    {
        //0 - In Green shirt
        //1 - In Purple shirt
        //2 - In Blue shirt
        //3 - In pink Shirt
        //4 - In Yellow Shirt
        //5 - In red Shirt
        public int BabyType;
    }
}
