namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InputController : MonoBehaviour
    {
        [SerializeField] VRTK_SnapDropZone holster;
        [SerializeField] Gun vrGun;
        [SerializeField] float touchpadThreshold = .05f;
        [SerializeField] float triggerThreshold = .05f;

        float previousAxis = 0;

        private AudioSource ctrlAudio;

        private void Start()
        {
            ctrlAudio = GetComponent<AudioSource>();
        }

        void OnEnable()
        {
            VRTK_ControllerEvents controllerEvents = GetComponent<VRTK_ControllerEvents>();

            if (controllerEvents == null)
                Debug.LogError("VRTK_ControllerEvents is missing from this object");

            controllerEvents.TriggerPressed += ControllerEvents_TriggerPressed;
            controllerEvents.TriggerReleased += ControllerEvents_TriggerReleased;
            controllerEvents.TriggerTouchStart += ControllerEvents_TriggerTouchStart;
            controllerEvents.TriggerTouchEnd += ControllerEvents_TriggerTouchEnd;;
            controllerEvents.TriggerHairlineStart += ControllerEvents_TriggerHairlineStart;;
            controllerEvents.TriggerHairlineEnd += ControllerEvents_TriggerHairlineEnd;;
            controllerEvents.TriggerClicked += ControllerEvents_TriggerClicked;;
            controllerEvents.TriggerUnclicked += ControllerEvents_TriggerUnclicked;;
            controllerEvents.TriggerAxisChanged += ControllerEvents_TriggerAxisChanged;;
            controllerEvents.TriggerSenseAxisChanged += ControllerEvents_TriggerSenseAxisChanged;;

            controllerEvents.GripPressed += ControllerEvents_GripPressed;;
            controllerEvents.GripReleased += ControllerEvents_GripReleased;;
            controllerEvents.GripTouchStart += ControllerEvents_GripTouchStart;;
            controllerEvents.GripTouchEnd += ControllerEvents_GripTouchEnd;;
            controllerEvents.GripHairlineStart += ControllerEvents_GripHairlineStart;;
            controllerEvents.GripHairlineEnd += ControllerEvents_GripHairlineEnd;;
            controllerEvents.GripClicked += ControllerEvents_GripClicked;;
            controllerEvents.GripUnclicked += ControllerEvents_GripUnclicked;;
            controllerEvents.GripAxisChanged += ControllerEvents_GripAxisChanged;;

            controllerEvents.TouchpadPressed += ControllerEvents_TouchpadPressed;;
            controllerEvents.TouchpadReleased += ControllerEvents_TouchpadReleased;;
            controllerEvents.TouchpadTouchStart += ControllerEvents_TouchpadTouchStart;;
            controllerEvents.TouchpadTouchEnd += ControllerEvents_TouchpadTouchEnd;;
            controllerEvents.TouchpadAxisChanged += ControllerEvents_TouchpadAxisChanged;;
        }

        public void EquipGun(Gun newGun)
        {
            vrGun = newGun;
            AudioManager.instance.PlaySound("PickUpItem", ref ctrlAudio);
        }

        public void UnEquipGun()
        {
            vrGun = null;
            AudioManager.instance.PlaySound("PickUpItem", ref ctrlAudio);
        }

        void ControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
        }


        void ControllerEvents_TriggerTouchStart(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerUnclicked(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (vrGun != null) 
            {
                if (!holster.ValidSnappableObjectIsHovering()) 
                {
                    if (e.buttonPressure >= triggerThreshold)
                    {
                        vrGun.TriggerPull(e.buttonPressure);
                    }
                    else
                    {
                        vrGun.TriggerPull(0f);
                    }
                }
            }
        }

        void ControllerEvents_TriggerSenseAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripPressed(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripReleased(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripTouchStart(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripClicked(object sender, ControllerInteractionEventArgs e)
        {
            if (vrGun != null)
            {
                if (vrGun.loaded)
                {
                    vrGun.Eject();
                }
                else
                {
                    vrGun.Insert();
                }
            }
        }

        void ControllerEvents_GripUnclicked(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_GripAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TouchpadPressed(object sender, ControllerInteractionEventArgs e)
        {
           
        }

        void ControllerEvents_TouchpadReleased(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
        }

        void ControllerEvents_TouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            if (vrGun != null)
            {
                if (vrGun.loaded)
                {
                    vrGun.HammerPull(TouchPadAxisY(e.touchpadAxis.y, e.touchpadAngle));
                }
                else
                {
                    vrGun.cylinder.SpinCylinder(e.touchpadAngle, true);
                }
            }
        }


        float TouchPadAxisY(float axis, float angle)
        {
            float newAxis;

            if (Deg2Rad(angle) > Deg2Rad(90f) && Deg2Rad(angle) < Deg2Rad(270))
            {
                newAxis = (1 - axis) / 2;
            }
            else
            {
                newAxis = (axis / 2) + 0.5f;
            }

            float delta = newAxis - previousAxis;

            previousAxis = newAxis;

            return newAxis;
        }

        float Deg2Rad(float deg)
        {
            return deg * Mathf.Deg2Rad;
        }
    }
}