using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mission : MonoBehaviour
{
    public List<MissionLine> path;
    public CharacterNavMovement movement;
    public bool isFinish;
    public int currentIndexPoint;
    public int roundCount;

    private List<CharacterManager> rivals;
    private CharacterManager self;
    private float deltaTime;
    private float changeSpellTime = 1f;

    private void Awake()
    {
        self = GetComponent<CharacterManager>();
    }


    private void Update()
    {
        DoMission();
        ApplySpell();
    }

    public void ResetRound()
    {
        roundCount = LevelManager.instance.levelData.roundCount;
    }

    private void DoMission()
    {
        if (isFinish)
        {
            movement.Stop();
            return;
        }

        if (path.Count > 0)
        {
            if (movement.needChangeNumberPath)
            {
                var target = GetNextPoint(movement.transform);
                movement.MoveToPoint(target.position - target.right);
            }
            else
                movement.MoveToPoint(GetNextPoint(movement.transform).position);
        }
    }

    private void ApplySpell()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime < changeSpellTime) return;

        deltaTime = 0;
        rivals = Statistic.instance.GetRivalForSportman(self);
        for (int r = 0; r < rivals.Count; r++)
        {
            for (int a = 0; a < self.attack.arsenal.Count; a++)
            {
                if (self.attack.arsenal[a].IsMayApply(self.transform, rivals[r].transform))
                {
                    self.attack.SelectSpell(self.attack.arsenal[a]);
                    self.attack.Attack(rivals[r].health);
                    return;
                }
            }
        }
    }


    public Transform GetNextPoint(Transform movement)
    {
        if (path.Count > 0)
        {
            float minDistance = Mathf.Infinity;
            float distance;
            int index = 0;
            for (int i = 0; i < path[0].points.Count; i++)
            {
                distance = (movement.position - path[0].points[i].position).magnitude;
                if (distance < minDistance)
                {
                    index = i;
                    minDistance = distance;
                }
            }
            currentIndexPoint = index;
            return path[0].points[index];
        }
        return null;
    }

    public Transform GetNextPointRotate(Transform movement)
    {
        GetNextPoint(movement);
        if (path.Count > 0)
        {
            int other = GetOtherIndex(path[0].points.Count, currentIndexPoint);
            return path[0].points[other];
        }
        return null;
    }

    private int GetOtherIndex(int count, int index)
    {
        if (count < 2 || currentIndexPoint < 0) return 0;
        if (index + 1 <= count) return index + 1;
        if (index - 1 <= count) return index - 1;
        return 0;
    }

    public bool CheckOnNearestLine(Transform movement)
    {
        if (path[0] != null)
        {
            Vector3 moveVector;
            for (int i = 0; i < path[0].points.Count; i++)
            {
                moveVector = path[0].points[i].position - movement.position;
                if (moveVector.x * moveVector.x + moveVector.z * moveVector.z < 0.01f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AtLine(MissionLine line)
    {
        if (path.Count == 0) return;
        if(path[0] == line)
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        path.RemoveAt(0);
        if(path.Count == 0)
        {
            roundCount--;
            if(roundCount > 0)
            {
                path = new List<MissionLine>(Level.instance.path);
            }
            else
            {
                isFinish = true;
                Level.instance.finishedSportsman.Add(movement.gameObject);
            }

        }
    }
}

