using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    //public List<GameObject> Foods;
    [SerializeField] GameObject SpawnArea;
    ObjectPooler objectPooler;
    int PullForce = -4;

    void Start()
    {
        StartCoroutine(nameof(Spawning));
        
    }
    IEnumerator Spawning()
    {
        if(!GameManager.isGameStarted||GameManager.isGameEnded)
        {
            while(!GameManager.isGameStarted || GameManager.isGameEnded) 
            {
                yield return new WaitForSeconds(2f);
                Debug.Log("WaitingFor2Seconds");
            }
            if (GameManager.isGameStarted && !GameManager.isGameEnded)
            {
                Debug.Log("SecondArea");
                if (GameManager.LevelNumber == 1)
                {
                    PullForce = -4;
                    ConveyorMover.OffsetSpeed = 0.0007f;
                    FoodCreator("Bread");
                    yield return new WaitForSeconds(4f);
                    FoodCreator("Hamburger");
                    yield return new WaitForSeconds(4f);
                    FoodCreator("Waffle");
                    yield return new WaitForSeconds(4f);
                    FoodCreator("IceCream");
                    yield return new WaitForSeconds(2f);
                    FoodCreator("Fish");
                    yield return new WaitForSeconds(2f);
                    FoodCreator("Pie");
                    yield return new WaitForSeconds(8f);
                    if (GameManager.instance.LastCounted > 0)
                    {
                        OnFinish();
                    }
                    else
                    {
                        Failed();
                    }
                }
                if (GameManager.LevelNumber == 2)
                {
                    PullForce = -4; 
                    ConveyorMover.OffsetSpeed = 0.0007f;
                    FoodCreator("HamEgg");
                    yield return new WaitForSeconds(4f);
                    FoodCreator("Ham");
                    yield return new WaitForSeconds(3f);
                    FoodCreator("Donuts");
                    PullForce = -5;
                    ConveyorMover.OffsetSpeed = 0.0015f;
                    yield return new WaitForSeconds(4f);
                    FoodCreator("IceCream");
                    yield return new WaitForSeconds(2f);
                    FoodCreator("Pie");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Cake");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Hamburger");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Donuts");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Ham");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Waffle");
                    yield return new WaitForSeconds(1.5f);
                    FoodCreator("Fish");
                    yield return new WaitForSeconds(7f);
                    if (GameManager.instance.LastCounted > 0)
                    {
                        OnFinish();
                    }
                }
            }
        }
        
    }
    public void FoodCreator(string foodname)
    {
        
        GameObject instantiatedFood = ObjectPooler.Instance.SpawnForGameObject(foodname, SpawnArea.transform.position, Quaternion.identity);
        BornForce(instantiatedFood);
    }
    public void BornForce(GameObject food)
    {
        food.GetComponent<ConstantForce>().force = new Vector3(0, 0, PullForce);
        Debug.Log("ApplyingForce");
    }
    public void OnFinish()
    {
        GameManager.instance.EndGame();
        GameManager.instance.OnLevelCompleted();
    }
    public void Failed()
    {
        GameManager.instance.EndGame();
        GameManager.instance.OnLevelFailed();
    }
}
