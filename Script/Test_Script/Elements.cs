using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element_type
{
    Voltage_Source, //전압원
    Current_Source, //전류원
    Anode, //양극
    Cathode, //음극
    Resistance, //저항
    Node_In, //노드 Input
    Node_Out //노드 Out
}

public class Elements : MonoBehaviour
{
    public float voltage; //전압
    public float current; //전류
    public float resistance; //저항
    public Element_type elty; //현재 소자의 종류
    public int numEl = 1; //연결될 소자의 개수
    public Elements[] elements; //다음에 연결된 소자

    public Elements(float voltage, float current, float resistance, Element_type elty, int numEl)
    {
        this.voltage = voltage;
        this.current = current;
        this.resistance = resistance;
        this.elty = elty;
        this.numEl = numEl;
        elements = new Elements[numEl];
    }

    public Elements(float voltage, float current, float resistance, Element_type elty, int numEl, Elements[] elements)
    {
        this.voltage = voltage;
        this.current = current;
        this.resistance = resistance;
        this.elty = elty;
        this.numEl = numEl;
        elements = new Elements[numEl];
        this.elements = elements;
    }
}