using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FunWithDelegatesEventsLambdas
{
    class Car
    {
        #region Constructors and Load

        public Car()
        {
            MaxSpeed = 100;
        }

        public Car(string name, int maxSpeed, int currentSpeed)
        {
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;
            PetName = name;
        }

        #endregion
    
        #region Properties and Fields
        
        private bool carIsDead;

        // Fields...
        private string _PetName;
        private int _MaxSpeed;
        private int _CurrentSpeed;

        public int CurrentSpeed
        {
            get { return _CurrentSpeed; }
            set
            {
                _CurrentSpeed = value;
            }
        }

        public int MaxSpeed
        {
            get { return _MaxSpeed; }
            set
            {
                _MaxSpeed = value;
            }
        }

        public string PetName
        {
            get { return _PetName; }
            set
            {
                _PetName = value;
            }
        }

        #endregion

        public delegate void CarEngineHandler(string msgForCaller);

        private CarEngineHandler listOfHandlers;

        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers = methodToCall;
        }

        public void Accelerate(int delta)
        {
            Console.WriteLine("{1} CurrentSpeed = {0}", CurrentSpeed, PetName);

            if (carIsDead)
            {
                if (listOfHandlers != null)
                {
                    listOfHandlers(string.Format("Sorry, {0} went too fast :(", PetName));
                }
            }
            else
            {
                CurrentSpeed += delta;

                if (10 >= (MaxSpeed - CurrentSpeed)
                    && listOfHandlers != null)
                {
                    listOfHandlers(string.Format("Slow down, going near {0} max speed !!!", PetName));
                }
            
                if (CurrentSpeed >= MaxSpeed)
                {
                	carIsDead = true;
                }
                else
                {
                    
                }
            }
        }
    }
}
