using System;
using Photon.Pun;

namespace liblib.Core;

public class MonoWrap : MonoBehaviourPunCallbacks
{
    protected MonoWrap()
    {
    }

    protected MonoWrap(IntPtr handle) : base(handle)
    {
    }
}