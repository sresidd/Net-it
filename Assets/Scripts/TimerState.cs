using UnityEngine;

    public class TimerState : MonoBehaviour
    {
        Timer _timer;

    void Start(){
        _timer = FindObjectOfType<Timer>();
    }

        public void UpdateTimerState(bool _state){
            if(_timer == null)
                return;
            else
            {
                _timer.GetComponent<Timer>().PauseTimer(_state);    
            }
        }
    }

