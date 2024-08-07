using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PackCell : MonoBehaviour
{
    public UnityEngine.UI.RawImage _cellRawImage;
    
    public int _goodsId=0;

    public int getGoodsId(){
        return _goodsId;
    }

   

    public void setGoodsId(int id){
        _goodsId = id;

    }

    public void updateImage(){
         if(_goodsId==0){
            _cellRawImage.texture = Resources.Load<Texture>("Art/Sprites/Items/tou");
        }else{
            _cellRawImage.texture = Resources.Load<Texture>(ItemsInfo.getInstance().getSpritePath(_goodsId));
        }

    }

    void Awake(){
        // Debug.Log(ItemsInfo.getInstance().getSpritePath(2));
        _cellRawImage = transform.Find("RawImage").GetComponent<UnityEngine.UI.RawImage>();
        // _cellRawImage.texture = Resources.Load<Texture>(ItemsInfo.getInstance().getSpritePath(2));
        
    }
    void Start(){
        //Debug.Log(ItemsInfo.getInstance().getSpritePath(2));
        if(_goodsId==0){
            _cellRawImage.texture = Resources.Load<Texture>("Art/Sprites/Items/tou");
        }else{
            _cellRawImage.texture = Resources.Load<Texture>(ItemsInfo.getInstance().getSpritePath(_goodsId));
        }
        
    }
}
