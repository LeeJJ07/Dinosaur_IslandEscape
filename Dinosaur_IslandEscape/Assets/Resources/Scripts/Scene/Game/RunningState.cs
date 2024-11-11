using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace JongJin
{
	public class RunningState : MonoBehaviour, IGameState
	{
		[Header("Object")]
		[SerializeField] private GameObject[] players;
		[SerializeField] private GameObject dinosaur;

		[Header("Distance")]
		[SerializeField] private float totalRunningDistance = 100.0f;
		[SerializeField] private float minDistance = 5.0f;
		[SerializeField] private float maxDistance = 15.0f;

		[Header("ProgressRate")]
		[SerializeField] private float tailMissionStartRate = 15.0f;
		// TODO<이종진> - 진행률에 따른 미션 이름 수정 필요 - 20241110
		[SerializeField] private float firstMissionRate = 35.0f;
		[SerializeField] private float secondMissionRate = 55.0f;
		[SerializeField] private float thirdMissionRate = 80.0f;

		private bool isPossibleTailMission = false;
		private bool isFirstMissionCompleted = false;
		private bool isSecondMissionCompleted = false;
		private bool isThirdMissionCompleted = false;

		private float[] playerDistance = { 0.0f, 0.0f, 0.0f, 0.0f };
		private float dinosaurDistance = 0.0f;
		private Vector3[] prevPlayerPosition = new Vector3[4];
		private Vector3 prevDinosaurPosition;
		private float firstRankerDistance = 0.0f;
		private float lastRankerDistance = 0.0f;

        public float FirstRankerDistance { get { return firstRankerDistance; } }

        public float ProgressRate { get { return firstRankerDistance / totalRunningDistance * 100.0f; } }

		public float DinosaurSpeed { get { return dinosaurSpeed; } }
		private float dinosaurSpeed = 2.0f;

		public float GetPlayerDistance(int playerNumber)
		{
			return playerDistance[playerNumber];
		}
		public Vector3 GetPlayerPrevPosition(int playerNumber)
		{
			return prevPlayerPosition[playerNumber];
		}
		public void EnterState()
		{
			dinosaurSpeed = dinosaur.GetComponent<DinosaurController>().Speed;

			if (prevDinosaurPosition.z <= 1e-3)
				return;

			SetInfo();

			Camera.main.GetComponent<CameraController>().enabled = true;
		}
		public void UpdateState()
		{
			if (!isPossibleTailMission && ProgressRate > tailMissionStartRate)
				isPossibleTailMission = true;
			Move();
			CalculateObjectDistance();
			CalculateRank();
		}

		public void ExitState()
		{
			SaveInfo();
			Camera.main.GetComponent<CameraController>().enabled = false;
        }
		private void Move()
		{
			transform.position = dinosaur.transform.forward * firstRankerDistance;
		}

		#region 씬 전환시 플레이어, 공룡 정보 Setting
		private void SetInfo()
		{
			for(int playerNum = 0; playerNum < players.Length; playerNum++)
				players[playerNum].transform.position = prevPlayerPosition[playerNum];
			dinosaur.transform.position = prevDinosaurPosition;
		}
		private void SaveInfo()
		{
			for (int playerNum = 0; playerNum < players.Length; playerNum++)
				prevPlayerPosition[playerNum] = players[playerNum].transform.position;
			prevDinosaurPosition = dinosaur.transform.position;
		}
		#endregion

		#region 플레이어, 공룡 거리 및 랭킹 계산
		private void CalculateObjectDistance()
		{
			for (int playerNum = 0; playerNum < players.Length; playerNum++)
				playerDistance[playerNum] = players[playerNum].transform.position.z;
			dinosaurDistance = dinosaur.transform.position.z;
		}
		private void CalculateRank()
		{
			firstRankerDistance = playerDistance[0];
			lastRankerDistance = playerDistance[0];

			for (int playerNum = 1; playerNum < players.Length; playerNum++)
			{
				if (playerDistance[playerNum] > firstRankerDistance) firstRankerDistance = playerDistance[playerNum];
				if (playerDistance[playerNum] < lastRankerDistance) lastRankerDistance = playerDistance[playerNum];
			}
		}
		#endregion

		#region 러버 밴딩의 최대 최소 범위 제한
		public bool IsBeyondMaxDistance(Vector3 position)
		{
			if (position.z - dinosaur.transform.position.z > maxDistance)
				return false;
			return true;
		}

		public bool IsUnderMinDistance(Vector3 position, out Vector3 curPos)
		{
			curPos = new Vector3(position.x, position.y, dinosaur.transform.position.z + minDistance);
			if (position.z - dinosaur.transform.position.z < minDistance)
				return true;
			return false;
		}
		#endregion

		#region Running 상태일 때 전이 조건
		public bool IsTailMissionTriggered()
		{
			if (!isPossibleTailMission)
				return false;
			if (minDistance < lastRankerDistance - dinosaurDistance)
				return false;
			return true;
		}

		public bool IsFirstMissionTriggered()
		{
			if (isFirstMissionCompleted)
				return false;
			if (ProgressRate < firstMissionRate)
				return false;

			isFirstMissionCompleted = true;
			return true;
		}
		public bool IsSecondMissionTriggered()
		{
			if (isSecondMissionCompleted)
				return false;
			if (ProgressRate < secondMissionRate)
				return false;

			isSecondMissionCompleted = true;
			return true;
		}
		public bool IsThirdMissionTriggered()
		{
			if (isThirdMissionCompleted)
				return false;
			if (ProgressRate < thirdMissionRate)
				return false;

			isThirdMissionCompleted = true;
			return true;
		}

		#endregion
	}
}