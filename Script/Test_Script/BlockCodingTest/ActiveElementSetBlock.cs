using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveElementSetBlock : MonoBehaviour
{
    //능동소자가 가지고 있는 activeBlock
    //Canvas에 생성할 이 능동소자에 관련된 activeBlock
    //CommandController(C.C)에 접근하면 C.C에 연결된 Canvas에 이 능동소자를 제어할 수 있는 activeBlock이 생성
    //예)모터에 경우 MotorBlock이 Canvas에 생성됨
    //스크립트 능동소자에 연결

    public GameObject activeBlock; //생성할 activeBlock
}
