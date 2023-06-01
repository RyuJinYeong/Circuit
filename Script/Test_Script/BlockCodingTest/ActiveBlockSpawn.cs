using UnityEngine;

public class ActiveBlockSpawn : MonoBehaviour
{
    /// <summary>
    /// 추후 블럭스폰 방식이 아닌 드롭다운 형식으로 블럭연결을 구현할 수 있으면 구현
    /// actBlock을 드래그 형식으로 옮기는게 아닌 slot을 클릭하여 다음에 올 블럭을 선택
    /// </summary>
    //CommandController(C.C)에 연결된 능동소자의 관한 ActiveBlock을 연결된 Canvas에 생성함
    //Canvas의 Generator Panel에 생성함
    //스크립트 C.C에 연결

    //[SerializeField] private CommandWindowControl cwc; //canvas에 연결되있는 cwc
    //[SerializeField] private ActiveElementFind aef; //canvas에 연결되있는 aef

    //[SerializeField] private Canvas canvas; //상호작용한 플레이어의 canvas
    //[SerializeField] private GameObject generatorPanel; //플레이어 canvas의 generator Panel

    //private GameObject spawnActiveBlock;

    //private bool isSpawnBlock = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    cwc = this.GetComponent<CommandWindowControl>();
    //    aef = this.GetComponent<ActiveElementFind>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(cwc.getIsCanvasOpen()) //player와 C.C가 상호작용 하면
    //    {
    //        this.canvas = cwc.canvas; //canvas setting
    //        generatorPanel = canvas.transform.GetChild(0).gameObject; //canvas의 첫번째 자식오브젝트 (generator panel)

    //        if(!isSpawnBlock)
    //        {
    //            spawnActiveBlock = Instantiate(aef.activeElement.GetComponent<ActiveElementSetBlock>().activeBlock); //생성된 activeBlock

    //            spawnActiveBlock.transform.SetParent(generatorPanel.transform, false); //generatorPanel의 자식으로 설정 제일 마지막 순번으로 설정됨

    //            //activeBlock의 generator Panel에서 위치 설정
    //            //generator Panel의 activeBlock이전의 위치에서 일정 거리만큼 떨어지도록
    //            Vector2 lastChildPos = generatorPanel.transform.GetChild(generatorPanel.transform.childCount - 2).gameObject.GetComponent<RectTransform>().anchoredPosition;
    //            Vector2 gapPos = new Vector2(0, -150.0f);
    //            spawnActiveBlock.GetComponent<RectTransform>().anchoredPosition = lastChildPos + gapPos;

    //            //spawnActiveBlock에 연결된 스폰 스크립트 설정
    //            spawnActiveBlock.GetComponent<BlockSpawn_Single>().canvas = canvas;
    //            spawnActiveBlock.GetComponent<BlockSpawn_Single>().designPanel = canvas.transform.GetChild(1).gameObject;

    //            isSpawnBlock = true;
    //        }
    //    }
    //    else
    //    {
    //        isSpawnBlock = false;
    //    }

    //    //Debug.Log("isSpawnBlock:" + isSpawnBlock);
    //    //Debug.Log("isCanvasOpen:" + cwc.getIsCanvasOpen());
    //}
}
