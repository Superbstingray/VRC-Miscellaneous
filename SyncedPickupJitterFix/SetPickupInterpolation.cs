
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Superbstingray
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
	public class SetPickupInterpolation : UdonSharp.UdonSharpBehaviour
	{

	VRCPlayerApi localPlayer;
	VRC_Pickup pickupRight, pickupLeft;

		void Start()
		{
			localPlayer = Networking.LocalPlayer;
		}

		public void FixedUpdate()
		{
            if (!VRC.SDKBase.Utilities.IsValid(localPlayer)) { return; }
            
			pickupLeft = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left);
			pickupRight = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);

			if (VRC.SDKBase.Utilities.IsValid(pickupLeft))
			{
				pickupLeft.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
			}

			if (VRC.SDKBase.Utilities.IsValid(pickupRight))
			{
				pickupRight.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
			}
		}
	}
}
