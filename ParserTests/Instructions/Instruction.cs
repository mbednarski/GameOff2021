using System;

namespace Instructions {
public class IntelligenceProcessingUnit{}

public enum Instructions0op {
    NOOP
}
public enum Instructions1op {
    INC,
    DEC
}

public enum OperandType
{
    HEX_NUMBER,
    DEC_NUMBER,
    REG_NAME,
    REG_ADDR,
    HEX_ADDR
}

public class Operand
{
    public OperandType type;
    public Object value;
}

public  class Instruction
{
    public  bool Execute(IntelligenceProcessingUnit ipu){return true;}


}
}