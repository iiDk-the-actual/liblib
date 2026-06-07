using Photon.Pun;

namespace liblib.Libraries.RadiumWrapper.Extensions;

public static class PlayerExt
{
    public static bool IsLocal(this Player player) =>
        player.owner == PhotonNetwork.LocalPlayer;
}