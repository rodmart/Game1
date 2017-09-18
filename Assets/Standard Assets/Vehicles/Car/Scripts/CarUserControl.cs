using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public Text countText;
        public Text winText;
        public Text looseText;

        private int count;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        void Start ()
        {
        	count = 0;
        	setCountText();
        	winText.text = " ";
        	looseText.text = " ";
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

        void OnTriggerEnter(Collider other)
        {
        	if(other.gameObject.CompareTag("PickUp"))
        	{
        		other.gameObject.SetActive(false);
        		count += 1;
        		setCountText();
        	}
        	else if(other.gameObject.CompareTag("Destroyer"))
        	{ 
        		Application.LoadLevel(Application.loadedLevel);
   		
        	}
        }

        void setCountText()
        {
        	countText.text = "count: "+ count.ToString();
        	if(count >= 16)
        	{
        		winText.text = "You Win!!";
                Application.LoadLevel("Menu");
        	}
        }


    }
}
