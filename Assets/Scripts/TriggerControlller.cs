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
                TriggerCompleted(collider);
            }
            else if (FoodType == FoodEnum.NonSweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet)
            {
                counted = -5;
                GameManager.instance.PointCounter(counted);
                TriggerCompleted(collider);
            }
            else if (FoodType == FoodEnum.NonSweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.NonSweet)
            {
                counted = 5;
                GameManager.instance.PointCounter(counted);
                TriggerCompleted(collider);
            }
            else if (FoodType == FoodEnum.SweetCounter && collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.NonSweet)
            {
                counted = -5;
                GameManager.instance.PointCounter(counted);
                TriggerCompleted(collider);
            }
            if (FoodType == FoodEnum.GarbageCollector && (collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.NonSweet || collider.GetComponent<TriggerControlller>().FoodType == FoodEnum.Sweet))
            {
                TriggerCompleted(collider);
            }
        }
    }
   void TriggerCompleted(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        ConstantForce SettingConstantForce = collider.GetComponent<ConstantForce>();
        SettingConstantForce.force = Vector3.zero;
        rb.velocity = Vector3.zero;
        collider.gameObject.SetActive(false);
    }
}
