using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControlller : MonoBehaviour
{
    [SerializeField] FoodEnum FoodType;
    int Points;
    private void OnTriggerEnter(Collider collider)
    {if (GameManager.isGameStarted && !GameManager.isGameEnded)
        {
            int counted = 0;
            TriggerControlller ColliderTriggerController = collider.GetComponent<TriggerControlller>();
            if (ColliderTriggerController == null) { return; }
            if (FoodType == FoodEnum.SweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet)
            {
                counted = 5;
                GameManager.instance.PointCounter(counted);
                collider.gameObject.SetActive(false);
            }
            else if (FoodType == FoodEnum.NonSweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet)
            {
                counted = -5;
                GameManager.instance.PointCounter(counted);
                collider.gameObject.SetActive(false);
            }
            if (FoodType == FoodEnum.NonSweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.NonSweet)
            {
                counted = 5;
                GameManager.instance.PointCounter(counted);
                collider.gameObject.SetActive(false);
            }
            else if (FoodType == FoodEnum.NonSweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet)
            {
                counted = -5;
                GameManager.instance.PointCounter(counted);
                collider.gameObject.SetActive(false);
            }
            if (FoodType == FoodEnum.GarbageCollector && (collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.NonSweet || collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet))
            {
                collider.gameObject.SetActive(false);
            }
        }
    }
   
}
