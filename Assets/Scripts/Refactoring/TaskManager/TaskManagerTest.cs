using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OldSellswords
{
	public class TaskManagerTest : MonoBehaviour
	{
		public Button StartTaskQueue;
		public Button StopTaskQueue;

		public Image TargetImage;
		public Transform From;
		public Transform To;

		private TaskManager _taskManager = new TaskManager();

		private void Start()
		{
			StartTaskQueue.onClick.AddListener(StartTaskQueueClick);
			StopTaskQueue.onClick.AddListener(StopTaskQueueClick);
		}

		private void StartTaskQueueClick()
		{
			_taskManager.AddTask(MoveFromTo(TargetImage.gameObject.transform, From.position, To.position, 2f));
			_taskManager.AddTask(AlphaFromTo(TargetImage, 1f, 0f, 0.5f));
			_taskManager.AddTask(Wait(1f));
			_taskManager.AddTask(AlphaFromTo(TargetImage, 0f, 1f, 0.5f));
			_taskManager.AddTask(MoveFromTo(TargetImage.gameObject.transform, To.position, From.position, 2f));
		}

		private void StopTaskQueueClick()
		{
			if (_taskManager.CurrentTask != null)
			{
				_taskManager.Break();
			}
			else
			{
				_taskManager.Restore();
			}
		}

		private IEnumerator Wait(float time)
		{
			yield return new WaitForSeconds(time);
		}

		private IEnumerator MoveFromTo(Transform target, Vector3 from, Vector3 to, float time)
		{
			var t = 0f;
			do
			{
				t = Mathf.Clamp(t + Time.deltaTime, 0f, time);

				target.position = Vector3.Lerp(from, to, t / time);

				yield return null;
			} while (t < time);
		}

		private IEnumerator AlphaFromTo(Image target, float from, float to, float time)
		{
			var imageColor = target.color;
			var t = 0f;
			do
			{
				t = Mathf.Clamp(t + Time.deltaTime, 0f, time);

				imageColor.a = Mathf.Lerp(from, to, t / time);
				target.color = imageColor;

				yield return null;
			} while (t < time);
		}
	}
}