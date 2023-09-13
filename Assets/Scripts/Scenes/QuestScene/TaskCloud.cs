using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestBaby
{
    public class TaskCloud : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer task;
        [SerializeField] private List<Sprite> sprites;

        public void setTask()
        {
            task.sprite = sprites[Random.Range(0 , sprites.Count)];
        }
    }
}
