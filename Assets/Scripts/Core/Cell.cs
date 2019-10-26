using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsBeeperPresented()
    {
        throw new NotImplementedException();
    }

    public bool IsBeeperNotPresented()
    {
        return !IsBeeperPresented();
    }

    public bool IsNorthClear()
    {
        throw new NotImplementedException();
    }

    public bool IsNorthNotClear()
    {
        return !IsNorthClear();
    }
}
