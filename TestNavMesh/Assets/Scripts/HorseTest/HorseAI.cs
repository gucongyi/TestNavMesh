using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorseAI : MonoBehaviour
{
    public List<Transform> targets;
    public int HowMuchMeters;
    private float speed=5f;
    private float acceleration = 3f;
    private NavMeshAgent agent;
    private float randomTime = 5f;
    private List<Vector3> targetAllTransformPos = new List<Vector3>();
    private Vector3 CurrSetPos;
    private bool isSetNextPos = false;
    private bool isHorseStop;
    void Start()
    {
        if (HowMuchMeters==0)
        {
            HowMuchMeters = 1000;
        }

        switch (HowMuchMeters)
        {
            case 1000:
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
            case 1200:
                targetAllTransformPos.Add(targets[3].transform.position);
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
            case 1400:
                targetAllTransformPos.Add(targets[3].transform.position);
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
            case 1600:
                targetAllTransformPos.Add(targets[3].transform.position);
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
            case 1800:
                targetAllTransformPos.Add(targets[3].transform.position);
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
            case 2000:
            case 2200:
            case 2400:
                targetAllTransformPos.Add(targets[1].transform.position);
                targetAllTransformPos.Add(targets[2].transform.position);
                targetAllTransformPos.Add(targets[3].transform.position);
                targetAllTransformPos.Add(targets[0].transform.position);
                targetAllTransformPos.Add(targets[1].transform.position);
                break;
        }
        agent = GetComponent<NavMeshAgent>();
        CurrSetPos = targetAllTransformPos[0];
        targetAllTransformPos.RemoveAt(0);
        agent.SetDestination(CurrSetPos);
    }
    bool HaveNextPos()
    {
        if (targetAllTransformPos.Count>0)
        {
            return true;
        }

        return false;
    }

    Vector3 GetNextPos()
    {
        var Pos=targetAllTransformPos[0];
        CurrSetPos = Pos;
        targetAllTransformPos.RemoveAt(0);
        return Pos;
    }

    void Update()
    {
        if (isHorseStop)
        {
            return;
        }

        if (Vector3.Distance(this.transform.position, CurrSetPos) <20f&& Vector3.Distance(this.transform.position, CurrSetPos)>10)//10-20m设置下一个点
        {
            if (HaveNextPos()&& isSetNextPos==false)
            {
                agent.SetDestination(GetNextPos());
                isSetNextPos = true;
            }
        }

        

        if (Vector3.Distance(this.transform.position, CurrSetPos) < 10)
        {
            isSetNextPos = false;//重置变量
        }

        if (!isHorseStop&& Vector3.Distance(this.transform.position, CurrSetPos) < 1f)
        {
            isHorseStop = true;
            agent.speed = 0;
            agent.acceleration = 0;
            var animtors = this.transform.GetComponentsInChildren<Animator>();
            if (animtors != null && animtors.Length > 0)
            {
                foreach (var eachAnimator in animtors)
                {
                    eachAnimator.SetTrigger("Reach");
                }
            }
            return;
        }

        randomTime -= Time.deltaTime;
        if (randomTime <= 0f)
        {
            speed = Random.Range(5f, 10f);
            acceleration = Random.Range(3f, 5f);
            randomTime = Random.Range(5f, 10f);
        }
        agent.speed = speed;
        agent.acceleration = acceleration;
        //RaycastHit hit;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //        agent.SetDestination(hit.point);

        //}
    }
}
