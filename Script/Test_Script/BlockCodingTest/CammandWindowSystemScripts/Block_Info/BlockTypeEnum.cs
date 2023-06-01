/// <summary>
/// 블록의 종류를 모아놓은 열거형 변수
/// </summary>
/// tag :
/// DataBlock
/// ValueBlock
/// ActBlock
/// ConditionBlock
/// OperatorBlock
/// PrimeBlock
///

public enum BlockTypeEnum
{
    PinBlock,
    ValueBlock,
    MotorBlock, DoorBlock, SwitchBlock,
    IfBlock, ElseBlock, ExitBlock, CalculateBlock,
    OperatorBlock, ComparisonBlock,
    StartBlock, EndBlock,
    InfraredSensorBlock, //IR-Sensor
    InputKeyBlock
}