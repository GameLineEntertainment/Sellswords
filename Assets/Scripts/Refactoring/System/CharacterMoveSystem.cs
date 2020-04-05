using UnityEngine;

namespace OldSellswords
{
	public class CharacterMoveSystem : ITick, IAwake
	{
		private CharacterComponent[] _characters;
		private Transform _takeALook;
		public event System.Action OnCharacterChange;

		public void OnAwake()
		{
			_characters = Object.FindObjectsOfType<CharacterComponent>();
			_takeALook = GameObject.Find("Side").transform;
		}

		public void Tick()
		{
			foreach (var character in _characters)
			{
				if (character.myTransform != character.New_Edge_Position)
				{
					Move(character);
				}
			}
		}

		public void Left()
		{
			if (!_characters[0].Walking && !_characters[1].Walking && !_characters[2].Walking)
			{
				RotateCharacters(true);
			}
		}

		public void Right()
		{
			if (!_characters[0].Walking && !_characters[1].Walking && !_characters[2].Walking)
			{
				RotateCharacters(false);
			}
		}

		private void RotateCharacters(bool left)
		{
			OnCharacterChange?.Invoke();

			for (var i = 0; i < _characters.Length; i++)
			{
				TargetPositions(left, _characters[i]);
			}
		}

		private void TargetPositions(bool isClockwise, CharacterComponent character)
		{
			if (isClockwise)
			{
				character.Index++;
				if (character.Index > 2)
				{
					character.Index = 0;
				}
			}

			if (!isClockwise)
			{
				character.Index--;
				if (character.Index < 0)
				{
					character.Index = 2;
				}
			}

			if (character.Index == 2)
			{
				character.CanAttack = true;
			}

			character.New_Edge_Position = character.Edge_Positions[character.Index].transform;
		}

		private void Move(CharacterComponent character)
		{
			var td = Time.deltaTime;

			if (Vector3.Distance(character.New_Edge_Position.position, 
				    character.myTransform.position) >= character.maxDistance)
			{
				character.Walking = true;

				character.MyAnim.Run(character.moveSpeed);

				character.myTransform.rotation = Quaternion.Slerp(character.myTransform.rotation,
					Quaternion.LookRotation(character.New_Edge_Position.position - character.myTransform.position),
					character.rotationSpeed * td);

				character.myTransform.position += character.myTransform.forward * character.moveSpeed * td;
			}
			else
			{
				character.myTransform.LookAt(_takeALook);
				character.MyAnim.Run(0);
				character.Walking = false;
			}
		}
	}
}