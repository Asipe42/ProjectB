using System;
using System.Collections;
using UnityEngine;

namespace Modin
{
    public class GameManager : MonoSingleton<GameManager>
    {
        /*
         * 역할
         *  - 인트로 및 타이틀 진입
         *  - 게임 저장
         *  - 게임 상태 관리
         */
        
        /*
         * 게임 상태
         *  - 인트로: 타이틀 전 진입 화면
         *  - 타이틀: 게임의 시작 화면
         *  - 게임 플레이: 실제 플레이 화면
         *  - 로딩: 게임 흐름 전환 시 대기 화면
         */

        [SerializeField] private GameConfig gameConfig;

        private GameState gameState;
        
        protected override void Awake()
        {
            base.Awake();
            gameState = GameState.Loading;
        }

        private void Start()
        {
            Run();
        }

        private void Run()
        {
            StartCoroutine(RunRoutine());
        }

        private IEnumerator RunRoutine()
        {
            
            
            yield return null;
        }
    }
}