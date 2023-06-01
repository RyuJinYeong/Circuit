using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBlockDelete : MonoBehaviour
{
    //특정 C.C에서만 생성되거나 C.C에 연결된 능동소자의 activeBlock을 player의 canvas가 비활성화(C.C와 연결을 끊은 상태)일 때 삭제하는 코드
    //자신이 비활성화 되면 자기 자신 삭제
    //스크립트 해당되는 블럭 및 블럭 스포너에 연결

    private void OnDisable() //비활성화되면 실행
    {
        Destroy(gameObject);
    }
}
