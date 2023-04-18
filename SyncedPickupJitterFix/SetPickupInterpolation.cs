
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Superbstingray
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
	public class SetPickupInterpolation : UdonSharp.UdonSharpBehaviour
	{

	public bool setKinematic;

	bool kinematicRight, kinematicLeft;
	bool nullRight, nullLeft;
	int frameSkipRight, frameSkipLeft;

	VRCPlayerApi localPlayer;
	VRC_Pickup pickupRight, pickupLeft;
	Rigidbody rigidRight, rigidLeft;

		void Start()
		{
			localPlayer = Networking.LocalPlayer;
		}

		void FixedUpdate()
		{
			if (!VRC.SDKBase.Utilities.IsValid(localPlayer)) { return; }

			pickupRight = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Right);
 
			if (VRC.SDKBase.Utilities.IsValid(pickupRight))
			{
				frameSkipRight++;
				rigidRight = pickupRight.GetComponent<Rigidbody>();
				rigidRight.interpolation = RigidbodyInterpolation.None;

				if (!nullRight && (frameSkipRight > 5))
				{
					kinematicRight = rigidRight.isKinematic;
				}

				if (setKinematic && (frameSkipRight > 5))
				{
					rigidRight.isKinematic = true;
					nullRight = true;
				}
			}
			else if (VRC.SDKBase.Utilities.IsValid(rigidRight))
			{
				rigidRight = null;
				nullRight = false;
				frameSkipRight = 0;

				if (frameSkipRight > 5)
				{
					rigidRight.isKinematic = kinematicRight;
				}
			}

			pickupLeft = localPlayer.GetPickupInHand(VRC_Pickup.PickupHand.Left);

			if (VRC.SDKBase.Utilities.IsValid(pickupLeft))
			{
				frameSkipLeft++;
				rigidLeft = pickupLeft.GetComponent<Rigidbody>();
				rigidLeft.interpolation = RigidbodyInterpolation.None;

				if (!nullLeft && (frameSkipRight > 5))
				{
					kinematicLeft = rigidLeft.isKinematic;
				}

				if (setKinematic && (frameSkipRight > 5))
				{
					rigidLeft.isKinematic = true;
					nullLeft = true;
				}
			}
			else if (VRC.SDKBase.Utilities.IsValid(rigidLeft))
			{
				rigidLeft = null;
				nullLeft = false;
				frameSkipLeft = 0;

				if (frameSkipLeft > 5)
				{
					rigidLeft.isKinematic = kinematicLeft;
				}
			}
		}
	}
}
