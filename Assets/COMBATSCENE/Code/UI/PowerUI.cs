using UnityEngine;
using UnityEngine.UI;

namespace CombatScene
{
    public class PowerUI: IAwake, IUpdate
    {
        private UIManager.Element[] _element;

        public static PowerUI Indicator;
        public float PowerTime { get; set; }
        public int Streak { get; set; }
        public float StepStreak { get; set; }
        public float Damage { get; set; }
        public float LerpSmooth { get; set; }

        private GameObject _charUi;
        
        private Image _energyBar;
        
        private float _timer = 0;
        private float _maxEnergy;
        private float _energy = 0;
        private float _step = 33;
        private float _percent;
        private float _timeLerp = 0;
        private int _colorId;
        private int _maxStreak = 3;

        private bool _updateMaxCount;
        private bool _isChangeColor;
        private bool _isHalfDone;
        private bool _isPowerDone;
        private bool _isDmgUp;

        private Color _nowColor;
        private Color _nextColor;

        private Color _full = Color.red;
        private Color _medium = Color.yellow;
        //private Color _null = Color.black;
        private Color _null = Color.white;
        //private Color _null = Color.grey;
        private Color _start = new Color(0.145f, 0.145f, 0.145f);
        //private Color _null = new Color(0.36f, 0.27f, 0);


        private CharacterModel[] _player = new CharacterModel[3];
        private Color[] _colors = new Color[3];

        public void OnAwake()
        {
            Indicator = this;
            Streak = 1;
            StepStreak = 100;
            Damage = 20;
             LerpSmooth = 2;
             _player = Object.FindObjectsOfType<CharacterModel>();
            _element = Object.FindObjectOfType<Manager>().UIElementSettings.UI;

            _colors[0] = _null;
            _colors[1] = _medium;
            _colors[2] = _full;

            _colorId = _colors.Length - 1;
            _nowColor = _start;
            _nextColor = _colors[0];

            for (int i = 0; i < _element.Length; i++)
            {
                if (_element[i].Name == "CharUI")
                {
                    PowerTime = _element[i].Value;
                    _charUi = Object.FindObjectOfType<Canvas>().transform.GetChild(0).gameObject;
                }
            }

            _energyBar = _charUi.transform.GetChild(1).GetChild(1).GetComponent<Image>();
            _energyBar.fillAmount = 0;
            _energyBar.color = _nowColor;

            _maxEnergy = PowerTime;
            _percent = PowerTime * _step / 100;
        }

        public void OnUpdate()
        {
            if (_energyBar.fillAmount <= 0 && _isPowerDone)
            {
                _isPowerDone = false;
                NewSpeed(-StepStreak);
                NewDamage(-Damage);
            }

            if (_energyBar.color == _nextColor && !_isChangeColor)
            {
                _timeLerp = 0;
                _isChangeColor = true;
                _nextColor = _medium;
                _nowColor = _energyBar.color;
            }

            if (_energyBar.fillAmount <= 0.5f && !_isHalfDone)
            {
                if (Streak == 3)
                {
                    NewSpeed(-StepStreak);
                    NewDamage(-Damage);
                }
                Streak = 2;
                _isHalfDone = true;
                _timeLerp = 0;
                _nextColor = _null;
                _nowColor = _energyBar.color;
            }
            else if (!_isChangeColor) _timeLerp += Time.deltaTime * LerpSmooth;
            else _timeLerp += Time.deltaTime / (_maxEnergy / 1f);

            _energyBar.color = Color.Lerp(_nowColor, _nextColor, _timeLerp);

            if (_energy <= 0)
            {
                Streak = 1;
                _timeLerp = 0;
                return;
            }

            if (_energy < _maxEnergy - _percent)
            {
                _nowColor = _energyBar.color;
                _maxEnergy -= _percent;
                _timeLerp = 0;
            }
            Ticker();
        }

        public void NewSettings(float powerTime, float speedStep, float damage, float SmoothScale)
        {
            _maxEnergy = powerTime;
            PowerTime = powerTime;
            StepStreak = speedStep;
            Damage = damage;
            LerpSmooth = SmoothScale;
        }

        private void Ticker()
        {
            _timer += Time.deltaTime;
            _energyBar.fillAmount -= Time.deltaTime / PowerTime;
            if (_timer >= 1f)
            {
                if (_energy != 0)
                {
                    _updateMaxCount = false;
                    _energy--;
                    _timer = 0;
                }
            }
        }

        public void ChangeValue(float count)
        {
            _energyBar.fillAmount = 1;
            _isPowerDone = true;
            _isHalfDone = false;
            _energy = count;
            if (Streak < 3)
            {
                NewSpeed(StepStreak);
                NewDamage(Damage);
                Streak++;
            }
            _nextColor = _colors[Streak-1];
            _isChangeColor = false;

            if (!_updateMaxCount)
            {
                _maxEnergy = _energy;
                _updateMaxCount = true;
            };

            _timer = 0;
        }

        public void NewSpeed(float speed)
        {
            foreach(CharacterModel pl in _player)
            {
                pl.Speed += pl.BasicSpeed * speed / 100;
            }
        }

        public void NewDamage(float damage)
        {
            foreach (CharacterModel pl in _player)
            {
                pl.Damage += pl.BasicDamage * damage / 100;
            }
        }
    }
}
