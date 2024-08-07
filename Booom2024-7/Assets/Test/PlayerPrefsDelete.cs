using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteButton(){
        PlayerPrefs.DeleteAll();
        PickedItems.getInstance().pickedItemNum = 0;
        PickedItems.getInstance().pickedItems.Clear();
    }


    public void PrintPlayerPrefs(){
        int num = PlayerPrefs.GetInt("PickedItemNum");
        for(int i=1;i<=num;i++){
            Debug.Log("picked:"+i+":"+PlayerPrefs.GetInt("picked"+i.ToString()));
        }
    }
}
