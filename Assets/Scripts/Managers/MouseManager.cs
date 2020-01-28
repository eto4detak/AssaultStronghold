using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    //  private SelectObjects selecting;
    public CharacterManager character;

    private void Start()
    {
       // selecting = SelectObjects.instance;
    }

    void Update()
    {
        if (!IsPointerOverUIObject())
        {
            CheckLeftClick();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData e = new PointerEventData(EventSystem.current);
        e.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        return results.Count > 0;
    }

    //private void CheckLeftClick()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(mouseRay, out RaycastHit mouseHit, 100))
    //        {
    //            CharacterManager filter = mouseHit.collider.GetComponent(typeof(CharacterManager)) as CharacterManager;
    //            if (filter)
    //            {
    //                OnClickLeftUnit(filter);
    //                return;
    //            }
    //            Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
    //            if (filterTerrain)
    //            {


    //                OnClickLeftTerrain(filterTerrain);
    //                return;
    //            }
    //        }
    //    }
    //}

    private void CheckLeftClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            CharacterManager unitTarget;
            if (Physics.Raycast(mouseRay, out RaycastHit mouseHit, 100))
            {
                //attack
                unitTarget = mouseHit.collider.GetComponent<CharacterManager>();
                if (unitTarget)
                {
                    OnClickRightObject(unitTarget);
                    return;
                }

                //move
                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    OnClickRight(mouseHit.point);
                    return;
                }
            }
        }
    }

    protected void OnClickLeftUnit(CharacterManager unit)
    {
     //   selecting.Deselect();
      //  selecting.SelectUnit(unit);
    }

    protected void OnClickLeftTerrain(Terrain terrain)
    {
      //  if (selecting.HaveSelected()) selecting.Deselect();
      //  GManager.gameHUD.ClearPanel();
    }

    private void OnClickRightObject(CharacterManager target)
    {
        Debug.Log("character");
        character.attack.Attack(target.health);
        //if (selecting.HaveSelected())
        //{
        //    if (selecting.GetSelected()[0] == target) return;
        //    CharacterManager uTarget = target.GetComponent<Unit>();
        //    if (!uTarget || (uTarget && Unions.instance.CheckEnemies(uTarget.faction, selecting.GetSelected()[0].faction) ) )
        //        for (int i = 0; i < selecting.GetSelected().Count; i++)
        //            selecting.GetSelected()[i].SetCommand(new AttackCommand(selecting.GetSelected()[i], target));
        //}
        //  GManager.gameHUD.SetTarget(target);
    }

    private void OnClickRight(Vector3 point)
    {
        character.Move(point);
        //if (selecting.GetSelected().Count == 1) Unit.SetMoveCommand(selecting.GetSelected(), point);
        //else if (selecting.GetSelected().Count > 1) Formation.MoveFormationRow(selecting.GetSelected(), point);
    }
}
