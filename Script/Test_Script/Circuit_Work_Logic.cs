using UnityEngine;

public class Circuit_Work_Logic : MonoBehaviour
{
    public GameObject motor;
    Elements el;
    public Elements testEl; // Next connected element
    public float voltage;

    // Start is called before the first frame update
    void Start()
    {
        el = new Elements(voltage, 0.0f, 0.0f, Element_type.Voltage_Source, 1);
        el.elements[0] = testEl;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("v:" + el.voltage + "  i:" + el.current + "  r:" + el.resistance + "  type:" + el.elty + "  elementNum:" + el.numEl);
        Circuit_calculate();
        //MotorRun(voltage);
    }

    public void MotorRun(float voltage) // 전압에 따라 회전속, 전류의 방향에 따라 회전방향 변환 (음수일경우 반대방향 회전)
    {
        motor.transform.Rotate(new Vector3(0, voltage * Time.deltaTime * 10.0f, 0)); // 제자리 회전
    }

    public void Circuit_calculate()
    {
        //int elCnt = 0; // 소자 개수
        float r = 0.0f; // 회로의 저항값 (등가회로에서의 저항값)
        float i; // 회로의 전류값 (등가회로에서의 전류값)
        Elements e;
        e = el.elements[0];

        // 소자의 개수 관련(저항값 등)
        
        while (e.elty != Element_type.Cathode)
        {
            switch (e.elty)
            {
                case Element_type.Resistance:
                    //elCnt++;
                    Debug.Log("r:" + e.resistance);
                    r += e.resistance;
                    break;
                case Element_type.Node_In: //병렬의 합성저항 계산
                    r += CalResistance(e);
                    while(e.elty != Element_type.Node_Out)
                    {
                        e = e.elements[0];
                    }
                    
                    break;
            }
            e = e.elements[0];
        }
        
        i = CalCurrent(r, voltage); // 루프에 흐르는 전류값 계산

        //주어진 소자에 계산된 값 대입
        e = el.elements[0];
        while (e.elty != Element_type.Cathode)
        {
            switch (e.elty)
            {
                case Element_type.Resistance:
                    e.current = i;
                    e.voltage = e.current * e.resistance; //전압값 부여
                    break;
            }
            e = e.elements[0];
        }

        //Debug.Log("elCnt: " + elCnt);
        Debug.Log("Total resistance: " + r);
        Debug.Log("Total current: " + i);
    }

    //전류 계산 함수
    public float CalCurrent(float resistance, float voltage)
    {
        float current;

        current = voltage / resistance;

        return current;
    }

    //합성저항 구하는 함수
    public float CalResistance(Elements elements)
    {
        Elements e = elements;
        switch (e.elty)
        {
            case Element_type.Cathode:
                return 0.0f;
            case Element_type.Node_Out:
                return CalResistance(e.elements[0]);
            case Element_type.Resistance:
                return e.resistance + CalResistance(e.elements[0]);
            case Element_type.Node_In:
                int elCnt = e.elements.Length - 1;
                Elements p;
                float[] r = new float[elCnt + 1];
                float res;
                
                for (int i = 0; i <= elCnt; i++)
                {
                    r[i] = 0.0f;
                    p = e.elements[i];
                    r[i] += CalResistance(p);
                    Debug.Log(i +":"+ r[i]);
                }

                res = 1 / r[0];
                for (int j = 1; j <= elCnt; j++)
                {
                    res += 1 / r[j];
                }
                return 1 / res;
            default:
                return e.resistance + CalResistance(e.elements[0]);
        }
    }
    /*
    public Elements FindLastNode(Elements elements) // elements == Node_In
    {
        Elements p = elements;

        while(p.elty != Element_type.Node_Out || p.elty != Element_type.Cathode)
        {
            if (p.elty == Element_type.Node_In)
                return FindLastNode(p);
            p = p.elements[0];
        }

        return p;
    }
    */
}