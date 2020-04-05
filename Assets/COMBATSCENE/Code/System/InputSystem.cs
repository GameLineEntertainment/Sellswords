using UnityEngine;

namespace CombatScene
{
    public class InputSystem :IAwake, IUpdate
    {
        private PlayerController _playerController;
        private bool _isClockWise;

        public void OnAwake()
        {
            _playerController = new PlayerController();
            _playerController.OnAwake();
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _isClockWise = false;
                _playerController.ChangePosition(_isClockWise);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _isClockWise = true;
                _playerController.ChangePosition(_isClockWise);
            }
           
            
        }
    }
}
