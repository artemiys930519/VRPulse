using VR.Player.Interfaces;

namespace VR.Player
{
    public class OculusTrackingChecker : IHandTrackingChecker
    {
        public bool IsHandTrackingEnable()
        {
            return OVRPlugin.GetHandTrackingEnabled();
        }
    }
}