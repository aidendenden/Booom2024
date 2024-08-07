using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<UI_PackCell> cells;
    List<int> pickedItems = new List<int>();
    private UnityEngine.UI.Button _Btn_ListUp;
    private UnityEngine.UI.Button _Btn_ListDown;
    private int currentPage;
    private int page;

     #region 单例
    private static Inventory _instance;
    public static Inventory getInstance()
    {
        return _instance;
    }
    #endregion
    public void ListUp(){
        if(page==1){
            return;
        }
        Debug.Log("ListUp");
        if(currentPage<page){
            currentPage++;
        }else if(currentPage==page){
            currentPage=1;
        }
        ItemUpdate();
    }
    public void ListDown(){
        if(page==1){
            return;
        }
        Debug.Log("ListDown");
        if(currentPage>1){
            currentPage--;
        }else if(currentPage==1){
            currentPage=page;
        }
        ItemUpdate();
    }

    void Awake(){
        _instance=this;
        _Btn_ListUp = transform.Find("ButtonUp").GetComponent<UnityEngine.UI.Button>();
        _Btn_ListDown = transform.Find("ButtonDown").GetComponent<UnityEngine.UI.Button>();

        _Btn_ListUp.onClick.AddListener(new UnityEngine.Events.UnityAction(ListUp));
        _Btn_ListDown.onClick.AddListener(new UnityEngine.Events.UnityAction(ListDown));

    }

    void Start(){
        pickedItems = PickedItems.getInstance().pickedItems;
        page = pickedItems.Count/4+1;
        currentPage = 1;
        ItemUpdate();
    }


    public void ItemUpdate(){
        PageUpdate();
        for(int i=0;i<4;i++){
            if(4*(currentPage-1)+i < pickedItems.Count){
                // Debug.Log(page+","+currentPage);
                // Debug.Log(page*(currentPage-1)+i+","+pickedItems[page*(currentPage-1)+i]);
                cells[i]._goodsId = pickedItems[4*(currentPage-1)+i];
                cells[i].updateImage();
            
            }else{
                cells[i]._goodsId = 0;
                cells[i].updateImage();
            }
            
        }
    }

    void PageUpdate(){
        pickedItems = PickedItems.getInstance().pickedItems;
        page = pickedItems.Count/4+1;
    }


}
