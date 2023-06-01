using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalElementValue : MonoBehaviour
{
    public Elements nextElement; //+극 쪽으로 연결된 소자
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalCircuit()
    {
        Elements e = nextElement;
        float r = 0.0f;
        while(e.elty != Element_type.Cathode) //종료조건: e.elty == 자기자신
        {
            switch(e.elty)
            {
                case Element_type.Resistance:
                    r += e.resistance;
                    break;
                case Element_type.Node_In:
                    float[] res = new float[e.elements.Length - 1];
                    
                    break;
            }
            e = e.elements[0];
        }
    }

    //직렬로 연결된 저항의 합성저항을 구함
    public float CalSerialResistance(Elements elements)
    {
        Elements e = elements;

        if (e.elty == Element_type.Node_Out || e.elty == Element_type.Cathode)
            return 0.0f;

        return e.resistance + CalSerialResistance(e.elements[0]);
    }

    //병렬로 연결된 저항의 합성저항을 구함
    public float CalParallelResistance(float[] r)
    {
        float g = 0.0f; // conductance (저항 역수)
        for(int i = 0; i <= r.Length; i++)
        {
            g += 1 / r[i];
        }
        return 1 / g;
    }

    //가장 안쪽에 있는 Node_In 찾기 
    public Elements FindLastNode(Elements elements) // elements == Node_In
    {
        Elements p = elements;

        while (p.elty != Element_type.Node_Out || p.elty != Element_type.Cathode)
        {
            if (p.elty == Element_type.Node_In)
                return FindLastNode(p);
            p = p.elements[0];
        }

        return p;
    }


    //이전에 있는 Node_In 찾기
    public Elements FindPreviousNode(Elements last, Elements first) //elements == last Node_In
    {
        Elements p = first;
        Elements t;

        while (p.elty != Element_type.Node_Out || p.elty != Element_type.Cathode)
        {
            p = p.elements[0];
            if (p.elty == Element_type.Node_In)
            {
                t = p;
                if (ReferenceEquals(p, last))
                    return t;
            }
        }

        return p;
    }
}
