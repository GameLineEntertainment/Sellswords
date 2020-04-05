using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OldSellswords
{
	public class CharacterComponent : MonoBehaviour
	{
		public Transform myTransform { get; private set; }
		//public GameObject My_Edge_Position;
		public List<GameObject> Edge_Positions;    //определяем переменную как хранилище всех стационарных позиций
		public Transform New_Edge_Position;       //внутренняя переменная, для хранения текущей/выбранной позиции
		public int Index = 0;

		[HideInInspector] public int rotationSpeed = 7;                  //скорость поворота
		[HideInInspector] public float maxDistance = 0.2f;               //максимальное приближение к игроку
		[HideInInspector] public int moveSpeed = 3;                      //скорость перемещения
		public float CoulDownTime = 1.1f;

		[HideInInspector] public MyAnim MyAnim;
		[HideInInspector] public bool Walking;
		[HideInInspector] public bool CanAttack;
		public MeshRenderer CharCircle;
		public ColorGroup ColorGroup;

		public BulletSettings[] Bullet; // Переделать в лист, чтобы удалять у экземпляра перса на сцене все снаряды, которые не пашут на данном уровне

		public Transform AttackPoint;

		public bool SortDistance;
		private static readonly int Opacity = Shader.PropertyToID("Opacity");
		private static readonly int TintColor = Shader.PropertyToID("_TintColor");

		private void Start()
		{
			rotationSpeed = 7;
			maxDistance = 0.2f;
			moveSpeed = 3;
			Edge_Positions = new List<GameObject>();

			myTransform = transform;
			CanAttack = false;

			AddAllPositions();
			MyAnim = GetComponentInChildren<MyAnim>();
			var circleRend = Instantiate(CharCircle, transform); // спауним круг цвета перса
			circleRend.transform.localPosition = new Vector3(0, -2, 0);
			circleRend.material.SetFloat(Opacity, 0.8F); // ставим ему прозрачность

			switch (ColorGroup) // устанавливаем цвет перса
			{
				case ColorGroup.Red:
				{
					circleRend.material.SetColor(TintColor, Color.red); break;
				}
				case ColorGroup.Green:
				{
					circleRend.material.SetColor(TintColor, Color.green); break;
				}
				case ColorGroup.Blue:
				{
					circleRend.material.SetColor(TintColor, Color.blue); break;
				}
			}
		}

		//поиск и добавление всех противников в список
		private void AddAllPositions()
		{
			//помещаем все позиции в массив go
			var go = GameObject.FindGameObjectsWithTag("Positions");
			//каждый элемент из найденных засовываем в массив потенциальных целей
			foreach (var Positions in go)
			{
				AddTarget(Positions);
			}

			Edge_Positions = Edge_Positions.OrderBy(obj => obj.name).ToList();

			New_Edge_Position = Edge_Positions[Index].transform;
		}

		//метод по добавлению в массив очередного элемента
		private void AddTarget(GameObject positions)
		{
			Edge_Positions.Add(positions);
		}
	}
}