using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour
{
    /// <summary>
    /// 블럭의 타입을 알려주는 스크립트
    /// 스크립트 블럭에 연결
    /// </summary>
    ///

    public BlockTypeEnum bte;

    public void setBlockType(BlockTypeEnum bte)
    {
        this.bte = bte;
    }
}
