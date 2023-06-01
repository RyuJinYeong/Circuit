using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BlockCompile : MonoBehaviour
{
    /// <summary>
    /// Start블록에서 end블록까지의 행동양식을 만들고 C.C에 전달해줌
    /// 스크립트 Canvas의 start블럭에 연결
    /// </summary>
    ///

    //compile성공시 캔버스의 가장 아래 자식으로 설정해 드래그와 클릭을 막는 패널 오브젝트
    public GameObject blockPanel;

    public GameObject compileResult; //컴파일 결과에 따라 띄울 팝업 오브젝트
    public Text text; //팝업 오브젝트에 들어갈 문자
    public bool isOpen = false;

    public GameObject commandController; //Canvas와 연결된 C.C 오브젝트

    public bool startCompile = false; //compile을 시작하는 버튼을 눌렀을 때 true
    [SerializeField] private bool successCompile = false; //compile 성공 여부
    private bool isOnce = false; //compile시작시 한번만 실행하는 함수

    public GameObject endBlock; //endBlock

    [SerializeField] private int[] spawnPinNum; //생성된 pin의 종류(pin의 숫자)

    [SerializeField] private PinInfo[] pinInfos; //pin숫자에 따라 저장할 정보값을 모아둔 배열

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(startCompile) //compile 시작 버튼을 눌렀을 때
        {
            try
            {
                if (!isOnce)
                {
                    spawnPinNum = KindOfPin(); //핀의 종류를 받아옴
                    pinInfos = SetPinInfos();
                    isOnce = true;
                    ConditionBlockStartSetting(this.gameObject); //If문과 연결되는 exit블럭들을 올바르게 연결해줌
                }
                StartCompile(this.gameObject, endBlock);
                successCompile = true;
                //commandController.GetComponent<MonitorController>().successCompile = successCompile; //monitor에 컴파일 성공여부 전달
                if (!isOpen)
                {
                    StopDrag(successCompile); //true
                    text.text = "코드 실행 성공";
                    compileResult.transform.SetAsLastSibling();
                    compileResult.SetActive(true);
                    isOpen = true;
                }
            }
            catch(System.Exception e)
            {
                Debug.Log("Compile Failed\nmessage : "+ e);
                //successCompile = false;
                //startCompile = false;
                //commandController.GetComponent<MonitorController>().successCompile = successCompile; //monitor에 컴파일 성공여부 전달
                setStartCompile(false);
                Debug.Log("DebugPoint1");
                StopDrag(successCompile); //false
                text.text = "코드 실행 실패";
                compileResult.transform.SetAsLastSibling();
                compileResult.SetActive(true);
            }
        }
        //else //stop버튼을 눌렀을 때
        //{
        //    Destroy(this.GetComponent<PinInfo>()); //startBlock에 추가된 pinInfos의 PinInfo Component 삭제
        //    isOnce = false;
        //    //commandController.GetComponent<MonitorController>().successCompile = successCompile; //monitor에 컴파일 성공여부 전달
        //}
    }

    public bool getSuccessCompile()
    {
        return successCompile;
    }

    private void StartCompile(GameObject start, GameObject end)
    {
        //GameObject block = this.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //연결된 블럭(초기값 : start블럭 다음에 연결된 블럭)
        GameObject block = start.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject;
        GameObject actBlock;
        while (block != end) //다음 블럭이 endBlock이면 종료
        {
            //Debug.Log("block name : " + block.name);
            switch (block.GetComponent<BlockType>().bte)
            {
                case BlockTypeEnum.PinBlock:
                    actBlock = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //actSlot에 연결된 블럭
                    BlockTypeEnum blockType = actBlock.GetComponent<BlockType>().bte; //actSlot에 연결된 블럭의 종류

                    int pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());//block의 pin번호와 동일한 번호를 가지고 있는 가성pinBlock의 배열에서의 위치
                    int actNum = pinInfos[pinNum].getPinNum(); //연결된 디바이스가 연결된 핀의 위치, pinInfos[pinNum] : pinBlock과 동일한 번호를 가진 가상pinInfo
                    //int actNum = spawnPinNum[pinNum];
                    //디바이스의 연결된 핀숫자 != pinInfo에 저장되는 순서
                    //Debug.Log("pinNum : " + pinNum);
                    //Debug.Log("pinNum : "+pinNum +"actNum : "+ actNum);
                    PinBlockDataChange(block, pinInfos[pinNum]); //pinBlock과 pinInfos의 pinInfo와 데이터 교환
                    PinBlockAction(blockType, block, actBlock); //pin블럭의 동작함수
                    PinBlockDataChange(block, pinInfos[pinNum], true);

                    SendPinDataToDevice(pinInfos[pinNum], commandController.GetComponent<ActiveElementFind>().activeElement[actNum]); //PinData Device로 데이터 송신 및 수신
                    break;
                case BlockTypeEnum.IfBlock:
                    actBlock = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject;
                    //ConditionBlockStartSetting(block); //ConditionBlock Start Setting
                    getConditionResult(block, actBlock); //조건문 실행결과 ConditionBlock에 저장
                    block = ConditionBlockAction(block); //조건문 실행 및 다음에 실행할 블럭 설정
                    break;
                case BlockTypeEnum.ElseBlock:
                    break;
                case BlockTypeEnum.ValueBlock:
                case BlockTypeEnum.MotorBlock:
                case BlockTypeEnum.DoorBlock:
                case BlockTypeEnum.SwitchBlock:
                    Debug.Log("Wrong Connection : ActBlock at Slot");
                    break;
                default:
                    Debug.Log("해당하는 블럭 없음");
                    break;
            }
            block = block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //다음 블럭으로 지정
        }
    }

    /// <summary>
    /// actSlot에 있는 블럭의 종류에 따라 해당되는 동작수행하는 함수
    /// </summary>
    /// <param name="blockType">actSlot의 들어간 오브젝트의 BlockType</param>
    /// <param name="parentBlock"></param>
    /// <param name="actBlock"></param>
    ///
    private void PinBlockAction(BlockTypeEnum blockType, GameObject parentBlock = null, GameObject actBlock = null)
    {
        switch (blockType)
        {
            case BlockTypeEnum.ValueBlock:
                getValueBlockInfoToPinBlock(parentBlock, actBlock);
                break;
            case BlockTypeEnum.SwitchBlock:
                getSwitchBlockInfoToPinBlock(parentBlock, actBlock);
                break;
            case BlockTypeEnum.MotorBlock:
            case BlockTypeEnum.DoorBlock:
                setPinInfoToDeviceBlock(parentBlock, actBlock);
                break;
        }
    }

    /// <summary>
    /// If블럭의 조건문을 해석해 다음 동작을 어떻게 할지 결정하는 함수
    /// If문 안의 결과값(bool)에 따라
    /// If문 아래의 문장을 실행할지
    /// 다음 문장(또는 else문)으로 넘어갈지 결정
    /// </summary>
    /// <param name="parentBlock">If Block</param>
    private GameObject ConditionBlockAction(GameObject parentBlock = null)
    {
        GameObject exitBlock; //조건문이 실행해야 하는 블럭이 끝나는 블럭
        GameObject nextBlock; //조건문 실행 후 넘어가야 할 블럭(조건문 안에 있는 블록까지 실행 후)

        GameObject elseBlock = null;

        if (parentBlock.GetComponent<ConditionInfo>().exitBlock == null)
        {
            Debug.Log("Wrong Connection : IfBlock don't have ExitBlock");
            return null;
        }

        if(parentBlock.GetComponent<ConditionInfo>().elseBlock != null)
        {
            elseBlock = parentBlock.GetComponent<ConditionInfo>().elseBlock;
        }

        if(parentBlock.GetComponent<ConditionInfo>().getResult()) //조건문의 결과
        { //result = true
            exitBlock = parentBlock.GetComponent<ConditionInfo>().exitBlock;
            StartCompile(parentBlock, exitBlock);

            if(elseBlock == null) //elseBlock이 없는 경우
            {
                nextBlock = exitBlock; //ifBlock의 exitBlock
                return nextBlock;
            }
            else
            {
                nextBlock = elseBlock.GetComponent<ConditionInfo>().exitBlock; //elseBlock의 exitBlock
                return nextBlock;
            }
        }
        else
        { //result = false
            if(elseBlock == null) //elseBlock이 없는 경우
            {
                exitBlock = parentBlock.GetComponent<ConditionInfo>().exitBlock;
                nextBlock = exitBlock;
                return nextBlock;
            }
            else
            {
                exitBlock = elseBlock.GetComponent<ConditionInfo>().exitBlock; //elseBlock의 exitBlock
                StartCompile(elseBlock, exitBlock);
                nextBlock = exitBlock;
                return nextBlock;
            }
        }
    }

    /// <summary>
    /// actSlot에 있는 switchBlock의 정보를 pinBlock에 받아오는 함수
    /// </summary>
    /// <param name="parentBlock"></param>
    /// <param name="actBlock"></param>
    /// 
    private void getSwitchBlockInfoToPinBlock(GameObject parentBlock, GameObject actBlock)
    {
        try
        {
            parentBlock.GetComponent<PinInfo>().setIsOn(actBlock.GetComponent<SwitchInfo>().getIsOn());
        }
        catch
        {
            Debug.Log("Compile Error : getSwitchBlockInfoToPinBlock");
        }
    }

    /// <summary>
    /// actSlot에 있는 valueBlock의 정보를 pinBlock에 받아오는 함수
    /// </summary>
    /// 
    private void getValueBlockInfoToPinBlock(GameObject parentBlock, GameObject actBlock)
    {
        try
        {
            if(actBlock.GetComponent<ValueInfo>().getIsPositive()) //양수
            {
                parentBlock.GetComponent<PinInfo>().setValue(actBlock.GetComponent<ValueInfo>().getValue());
            }
            else //음수
            {
                parentBlock.GetComponent<PinInfo>().setValue(actBlock.GetComponent<ValueInfo>().getValue() * -1);
            }
        }
        catch
        {
            Debug.Log("Compile Error : getValueBLockInfoToPinBlock");
        }
    }

    /// <summary>
    /// If블럭을 만났을 때 조건문의 결과를 If블럭에 전달해주는 함수
    /// </summary>
    /// <param name="parentBlock">If블럭</param>
    /// <param name="actBlock">조건문의 첫번째 블럭(PinBlock)</param>
    /// 
    private void getConditionResult(GameObject parentBlock, GameObject actBlock)
    {
        GameObject block = actBlock;
        GameObject oper;
        float leftValue = 0.0f;
        float rightValue = 0.0f;
        int pinNum;

        bool result;

        //actBlock의 첫번째가 Data블럭이 아니면 컴파일 실패
        if(block.GetComponent<BlockType>().bte != BlockTypeEnum.PinBlock)
        {
            Debug.Log("LeftTerm Not DataBlock");
            return;
        }

        //오퍼레이터 기준 좌항에 대한 정보
        switch(block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject.GetComponent<BlockType>().bte)
        {
            case BlockTypeEnum.SwitchBlock: //해당Pin이 Of/Off이진 확인하여 결과 전송
                bool conditionBool;
                //pinNum = block.GetComponent<PinInfo>().getPinNum();
                pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());
                conditionBool = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject.GetComponent<SwitchInfo>().getIsOn(); //switchBlock의 현재 상태
                result = pinInfos[pinNum].getIsOn() == conditionBool ? true : false;
                parentBlock.GetComponent<ConditionInfo>().setResult(result);
                return;
            case BlockTypeEnum.InputKeyBlock:
                int index = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject.GetComponent<InputKeyInfo>().getKeyIndex(); //연결된 InputKeyBlock의 선택된 Key의 주솟값
                pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());
                result = pinInfos[pinNum].getInputKeyDown(index); //현재 눌린 키 상태
                parentBlock.GetComponent<ConditionInfo>().setResult(result);
                return;
            case BlockTypeEnum.MotorBlock: //해당Pin에 지정된 블럭타입 비교하여 결과 전송 (DeviceBlock)
            case BlockTypeEnum.DoorBlock:
            case BlockTypeEnum.InfraredSensorBlock:
                //pinNum = block.GetComponent<PinInfo>().getPinNum();
                pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());
                if (pinInfos[pinNum].getPinControlBlock() == block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject.GetComponent<BlockType>().bte)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                parentBlock.GetComponent<ConditionInfo>().setResult(result);
                return;
            case BlockTypeEnum.ValueBlock: //해당Pin의 Value값을 좌항에 저장
                //pinNum = block.GetComponent<PinInfo>().getPinNum();
                pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());
                leftValue = pinInfos[pinNum].getValue();
                break;
        }
        block = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //PinBlock의 ActBlock
        block = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //OperatorBlock

        oper = block;

        block = block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject; //operatorBlock의 ActBlock

        if (oper.GetComponent<BlockType>().bte != BlockTypeEnum.ComparisonBlock) //비교연산자가 아니면 컴파일 실패
        {
            Debug.Log("Not ComparisonOperator");
            return;
        }

        if(block.GetComponent<BlockType>().bte == BlockTypeEnum.PinBlock)
        {
            //PinBlock의 ActBlock
            if(block.GetComponent<DragDrop>().actSlot.GetComponent<Slot>().dropObject.GetComponent<BlockType>().bte != BlockTypeEnum.ValueBlock)
            {
                Debug.Log("RightTerm Not Value Block");
                return;
            }

            //pinNum = block.GetComponent<PinInfo>().getPinNum();
            pinNum = FindPinIndex(block.GetComponent<PinInfo>().getPinNum());
            rightValue = pinInfos[pinNum].getValue();

            switch (oper.GetComponent<OperatorInfo>().getOperator())
            {
                case '=':
                    if (leftValue == rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    return;
                case '>':
                    if (leftValue > rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    rightValue = block.GetComponent<ValueInfo>().getValue();
                    return;
                case '<':
                    if (leftValue < rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    rightValue = block.GetComponent<ValueInfo>().getValue();
                    return;
            }
            return;
        }
        else //ValueBlock
        {
            if(block.GetComponent<BlockType>().bte != BlockTypeEnum.ValueBlock)
            {
                Debug.Log("ReftTerm Not Value Block");
                return;
            }

            switch(oper.GetComponent<OperatorInfo>().getOperator())
            {
                case '=':
                    rightValue = block.GetComponent<ValueInfo>().getValue();
                    if (!block.GetComponent<ValueInfo>().getIsPositive())
                        rightValue *= -1;
                    if(leftValue == rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    return;
                case '>':
                    rightValue = block.GetComponent<ValueInfo>().getValue();
                    if (!block.GetComponent<ValueInfo>().getIsPositive())
                        rightValue *= -1;
                    if (leftValue > rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    //rightValue = block.GetComponent<ValueInfo>().getValue();
                    return;
                case '<':
                    rightValue = block.GetComponent<ValueInfo>().getValue();
                    if (!block.GetComponent<ValueInfo>().getIsPositive())
                        rightValue *= -1;
                    if (leftValue < rightValue)
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(true);
                    }
                    else
                    {
                        parentBlock.GetComponent<ConditionInfo>().setResult(false);
                    }
                    //rightValue = block.GetComponent<ValueInfo>().getValue();
                    return;
            }
            return;
        }
    }

    /// <summary>
    /// pinBlock이 제어한 블럭의 종류를 설정해줌
    /// motor인지 door인지 등
    /// </summary>
    ///
    private void setPinInfoToDeviceBlock(GameObject parentBlock, GameObject actBlock)
    {
        ////MotorInfo.cs파일을 이용할 때 사용한 스크립트
        //try
        //{
        //    actBlock.GetComponent<MotorInfo>().setPower((int)parentBlock.GetComponent<PinInfo>().getValue()); //motorBlock의 power값 전달
        //    actBlock.GetComponent<MotorInfo>().setIsOn(parentBlock.GetComponent<PinInfo>().getIsOn()); //전원부여 여부값 전달
        //}
        //catch
        //{
        //    Debug.Log("Compile Error : setPinInfoToMotorBlock");
        //}
        try
        {
            parentBlock.GetComponent<PinInfo>().setPinControlBlock(actBlock.GetComponent<BlockType>().bte); //pinBlock이 제어할 블럭 설정
        }
        catch
        {
            Debug.Log("Compile Error : setPinInfoToDeviceBlock");
        }
    }

    /// <summary>
    /// IfBlock이 시작할 때 해당 If블럭과 맞는 Else & Exit Block을 연결시켜주는 함수
    /// </summary>
    /// 
    private void ConditionBlockStartSetting(GameObject firstBlock)
    {
        GameObject block = firstBlock;
        GameObject[] ifBlock = new GameObject[10];
        int ifPointer = 0;

        //If문에 맞는 ExitBlock, ElseBlock을 연결해주는 문장
        //If문의 순서를 계산해서 맞는 ExitBlock을 IfBlock에 부여함
        //ElseBlock의 경우 항상 if문이 끝나는 ExitBlock다음에 위치하도록 함
        //그 외의 경우는 잘못된 연결로 간주
        while(block != endBlock)
        {
            if(block.GetComponent<BlockType>().bte == BlockTypeEnum.IfBlock || block.GetComponent<BlockType>().bte == BlockTypeEnum.ElseBlock)
            {
                ifBlock[ifPointer] = block;
                ifPointer++;
            }
            else if(block.GetComponent<BlockType>().bte == BlockTypeEnum.ExitBlock)
            {
                //ExitBlock의 갯수가 부족하면 컴파일 실패
                //IfBlock의 ExitBlock인지 ElseBlock의 ExitBlock인지 구별할 필요성
                ifPointer--;
                ifBlock[ifPointer].GetComponent<ConditionInfo>().exitBlock = block; //ExitBlock설정

                if (block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject.GetComponent<BlockType>().bte == BlockTypeEnum.ElseBlock)
                { //ExitBlock 다음에 위차한 블럭이 ElseBlock의 경우
                    ifBlock[ifPointer].GetComponent<ConditionInfo>().elseBlock = block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //ElseBlock설정
                    block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject.GetComponent<ConditionInfo>().ifBlock = ifBlock[ifPointer]; //ElseBlock의 If블럭 설정
                }

                ifBlock[ifPointer] = null;
            }
            block = block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //다음 블럭으로 지정
            //Debug.Log("IfPointer : " + ifPointer);
        }
    }

    /// <summary>
    /// start - end에서 연결된 pin 번호의 종류를 알아내는 함수
    /// </summary>
    /// <returns></returns>
    private int[] KindOfPin()
    {
        HashSet<int> pinNum = new HashSet<int>();
        GameObject block = this.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //연결된 블럭(초기값 : start블럭 다음에 연결된 블럭)

        while (block != endBlock)
        {
            if(block.GetComponent<BlockType>().bte == BlockTypeEnum.PinBlock)
            {
                pinNum.Add(block.GetComponent<PinInfo>().getPinNum());
            }
            block = block.GetComponent<DragDrop>().slot.GetComponent<Slot>().dropObject; //다음 블럭으로 지정
        }

        //Debug.Log("KindOfPin : " + pinNum.ToArray());
        return pinNum.ToArray();
    }

    /// <summary>
    /// pin숫자에 따라 저장할 정보를 보관할 객체를 생성하는 함수
    /// </summary>
    /// 
    private PinInfo[] SetPinInfos()
    {
        PinInfo[] pi = new PinInfo[spawnPinNum.Length];
        int cnt = 0;
        foreach(int num in spawnPinNum)
        {
            pi[cnt] = this.gameObject.AddComponent<PinInfo>();// new PinInfo();
            pi[cnt].setPinNum(num);
            //Debug.Log("SetPinInfos : " + pi[cnt].getPinNum());
            cnt++;
        }
        return pi;
    }

    /// <summary>
    /// 해당 pinNum을 가진 PinInfo객체가 배열에서 어디에 있는지 주소값을 반환해주는 함수
    /// </summary>
    /// <param name="pi"></param>
    /// <returns></returns>
    /// 
    private int FindPinIndex(int pinNum)
    {
        int pinIndex = -1; //넘겨줄 주소값
        for(int i = 0; i < pinInfos.Length; i++)
        {
            if(pinNum == pinInfos[i].getPinNum())
            {
                //pinIndex = pinInfos[i].getPinNum();
                pinIndex = i;
                break;
            }
        }
        return pinIndex;
    }

    /// <summary>
    /// pinInfos에 있는 PinInfo 객체의 정보를 해당 block으로 옮겨줌
    /// reverse가 true일 경우 pinInfos에 있는 PinInfo 객체의 정보를 해당 block의 정보로 바꿔줌(업데이트)
    /// </summary>
    /// <param name="block"></param>
    /// <param name="pi"></param>
    /// <param name="reverse"></param>
    /// 
    private void PinBlockDataChange(GameObject block, PinInfo pi, bool reverse = false)
    {
        PinInfo bpi = block.GetComponent<PinInfo>();
        if(!reverse)
        {
            bpi.setActBlock(pi.getActBlock());
            bpi.setIsOn(pi.getIsOn());
            bpi.setPinControlBlock(pi.getPinControlBlock());
            bpi.setValue(pi.getValue());
        }
        else
        {
            pi.setActBlock(bpi.getActBlock());
            pi.setIsOn(bpi.getIsOn());
            pi.setPinControlBlock(bpi.getPinControlBlock());
            pi.setValue(bpi.getValue());
        }
    }

    /// <summary>
    /// 해당 pin과 연결된 device로 pin의 데이터를 넘겨줌
    /// device = commandController.GetComponent<ActiveElementFind>().activeElement;
    /// </summary>
    /// <param name="pi">전송할 데이터를 가지고있는 PinInfo객체</param>
    /// <param name="device">데이터를 보낼 device</param>
    /// 
    private void SendPinDataToDevice(PinInfo pi, GameObject device)
    {
        DeviceInfo di = device.GetComponent<DeviceInfo>();
        if(pi.getPinNum() == di.getPinNumber())
        {
            if(device.CompareTag("PassiveDevice"))
            {
                di.setIsOn(pi.getIsOn());
                pi.setValue(di.getValue());
                return;
            }
            if(device.CompareTag("Controller"))
            {
                di.setIsOn(pi.getIsOn());
                pi.setInputKeyDown(di.getInputKeyDown());
            }
            di.setValue(pi.getValue());
            di.setIsOn(pi.getIsOn());
        }
        else
        {
            Debug.Log("잘못된 핀번호");
        }
    }

    /// <summary>
    /// compile실패할 경우 또는 stop버튼을 누를 경우
    /// monitor와 연결된 Device의 DeviceInfo초기화
    /// </summary>
    /// 
    private void ResetDeviceInfo()
    {
        DeviceInfo di;
        //Debug.Log("ActiveElementFind.Length : " + commandController.GetComponent<ActiveElementFind>().activeElement.Length);
        for(int i = 0; i < commandController.GetComponent<ActiveElementFind>().activeElement.Length; i++)
        {
            //Debug.Log("i" + i);
            if(commandController.GetComponent<ActiveElementFind>().activeElement[i] == null)
            { //핀에 연결된 디바이스를 찾아 디바이스 정보 컴포넌트를 가져올 때 연결된 디바이스가 없으면 오류발생
                continue;
            }
            di = commandController.GetComponent<ActiveElementFind>().activeElement[i].GetComponent<DeviceInfo>();
            di.setIsOn(false);
            di.setValue(0);
            di.setPinNumber(-1);
        }
    }

    /// <summary>
    /// Generator패널에 있는 start버튼과 stop버튼과 연동됨
    /// 컴파일 시작, 정지 버튼
    /// </summary>
    /// 
    /// <param name="isStart"></param>
    public void setStartCompile(bool isStart)
    {
        startCompile = isStart;
        //stopButton을 눌렀을 때
        if(!isStart)
        {
            successCompile = isStart;
            Destroy(this.GetComponent<PinInfo>()); //startBlock에 추가된 pinInfos의 PinInfo Component 삭제
            ResetDeviceInfo(); //DeviceInfo 초기화
            isOnce = false;
            isOpen = false;
        }
    }

    /// <summary>
    /// compile성공시 Design Panel에 있는 블럭의 드래그를 할 수 없도록 함
    /// compile성공시 Spawner Panel에 있는 블럭을 클릭하여 생성을 할 수 없도록 함
    /// BlockPanel의 canvas내 자식순서를 가장 마지막으로 보내서 다른 블럭과 상호작용을 하지 못하도록 함
    /// </summary>
    /// 
    public void StopDrag(bool isStop)
    {
        if (isStop)
        {
            blockPanel.GetComponent<RectTransform>().SetAsLastSibling(); //하이라키에서 위치를 마지막으로 변경(동일한 상위 오브젝트 상에서)
        }
        else
        {
            blockPanel.GetComponent<RectTransform>().SetAsFirstSibling(); //하이라키에서 위치를 처음으로 변경(동일한 상위 오브젝트 상에서)
        }
    }
}
